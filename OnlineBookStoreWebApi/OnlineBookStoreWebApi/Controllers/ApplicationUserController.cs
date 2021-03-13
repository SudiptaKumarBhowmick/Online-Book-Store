using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DLL.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Security.Policy;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using BLL.Models;
using DLL.DbContext;
using DLL.Repositories;
using DLL.Models.ApplicationModel;
using Microsoft.EntityFrameworkCore;
using DLL.Models.RequestViewModel;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private AuthenticationContext _authenticationContext;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, AuthenticationContext authenticationContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _authenticationContext = authenticationContext;
        }

        [HttpPost]
        [Route("Register")]
        //Post: /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            ApplicationUserRepository applicationUserRepository = new ApplicationUserRepository(_authenticationContext);
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var user = new User()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password
            };
            try
            {
                var resultApplicationUser = await _userManager.CreateAsync(applicationUser, model.Password);
                var resultUser = await _authenticationContext.User.AddAsync(user);
                await _authenticationContext.SaveChangesAsync();
                return Ok(resultApplicationUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //2nd Section
        [HttpPost]
        [Route("LogIn")]
        //Post: /api/ApplicationUser/LogIn
        public async Task<IActionResult> LogIn(LoginModel _loginModel)
        {
            var user = await _userManager.FindByEmailAsync(_loginModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, _loginModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { Message = "UserName or Password is incorrect." });
        }

        [HttpPost]
        [Route("UserStorage")]
        //Post: api/ApplicationUser/UserSession
        public async Task<IActionResult> UserStorageGenerate(LoginModel _loginModel)
        {
            var user = await _userManager.FindByEmailAsync(_loginModel.Email);
            if (user != null)
            {
                User user_id = new User()
                {
                    UserId = _authenticationContext.User.FirstOrDefault(a =>
                    a.Email == _loginModel.Email).UserId
                };
                var storage = user_id.UserId + "." + user.UserName;
                return Ok(new { storage });
            }
            else
                return BadRequest(new { Message = "Login Failed." });
        }

        [HttpPost]
        [Route("SocialAuthentication")]
        public async Task<IActionResult> SocialAuthentication(SocialAuthenticationModel socialAuthenticationModel)
        {
            if(socialAuthenticationModel != null)
            {
                var findUser = await _userManager.FindByEmailAsync(socialAuthenticationModel.Email);
                if (findUser == null)
                {
                    string fullName = socialAuthenticationModel.FirstName + " " + socialAuthenticationModel.LastName;
                    var applicationUser = new ApplicationUser()
                    {
                        UserName = socialAuthenticationModel.FirstName,
                        FullName = fullName,
                        Email = socialAuthenticationModel.Email,
                        PhoneNumber = ""
                    };
                    var user = new User()
                    {
                        UserName = socialAuthenticationModel.FirstName,
                        FullName = fullName,
                        Email = socialAuthenticationModel.Email,
                        PhoneNumber = "",
                        Password = ""
                    };
                    try
                    {
                        var resultApplicationUser = await _userManager.CreateAsync(applicationUser);
                        var resultUser = await _authenticationContext.User.AddAsync(user);
                        await _authenticationContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(new Claim[]
                //    {
                //        new Claim("UserID",findUser.Id.ToString())
                //    }),
                //    Expires = DateTime.UtcNow.AddDays(1),
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt_Secret)), SecurityAlgorithms.HmacSha256Signature)
                //};
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                //var token = tokenHandler.WriteToken(securityToken);
                //return Ok(new { token });
                return Ok(findUser);
            }
            else
            {
                return BadRequest(new { Message = "Login Failed" });
            }
        }

        [HttpGet]
        [Route("CheckUserStatus")]
        public async Task<IActionResult> GetCheckUserStatus(string userId)
        {
            var userStatus = 1;
            var findUserType = await _authenticationContext.User.FirstOrDefaultAsync(x => x.UserId == Convert.ToInt32(userId));
            var findUserRole = await _authenticationContext.User.FirstOrDefaultAsync(x => x.UserId == Convert.ToInt32(userId));
            if(findUserType.UserTypeId != null && findUserRole.UserRoleId != null)
            {
                userStatus = 2;
            }
            return Ok(new { userStatus });
        }
    }
}

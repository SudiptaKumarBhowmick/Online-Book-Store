using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Models;
using DLL.Models.ApplicationModel;
using DLL.Models.RequestViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//4th section
namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private AuthenticationContext _context;
        public UserProfileController(UserManager<ApplicationUser> userManager, AuthenticationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        //GET: /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber
            };
        }

        [HttpGet]
        [Route("UserProfileDetails")]
        public async Task<ActionResult<IEnumerable<UserProfileDetailsModel>>> GetUserProfileDetails(int userId)
        {
            try
            {
                return await _context.UserDetails
                .Where(r=> r.UserId == userId)
                .Select(u => new UserProfileDetailsModel()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateOfBirth = u.DateOfBirth,
                    Address = u.Address,
                    City = u.City,
                    PhoneNumber = u.PhoneNumber
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserProfileData(int userId, UserProfileDetailsModel userProfileDetailsModel)
        {
            try
            {
                var findUserProfileDetails = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == userId);
                findUserProfileDetails.FirstName = userProfileDetailsModel.FirstName;
                findUserProfileDetails.LastName = userProfileDetailsModel.LastName;
                findUserProfileDetails.DateOfBirth = userProfileDetailsModel.DateOfBirth;
                findUserProfileDetails.Address = userProfileDetailsModel.Address;
                findUserProfileDetails.City = userProfileDetailsModel.City;
                findUserProfileDetails.PhoneNumber = userProfileDetailsModel.PhoneNumber;
                var resultUserDetails = _context.UserDetails.Update(findUserProfileDetails);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("UserDetails")]
        public async Task<ActionResult<IEnumerable<UserDetailsViewModel>>> GetUserDetails(string userId)
        {
            return await _context.User
                .Where(x => x.UserId == Convert.ToInt32(userId))
                .Select(u => new UserDetailsViewModel
                {
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                }).ToListAsync();
        }
    }
}

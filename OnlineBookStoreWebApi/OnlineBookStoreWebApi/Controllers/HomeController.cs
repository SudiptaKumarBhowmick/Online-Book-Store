using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DLL.Models.ApplicationModel;
using DLL.Models;
using DLL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        AuthenticationContext _context;
        public HomeController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("UserDetails")]
        //Post:// api/Home/UserDetails
        public async Task<IActionResult> PostUserDetails(UserDetailsModel userDetailsModel)
        {
            UserType UserType = new UserType()
            {
                UserTypeId =
                _context.UserTypes.FirstOrDefault(a =>
                a.UserTypeName == userDetailsModel.UserTypeName).UserTypeId
            };
            UserRole UserRole = new UserRole()
            {
                UserRoleId =
                _context.UserRole.FirstOrDefault(c =>
                c.UserRoleName == userDetailsModel.UserTypeName).UserRoleId
            };

            int userId = Convert.ToInt32(userDetailsModel.userId);
            var dbUser = await _context.User.FirstOrDefaultAsync(x => x.UserId == userId);
            dbUser.UserRoleId = UserRole.UserRoleId;
            dbUser.UserTypeId = UserType.UserTypeId;
            var result_user = _context.User.Update(dbUser);
            await _context.SaveChangesAsync();

            var dbApplicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.UserName == userDetailsModel.userName);
            dbApplicationUser.UserId = userId;
            dbApplicationUser.UserRoleId = UserRole.UserRoleId;
            dbApplicationUser.UserTypeId = UserType.UserTypeId;
            var result_application_user = _context.ApplicationUsers.Update(dbApplicationUser);
            await _context.SaveChangesAsync();

            //UserType.UserTypeId = _context.UserTypes.FirstOrDefault(a => a.UserTypeName == userDetailsModel.UserTypeName).UserTypeId;
            //var userTypeId =  _context.UserTypes.Where(x => x.UserTypeName == userDetailsModel.UserTypeName).Select(x=> new { x.UserTypeId});
            //UserType usertype = new UserType() { UserTypeId = UserType.UserTypeId };
            string date_of_birth = userDetailsModel.DateOfBirth;
            var userDetails = new UserDetails()
            {
                FirstName = userDetailsModel.FirstName,
                LastName = userDetailsModel.LastName,
                DateOfBirth = userDetailsModel.DateOfBirth,
                Address = userDetailsModel.Address,
                City = userDetailsModel.City,
                Country = userDetailsModel.Country,
                Email = userDetailsModel.Email,
                PhoneNumber = userDetailsModel.PhoneNumber,
                UserTypeName = userDetailsModel.UserTypeName,
                UserTypeId = UserType.UserTypeId,
                UserRoleId = UserRole.UserRoleId,
                UserId = userId
            };
            await _context.UserDetails.AddAsync(userDetails);
            await _context.SaveChangesAsync();
            
            //userDetails.UserType = new UserType() { UserTypeId = usertype.UserTypeId };
            //await _context.UserDetails.AddAsync(userDetails);

            return Ok(userDetailsModel);
        }

        [HttpPost]
        [Route("PaymentDetails")]
        //Post: api/Home/PaymentDetails
        public async Task<IActionResult> PostPaymentDetails(PaymentDetailsModel paymentDetailsModel)
        {
            int user_id = Convert.ToInt32(paymentDetailsModel.userId);
            var paymentDetails = new PaymentDetailsUser()
            {
                Name = paymentDetailsModel.Name,
                CardNumber = paymentDetailsModel.CardNumber,
                ExpirationDate = paymentDetailsModel.ExpirationDate,
                FullName = paymentDetailsModel.FullName,
                AddressLineOne = paymentDetailsModel.AddressLineOne,
                AddressLineTwo = paymentDetailsModel.AddressLineTwo,
                City = paymentDetailsModel.City,
                State = paymentDetailsModel.State,
                Zip = paymentDetailsModel.Zip,
                Country = paymentDetailsModel.Country,
                PhoneNumber = paymentDetailsModel.PhoneNumber,
                UserId = user_id
            };
            var result_payment_details = await _context.PaymentDetailsUser.AddAsync(paymentDetails);
            await _context.SaveChangesAsync();

            return Ok(paymentDetailsModel);
        }

    }
}

using DLL.DbContext;
using DLL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public interface IApplicationUserRepository
    {
    }

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private AuthenticationContext _context;
        public ApplicationUserRepository(AuthenticationContext context) : base()
        {
            _context = context;
        }

        public async Task<Object> add_user(ApplicationUserModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password
            };

            return await _context.User.AddAsync(user);
        }
    }
}

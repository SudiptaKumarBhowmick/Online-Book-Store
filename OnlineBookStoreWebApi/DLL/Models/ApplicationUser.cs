using DLL.Models.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class ApplicationUser: IdentityUser, ITrackable
    {
        [Column(TypeName="nvarchar(150)")]
        public string FullName { get; set; }
        public Nullable<int> UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public Nullable<int> UserId { get; set; }
        public User User { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

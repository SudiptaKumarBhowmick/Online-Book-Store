using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class UserType: ITrackable
    {
        public int UserTypeId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string UserTypeName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<UserDetails> UserDetails { get; set; }
    }
}

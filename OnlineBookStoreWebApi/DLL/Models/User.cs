using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class User: ITrackable
    {
        public int UserId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; }
        public Nullable<int> UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public UserDetails UserDetails { get; set; }
        public PaymentDetailsUser PaymentDetailsUsers { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

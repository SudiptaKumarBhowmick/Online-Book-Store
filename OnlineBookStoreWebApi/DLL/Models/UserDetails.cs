using DLL.Models.Interface;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class UserDetails: ITrackable
    {
        public int UserDetailsId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string DateOfBirth { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string City { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string UserTypeName { get; set; }
        public Nullable<int> UserId { get; set; }
        public User User { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public Nullable<int> UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

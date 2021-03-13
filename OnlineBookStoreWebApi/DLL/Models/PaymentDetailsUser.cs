using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class PaymentDetailsUser: ITrackable
    {
        public int PaymentDetailsUserId { get; set; }
        [Column(TypeName ="varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string CardNumber { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string ExpirationDate { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string AddressLineOne { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string AddressLineTwo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string City { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string State { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Zip { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Country { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; }
        public Nullable<int> UserId { get; set; }
        public User User { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

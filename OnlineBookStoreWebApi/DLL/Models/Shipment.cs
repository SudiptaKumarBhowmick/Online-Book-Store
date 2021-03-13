using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class Shipment: ITrackable
    {
        [Key]
        public int ShipmentId { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string ShipmentTrackNum { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string AddressLineOne { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string AddressLineTwo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string City { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string State { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Zip { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string Country { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ShipmentDate { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Orders Orders { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Invoices Invoices { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

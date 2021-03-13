using DLL.Models.Interface;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace DLL.Models
{
    public class Invoices: ITrackable
    {
        [Key]
        public int InvoiceId { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string InvoiceNumber { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime InvoiceDate { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Orders Orders { get; set; }
        public Nullable<int> InvoiceStatusCode { get; set; }
        public RefInvoiceStatusCode RefInvoiceStatusCode { get; set; }
        public Shipment Shipment { get; set; }
        public Payments Payments { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

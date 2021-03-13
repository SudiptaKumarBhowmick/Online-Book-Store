using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class Payments: ITrackable
    {
        [Key]
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(8,3)")]
        public decimal PaymentAmount { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string InvoiceNum { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Invoices Invoices { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }
    }
}

using DLL.Models.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace DLL.Models
{
    public class RefOrderStatusCodes: ITrackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderStatusCode { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string OrderStatusDescription { get; set; } //completed, canceled
        public Orders Orders { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }

    }
}

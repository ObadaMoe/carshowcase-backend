using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowcase.Entities;

    public class PaymentDetails
{
    [Key]
    public int PaymentId { get; set; }
    [Column(TypeName ="nvarchar(100)")]
    public string CardHolder { get; set; } = "";
    [Column(TypeName = "nvarchar(16)")]
    public string CardNumber { get; set; } = "";
    [Column(TypeName = "nvarchar(5)")]
    public string ExpDate { get; set; } = "";
    [Column(TypeName = "nvarchar(3)")]
    public string CVV { get; set; } = "";
}


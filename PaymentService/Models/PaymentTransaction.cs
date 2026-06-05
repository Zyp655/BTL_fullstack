using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models;

public class PaymentTransaction
{
    [Key]
    public int TransactionId { get; set; }

    [Required]
    public int PaymentId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [MaxLength(30)]
    public string PaymentMethod { get; set; } = "TienMat"; // TienMat, ChuyenKhoan, TheTD

    [MaxLength(500)]
    public string? Note { get; set; }

    public int? ReceivedByUserId { get; set; } // Admin ID

    public DateTime PaidAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("PaymentId")]
    public Payment? Payment { get; set; }
}

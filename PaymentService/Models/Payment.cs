using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    public int StudentUserId { get; set; } // Reference ID from User

    public int ClassId { get; set; } // Reference ID from CourseService

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal RemainingAmount { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "ChuaTT"; // ChuaTT, DangTT, HoanTat

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("StudentUserId")]
    public User? StudentUser { get; set; }

    public ICollection<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();
}

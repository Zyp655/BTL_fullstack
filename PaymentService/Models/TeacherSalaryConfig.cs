using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentService.Models;

public class TeacherSalaryConfig
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UserId { get; set; }

    public decimal BaseSalary { get; set; } = 0;

    public decimal RatePerSession { get; set; } = 300000;

    public decimal StudentAllowanceRate { get; set; } = 0;

    [MaxLength(250)]
    public string? Notes { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}

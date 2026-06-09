using System.ComponentModel.DataAnnotations;

namespace CourseService.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string CategoryCode { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities;

[Table($"Categories")]
public class Category
{
    [Required]
    public DateTime CreatedDate { get; set; }

    [Key]
    public int Id { get; set; }

    [Required, MaxLength(250)]
    public string Name { get; set; } = string.Empty!;

    public string? Description { get; set; }

    public ICollection<Category__Product>? Products { get; set; }
}

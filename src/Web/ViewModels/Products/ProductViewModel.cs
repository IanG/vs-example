using System.ComponentModel.DataAnnotations;

namespace VSExample.Web.ViewModels.Products;

public class ProductViewModel
{
    [Required] 
    [StringLength(100)] 
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, 10000.00)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } 
    
}
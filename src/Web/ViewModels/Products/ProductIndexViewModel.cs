using VsExample.Application.Features.Products.Dtos;

namespace VSExample.Web.ViewModels.Products;

public class ProductIndexViewModel
{
    public string Title { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    public IEnumerable<ProductDto> Products { get; set; } = [];
}
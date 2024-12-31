namespace VsExample.Application.Features.Products.Dtos;

public class ProductQueryResult
{
    public IEnumerable<ProductDto> Products { get; set; } = [];
    public int TotalCount { get; set; }
}
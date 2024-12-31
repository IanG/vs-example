using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VsExample.Application.Features.Products.Commands;
using VsExample.Application.Features.Products.Dtos;
using VsExample.Application.Features.Products.Queries;
using VSExample.Web.ViewModels.Products;

namespace VSExample.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IMediator _mediator;

    public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        GetProducts.Query getProductsQuery = new GetProducts.Query(PageNumber: pageNumber, PageSize: pageSize);
        
        Result<ProductQueryResult> result = await _mediator.Send(getProductsQuery);

        ProductIndexViewModel productIndexViewModel = new()
        {
            Title = "Products List",
            TotalCount = result.Value.TotalCount,
            Products = result.Value.Products,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        return View(productIndexViewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        GetProduct.Query query = new GetProduct.Query(id);
        
        Result<ProductDto?> result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            ProductDto? product = result.Value;
            ProductDetailsViewModel productDetailsViewModel = new()
            {
                Id = product!.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
            };

            return View(productDetailsViewModel);    
        }
        
        return NotFound();
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new ProductViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        AddProduct.Command command = new AddProduct.Command
        (
            model.Name,
            model.Price,
            model.Description
        );

        Result<ProductDto> result = await _mediator.Send(command);
        
        if (result.IsSuccess)
        {
            ProductDto productDto = result.Value;
            TempData["Message"] = $"Product {productDto.Id} '{productDto.Name}' Created successfully.";
            return RedirectToAction(nameof(Index));
        }
        
        TempData["Error"] = "Failed to create product.";
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        DeleteProduct.Command command = new DeleteProduct.Command(id);
        Result<bool> result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            TempData["Message"] = $"Product {id} deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Error"] = $"Failed to delete product {id}.";
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        GetProduct.Query productQuery = new GetProduct.Query(id);
        Result<ProductDto?> result = await _mediator.Send(productQuery);

        if (result.IsFailed)
        {
            return NotFound();
        }

        ProductDto? productDto = result.Value;   
        ProductUpdateViewModel viewModel = new()
        {
            Id = productDto!.Id,
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description
        };
        
        return View(viewModel);
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProductUpdateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var command = new UpdateProduct.Command(viewModel.Id, viewModel.Name, viewModel.Price, viewModel.Description);
            
            Result<bool> result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                TempData["Message"] = $"Product {viewModel.Id} updated successfully.";
                return RedirectToAction(nameof(Index)); // Redirect back to product list
            }
            else
            {
                ModelState.AddModelError("", $"Failed to update product {viewModel.Id}.");
            }
        }

        return View(viewModel);
    }
}
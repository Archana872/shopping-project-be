using Microsoft.AspNetCore.Mvc;
using WebApplication1.BusinessLogic;
using WebApplication1.DataModel;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductRequest request)
    {
        var error = _productService.CreateProduct(request);

        if (error is not null)
        {
            return BadRequest(new { message = error });
        }

        return CreatedAtAction(nameof(GetProductById), new { id = 1 }, request);
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _productService.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productService.GetProductById(id);

        if (product is null)
        {
            return NotFound(new { message = "Product not found." });
        }

        return Ok(product);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, UpdateProductRequest request)
    {
        var error = _productService.UpdateProduct(id, request);

        if (error is not null)
        {
            return BadRequest(new { message = error });
        }

        return Ok(new { message = "Product updated successfully." });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var error = _productService.DeleteProduct(id);

        if (error is not null)
        {
            return BadRequest(new { message = error });
        }

        return Ok(new { message = "Product deleted successfully." });
    }
}

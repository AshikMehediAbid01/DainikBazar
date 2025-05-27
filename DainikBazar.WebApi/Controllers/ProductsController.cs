using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Application.Services.Interfaces;
using DainikBazar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.WebApi.Controllers;

//localhost:7155/api/products
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service)
    {
        _service = service;
    }



    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    // Create Product
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody]ProductDto product)
    {
        try
        {
            var productEntity = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt
            };

            await _service.CreateNewAsync(productEntity);
            return Ok(productEntity);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }

    // Get product
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int? id)
    {
        if (id == null) return NotFound();
        var product = await _service.GetByIdAsync(id.Value);
        if (product == null) return NotFound();

        return Ok(product);

    }

    // Update Product
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        if (product == null || product.ProductId == 0) return BadRequest("Product id Invalid");

        await _service.UpdateAsync(product);
        return Ok(product);

    }


    // Delete Product 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        if (id == null) return NotFound();
        await _service.DeleteAsync(id.Value);
        return Ok();
    }

}



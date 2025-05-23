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
    public async Task<IActionResult> CreateProduct(ProductDto product)
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

}



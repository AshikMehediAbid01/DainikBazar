using System.Threading.Tasks;
using AutoMapper;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Service.Models;
using DomainModels = DainikBazar.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.Service.Controllers;

//localhost:7155/api/products
[Route("api/[controller]/[action]")]
[ApiController]
public class ProductsController(IProductManager service, IMapper mapper) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await service.GetAllAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Create Product
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product serviceModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var domainModel = mapper.Map<DomainModels.Product>(serviceModel);

            await service.CreateNewAsync(domainModel);
            return Created();
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
        if (id == null || id <= 0)return BadRequest("Invalid product ID.");

        var domainModel = await service.GetByIdAsync(id.Value);
        if (domainModel == null) return NotFound("Product not found.");

        var serviceModel = mapper.Map<Product>(domainModel);
        return Ok(serviceModel);
    }


    // Update Product
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] Product serviceModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (serviceModel == null || serviceModel.ProductId == 0) return BadRequest("Product id Invalid");

        var domainModel = mapper.Map<DomainModels.Product>(serviceModel);

        await service.UpdateAsync(domainModel);
        return NoContent();
    }


    // Delete Product 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        try
        {
            if (id == null || id <= 0) return BadRequest("Product id Invalid");
            await service.DeleteAsync(id.Value);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}



using System.Threading.Tasks;
using AutoMapper;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
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
    public async Task<IActionResult> CreateProduct([FromBody]ProductDto productDto)
    {
        try
        {
            var productEntity = mapper.Map<Product>(productDto);


            await service.CreateNewAsync(productEntity);
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
        var product = await service.GetByIdAsync(id.Value);
        if (product == null) return NotFound();

    var productDto = mapper.Map<ProductDto>(product);

        return Ok(productDto);

    }

    // Update Product
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDto productDto)
    {
        if (productDto == null || productDto.ProductId == 0) return BadRequest("Product id Invalid");

        var entityProduct = mapper.Map<Product>(productDto);

        await service.UpdateAsync(entityProduct);
        return Ok(entityProduct);

    }


    // Delete Product 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        if (id == null) return NotFound();
        await service.DeleteAsync(id.Value);
        return Ok();
    }

}



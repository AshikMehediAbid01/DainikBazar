using System.Threading.Tasks;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public ProductsController(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
    public async Task<IActionResult> CreateProduct([FromBody]ProductDto productDto)
    {
        try
        {
            var productEntity = _mapper.Map<Product>(productDto);


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

        var productDto = _mapper.Map<ProductDto>(product);

        return Ok(productDto);

    }

    // Update Product
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDto productDto)
    {
        if (productDto == null || productDto.ProductId == 0) return BadRequest("Product id Invalid");

        var entityProduct = _mapper.Map<Product>(productDto);

        await _service.UpdateAsync(entityProduct);
        return Ok(entityProduct);

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



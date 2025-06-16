using System.Threading.Tasks;
using AutoMapper;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Service.Models;
using DomainModels = DainikBazar.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DainikBazar.Service.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReviewAndRatingController(IReviewManager service, IMapper mapper) : ControllerBase
{
    [HttpPost] 
    public async Task<IActionResult> CreateReview([FromBody] ReviewAndRating serviceModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try 
        {   
            if (serviceModel == null) return BadRequest("Review cannot be null");
            var domainModel = mapper.Map<DomainModels.ReviewAndRating>(serviceModel);

            await service.CreateNewAsync(domainModel);
            return Ok(domainModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet("{ProductId}")]
    public async Task<IActionResult> GetAllReviewByProductId(int? ProductId)
    {
        if (ProductId == null) return NotFound();
        try
        {
            var domainModel = await service.GetAllByIdAsync(ProductId.Value);
            var serviceModel = mapper.Map<List<ReviewAndRating>>(domainModel);
            return Ok(serviceModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

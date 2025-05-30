
using DainikBazar.Domain.Repository_Interfaces;
using DainikBazar.Storage.Data;
using DainikBazar.Storage.Models;
using DomainModels = DainikBazar.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DainikBazar.Storage.Repositories;

public class ReviewRepository(AppDbContext db, IMapper mapper) : IReviewRepository
{
    public async Task CreateNewAsync(DomainModels.ReviewAndRating domainModel)
    {
        var storageModel = mapper.Map<ReviewAndRating>(domainModel);

        await db.ReviewAndRatings.AddAsync(storageModel);
        await db.SaveChangesAsync();
    }


    public async Task<List<DomainModels.ReviewAndRating>> GetAllByIdAsync(int productId)
    {
        var storageModel = await db.ReviewAndRatings.Where(c=>c.ProductId == productId).ToListAsync();

        var domainModel = mapper.Map<List<DomainModels.ReviewAndRating>>(storageModel);
        return domainModel;
    }

}

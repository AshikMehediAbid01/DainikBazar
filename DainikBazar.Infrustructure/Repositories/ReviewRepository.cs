using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Domain.Entities;
using DainikBazar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _db;

    public ReviewRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task CreateNewAsync(ReviewAndRating reviewAndRating)
    {
        await _db.ReviewAndRatings.AddAsync(reviewAndRating);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ReviewAndRating>> GetAllByIdAsync(int productId)
    {
        return await _db.ReviewAndRatings.Where(c=>c.ProductId == productId).ToListAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
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


    public async Task CreateNewAsync(ReviewDto reviewAndRating)
    {
        var review = new ReviewAndRating
        {
            Review = reviewAndRating.Review,
            Rating = reviewAndRating.Rating,
            CreatedAt = reviewAndRating.CreatedAt,
            ProductId = reviewAndRating.ProductId,
            UserId = reviewAndRating.UserId
        };

        await _db.ReviewAndRatings.AddAsync(review);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ReviewAndRating>> GetAllByIdAsync(int productId)
    {
        return await _db.ReviewAndRatings.Where(c=>c.ProductId == productId).ToListAsync();
    }
}

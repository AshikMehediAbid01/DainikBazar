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

public class ReviewRepository(AppDbContext db) : IReviewRepository
{
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

        await db.ReviewAndRatings.AddAsync(review);
        await db.SaveChangesAsync();
    }

    public async Task<List<ReviewAndRating>> GetAllByIdAsync(int productId)
    {
        return await db.ReviewAndRatings.Where(c=>c.ProductId == productId).ToListAsync();
    }
}

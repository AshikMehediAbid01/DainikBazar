using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Services.Interfaces;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;

    public ReviewService(IReviewRepository repo)
    {
        _repo = repo;
    }
    public async Task CreateNewAsync(ReviewDto reviewAndRating)
    {
        await _repo.CreateNewAsync(reviewAndRating);
    }

    public async Task<List<ReviewAndRating>> GetAllByIdAsync(int productId)
    {
       return await _repo.GetAllByIdAsync(productId);

    }
}

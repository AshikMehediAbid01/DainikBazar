using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Managers.Interfaces;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Managers.Implementations;

public class ReviewManager : IReviewManager
{
    private readonly IReviewRepository _repo;

    public ReviewManager(IReviewRepository repo)
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

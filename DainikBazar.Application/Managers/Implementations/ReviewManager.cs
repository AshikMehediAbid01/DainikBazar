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

public class ReviewManager(IReviewRepository repo) : IReviewManager
{
    public async Task CreateNewAsync(ReviewDto reviewAndRating)
    {
        await repo.CreateNewAsync(reviewAndRating);
    }

    public async Task<List<ReviewAndRating>> GetAllByIdAsync(int productId)
    {
       return await repo.GetAllByIdAsync(productId);

    }
}

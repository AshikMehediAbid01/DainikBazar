using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Domain.Interfaces;

namespace DainikBazar.Domain.Managers.Implementations;

public class ReviewManager(IReviewRepository repo) : IReviewManager
{
    public async Task CreateNewAsync(ReviewAndRating reviewAndRating)
    {
        await repo.CreateNewAsync(reviewAndRating);
    }

    public async Task<List<ReviewAndRating>> GetAllByIdAsync(int productId)
    {
        return await repo.GetAllByIdAsync(productId);

    }
}

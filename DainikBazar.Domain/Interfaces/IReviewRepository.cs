using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Interfaces;

public interface IReviewRepository
{
    Task CreateNewAsync(ReviewAndRating reviewAndRating);
    Task<List<ReviewAndRating>> GetAllByIdAsync(int productId);

}

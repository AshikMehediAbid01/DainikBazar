using DainikBazar.UI.Models;

namespace DainikBazar.UI.ApiServices.Interfaces;

public interface IReviewApiService
{
    Task<bool> CreateReviewAsync(ReviewAndRatingVM review);
}

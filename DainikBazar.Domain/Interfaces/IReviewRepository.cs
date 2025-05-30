using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Interfaces;

public interface IReviewRepository
{
    Task CreateNewAsync(ReviewAndRating reviewAndRating);
    Task<List<ReviewAndRating>> GetAllByIdAsync(int productId);

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Domain.Repository_Interfaces;

public interface IReviewRepository
{
    Task CreateNewAsync(ReviewDto reviewAndRating);
    Task<List<ReviewAndRating>> GetAllByIdAsync(int productId);

}

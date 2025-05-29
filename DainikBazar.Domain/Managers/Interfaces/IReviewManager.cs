using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Domain.Managers.Interfaces;

public interface IReviewManager
{
    Task CreateNewAsync(ReviewAndRating reviewAndRating);
    Task<List<ReviewAndRating>> GetAllByIdAsync(int productId);

}


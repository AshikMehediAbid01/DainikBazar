using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Services.Interfaces;

public interface IReviewService
{
    Task CreateNewAsync(ReviewDto reviewAndRating);
    Task<List<ReviewAndRating>> GetAllByIdAsync(int productId);

}

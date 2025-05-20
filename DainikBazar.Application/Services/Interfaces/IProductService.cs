using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task CreateNewAsync(Product product);
}

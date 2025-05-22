
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DainikBazar.Domain.Entities;

public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }

}

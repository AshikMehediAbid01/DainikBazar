
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DainikBazar.Domain.Entities;

public class User
{
    public string Id { get; set; }
    public required string Name { get; set; }

}

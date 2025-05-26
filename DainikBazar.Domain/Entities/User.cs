
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DainikBazar.Domain.Entities;

public class User
{
    [Key]
    public string Id { get; set; }
    public required string Name { get; set; }

}

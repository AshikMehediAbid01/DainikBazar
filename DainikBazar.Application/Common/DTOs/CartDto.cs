

namespace DainikBazar.Application.Common.DTOs;

public class CartDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CartStatus { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    public decimal SubtotalPrice {get; set; }
    public decimal TotalPrice => SubtotalPrice;
}

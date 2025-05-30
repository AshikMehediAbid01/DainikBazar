using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Service.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string CartStatus { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public decimal SubtotalPrice => CartItems.Sum(item => item.Quantity * item.Product.Price);
    public decimal TotalPrice => SubtotalPrice;

}
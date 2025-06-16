using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Storage.Models;

public class CartItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public Product Product { get; set; }
}
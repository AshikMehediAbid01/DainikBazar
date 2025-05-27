using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Application.Common.DTOs;

public class UpdateProductDto
{
    public int cartItemId {  get; set; }
    public int quantity { get; set; }
}

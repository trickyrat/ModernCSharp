using ModernCSharp.Application.Enums;

namespace ModernCSharp.Application.Models;

public class Order
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}

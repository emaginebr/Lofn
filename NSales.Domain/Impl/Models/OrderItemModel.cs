namespace NSales.Domain.Impl.Models
{
    public class OrderItemModel
    {
        public long ItemId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

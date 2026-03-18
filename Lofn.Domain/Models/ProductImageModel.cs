namespace Lofn.Domain.Models
{
    public class ProductImageModel
    {
        public long ImageId { get; set; }
        public long ProductId { get; set; }
        public string Image { get; set; }
        public int SortOrder { get; set; }
    }
}

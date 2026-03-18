namespace Lofn.Domain.Models
{
    public class StoreModel
    {
        public long StoreId { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
    }
}

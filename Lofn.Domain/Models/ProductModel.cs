using Lofn.DTO.Product;
using System;

namespace Lofn.Domain.Models
{
    public class ProductModel
    {
        public long ProductId { get; set; }
        public long? StoreId { get; set; }
        public long? CategoryId { get; set; }
        public long UserId { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Frequency { get; set; }
        public int Limit { get; set; }
        public ProductStatusEnum Status { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public bool Featured { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string StripeProductId { get; set; }
        public string StripePriceId { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Lofn.DTO.Product
{
    public class ProductImageInfo
    {
        [JsonPropertyName("imageId")]
        public long ImageId { get; set; }
        [JsonPropertyName("productId")]
        public long ProductId { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("sortOrder")]
        public int SortOrder { get; set; }
    }
}

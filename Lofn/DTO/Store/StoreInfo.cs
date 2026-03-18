using System.Text.Json.Serialization;

namespace Lofn.DTO.Store
{
    public class StoreInfo
    {
        [JsonPropertyName("storeId")]
        public long StoreId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("ownerId")]
        public long OwnerId { get; set; }
    }
}

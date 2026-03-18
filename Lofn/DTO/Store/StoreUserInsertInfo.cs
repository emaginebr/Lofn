using System.Text.Json.Serialization;

namespace Lofn.DTO.Store
{
    public class StoreUserInsertInfo
    {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
    }
}

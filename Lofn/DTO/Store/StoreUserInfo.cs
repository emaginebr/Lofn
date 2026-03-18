using NAuth.DTO.User;
using System.Text.Json.Serialization;

namespace Lofn.DTO.Store
{
    public class StoreUserInfo
    {
        [JsonPropertyName("storeUserId")]
        public long StoreUserId { get; set; }
        [JsonPropertyName("storeId")]
        public long StoreId { get; set; }
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        [JsonPropertyName("user")]
        public UserInfo User { get; set; }
    }
}

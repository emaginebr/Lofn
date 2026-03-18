using System.Text.Json.Serialization;
using Lofn.DTO.Domain;

namespace Lofn.DTO.Store
{
    public class StoreResult : StatusResult
    {
        [JsonPropertyName("store")]
        public StoreInfo Store { get; set; }
    }
}

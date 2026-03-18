using System.Text.Json.Serialization;

namespace Lofn.DTO.Store
{
    public class StoreInsertInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

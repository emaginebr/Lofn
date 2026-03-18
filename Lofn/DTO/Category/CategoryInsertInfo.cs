using System.Text.Json.Serialization;

namespace Lofn.DTO.Category
{
    public class CategoryInsertInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

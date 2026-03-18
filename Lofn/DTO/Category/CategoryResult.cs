using System.Text.Json.Serialization;
using Lofn.DTO.Domain;

namespace Lofn.DTO.Category
{
    public class CategoryResult : StatusResult
    {
        [JsonPropertyName("category")]
        public CategoryInfo Category { get; set; }
    }
}

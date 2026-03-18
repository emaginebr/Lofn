using System.Collections.Generic;
using System.Text.Json.Serialization;
using Lofn.DTO.Domain;

namespace Lofn.DTO.Category
{
    public class CategoryListResult : StatusResult
    {
        [JsonPropertyName("categories")]
        public IList<CategoryInfo> Categories { get; set; }
    }
}

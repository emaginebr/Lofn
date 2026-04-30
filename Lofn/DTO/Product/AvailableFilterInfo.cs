using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lofn.DTO.Product
{
    public class AvailableFilterInfo
    {
        [JsonPropertyName("filterId")]
        public long FilterId { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("dataType")]
        public string DataType { get; set; }

        [JsonPropertyName("isRequired")]
        public bool IsRequired { get; set; }

        [JsonPropertyName("displayOrder")]
        public int DisplayOrder { get; set; }

        [JsonPropertyName("availableValues")]
        public IList<string> AvailableValues { get; set; } = new List<string>();
    }
}

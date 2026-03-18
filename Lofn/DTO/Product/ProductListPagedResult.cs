using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lofn.DTO.Product
{
    public class ProductListPagedResult
    {
        [JsonPropertyName("products")]
        public IList<ProductInfo> Products { get; set; }
        [JsonPropertyName("pageNum")]
        public int PageNum { get; set; }
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
    }
}

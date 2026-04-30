using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lofn.DTO.Product
{
    public class ProductSearchFilteredParam
    {
        [JsonPropertyName("storeSlug")]
        public string StoreSlug { get; set; }

        [JsonPropertyName("categorySlug")]
        public string CategorySlug { get; set; }

        [JsonPropertyName("filters")]
        public IList<ProductFilterValueAssign> Filters { get; set; }

        [JsonPropertyName("priceMin")]
        public double? PriceMin { get; set; }

        [JsonPropertyName("priceMax")]
        public double? PriceMax { get; set; }

        [JsonPropertyName("onlyOnSale")]
        public bool OnlyOnSale { get; set; }

        [JsonPropertyName("pageNum")]
        public int PageNum { get; set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lofn.DTO.Order
{
    public class OrderListPagedResult
    {
        [JsonPropertyName("orders")]
        public IList<OrderInfo> Orders { get; set; }
        [JsonPropertyName("pageNum")]
        public int PageNum { get; set; }
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
    }
}

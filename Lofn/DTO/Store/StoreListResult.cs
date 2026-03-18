using System.Collections.Generic;
using System.Text.Json.Serialization;
using Lofn.DTO.Domain;

namespace Lofn.DTO.Store
{
    public class StoreListResult : StatusResult
    {
        [JsonPropertyName("stores")]
        public IList<StoreInfo> Stores { get; set; }
    }
}

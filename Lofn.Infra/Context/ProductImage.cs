using System;
using System.Collections.Generic;

namespace Lofn.Infra.Context;

public partial class ProductImage
{
    public long ImageId { get; set; }

    public long ProductId { get; set; }

    public string Image { get; set; }

    public int SortOrder { get; set; }

    public virtual Product Product { get; set; }
}

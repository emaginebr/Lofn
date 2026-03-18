namespace Lofn.Infra.Context;

public partial class StoreUser
{
    public long StoreUserId { get; set; }

    public long StoreId { get; set; }

    public long UserId { get; set; }

    public virtual Store Store { get; set; }
}

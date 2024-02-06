namespace Shop.Core.Abstract;

public abstract class AbstractClass
{
    protected AbstractClass()
    {
        Created = DateTime.UtcNow;
        Updated = null;
        IsDeleted = false;
    }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public virtual bool IsDeleted { get; set; }
}


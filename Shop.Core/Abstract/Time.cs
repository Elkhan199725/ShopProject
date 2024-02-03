namespace Shop.Core.Abstract;

public abstract class AbstractClass
{
    protected AbstractClass()
    {
        // Set default values
        Created = DateTime.Now;
        Updated = null;
        Deleted = null;
        IsDeleted = false;
    }

    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
    public virtual bool IsDeleted { get; set; }
}


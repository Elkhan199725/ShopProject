namespace Shop.Core.Abstract;

public abstract class AbstractClass
{
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }
    public virtual bool IsDeleted { get; set; }
}


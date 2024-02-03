using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Admin:AbstractClass
{
    public int Id { get; set; }
    public string AdminName { get; set; } = null!;
    public string AdminEmail { get; set; } = null!;
    public string AdminPassword { get; set; } = null!;
}

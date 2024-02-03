using Shop.Core.Abstract;

namespace Shop.Core.Entities;

public class Discount : AbstractClass
{
    public Discount()
    {
    }

    public Discount(string? name, string? description, decimal? discountPercentage, DateTime? startDate, DateTime? endDate, ICollection<Product>? products)
    {
        Name = name;
        Description = description;
        DiscountPercentage = discountPercentage;
        StartDate = startDate;
        EndDate = endDate;
        Products = products;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<Product>? Products { get; set; }
}

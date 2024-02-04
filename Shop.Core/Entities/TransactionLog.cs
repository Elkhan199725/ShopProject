using Shop.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities;

public class TransactionLog : AbstractClass
{
    public int? Id { get; set; }
    public int? UserId { get; set; }
    public int? CardId { get; set; }
    public decimal? Amount { get; set; }
    public string? TransactionType { get; set; }

    public User? User { get; set; }
    public Card? Card { get; set; }
}

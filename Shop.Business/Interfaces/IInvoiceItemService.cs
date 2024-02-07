using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces;

public interface IInvoiceItemService
{
    Task<bool> CreateInvoiceItem(InvoiceItem newItem);
}
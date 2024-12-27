using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutboxExample.Contracts.Persistence;
using OutboxExample.Contracts.Persistence.Models;
using OutboxExampleAPI.DTOs;
using System.Linq;

namespace OutboxExampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public InvoiceController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitInvoice(InvoiceModel input)
        {
            var entries = new List<StandardEntry>();
            foreach (var item in input.Entries)
            {
                entries.Add(new StandardEntry(Guid.NewGuid(), item.Number, item.ItemName, item.Amount, item.IsDiscountLine));
            }
            var invoice = new Invoice(input.OrderNo, Guid.NewGuid(), input.BuyerName, entries);
            _appDbContext.Invoices.Add(invoice);
            await _appDbContext.SaveChangesAsync();

            return Ok(invoice);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInvoice(InvoiceModel input)
        {
            // 加载现有的 Invoice 实体及其 Entries
            var invoice = await _appDbContext.Invoices
                .Include(i => i.Entries)
                .FirstOrDefaultAsync(i => i.Id == input.Id);

            if (invoice is null)
            {
                return NotFound();
            }

            invoice.OrderNo = input.OrderNo;
            invoice.BuyerName = input.BuyerName;

            var importEntries = input.Entries.Select(ie => {
                if (ie.Id ==Guid.Empty)
                {
                    return new StandardEntry(Guid.NewGuid(), ie.Number, ie.ItemName, ie.Amount, ie.IsDiscountLine);
                }
                return new StandardEntry(ie.Id, ie.Number, ie.ItemName, ie.Amount, ie.IsDiscountLine);
            }).ToList();

            //importEntries.ForEach(ie => {
            //    var entryToUpdate = invoice.Entries.FirstOrDefault(e => e.Id == ie.Id);
            //    if (entryToUpdate is not null)
            //    {
            //        entryToUpdate.IsDiscountLine = ie.IsDiscountLine;
            //        entryToUpdate.Number = ie.Number;
            //        entryToUpdate.Amount = ie.Amount;
            //        entryToUpdate.ItemName = ie.ItemName;
            //    }
            //    else
            //    {
            //        invoice.Entries.Add(ie);
            //        _appDbContext.Add(ie);
            //    }
            //});

            // 使用字典来查找现有的条目
            var existingEntries = invoice.Entries.ToDictionary(e => e.Id);

            importEntries.ForEach(ie =>
            {
                var existingEntry = invoice.Entries.FirstOrDefault(e => e.Id == ie.Id);
                if (existingEntry is null)
                {
                    invoice.Entries.Add(ie);
                    _appDbContext.Add(ie);
                }
                else
                {
                    existingEntry.Number = ie.Number;
                    existingEntry.ItemName = ie.ItemName;
                    existingEntry.Amount = ie.Amount;
                    existingEntry.IsDiscountLine = ie.IsDiscountLine;
                }
            });

            var deleteingEntris = invoice.Entries.Where(e => !importEntries.Select(ie => ie.Id).Contains(e.Id));
            if (deleteingEntris.Count() > 0)
            {
                foreach (var item in deleteingEntris)
                {
                    _appDbContext.Remove(item);
                }
            }

            _appDbContext.Invoices.Update(invoice);

            // 不需要手动更新 invoice，因为它已经被加载并且状态是 Unchanged
            await _appDbContext.SaveChangesAsync();
            return Ok(invoice);
        }

    }
}

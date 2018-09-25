using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LedgerCore.Authentication;
using LedgerCore.Data.Entities;
using LedgerCore.Data.Repositories;
using LedgerCore.Filters.ActionFilters;
using LedgerCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace LedgerCore.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/Transactions")]
    public class TransactionsController : BaseController
    {
        public TransactionsController(DBContext context, IUrlHelper urlHelper) : base(context, urlHelper)
        {

        }

        public async Task<IActionResult> GetTransactions()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.USER_ID)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();


            var result = _dbContext.Transactions.Where(t => t.Account.UserId == Guid.Parse(userId))
                .AsEnumerable()
                .Select(transactionModel => Mapper.Map<TransactionDTO>(transactionModel));
            
            return Ok(result);

        }

        // GET: api/Transactions
        [HttpGet("{accountId}", Name = "get-transactions")]
        public IActionResult GetTransactions([FromRoute] Guid accountId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.USER_ID)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = _dbContext.Transactions.Where(t => t.Account.Id == accountId)
                .ToAsyncEnumerable()
                .Select(transactionModel => Mapper.Map<TransactionDTO>(transactionModel));
            return Ok(result);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}", Name = "get-transaction")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.USER_ID)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _dbContext.Transactions.Where(t => t.Id == id).SingleAsync();
            var transaction = Mapper.Map<TransactionDTO>(result);
            return Ok(transaction);
        }

        // POST: api/Transactions
        [HttpPost(Name="update-transaction")]
        [ValidateModel]
        public async Task<IActionResult> PostTransaction([FromBody]TransactionDTO transaction)
        {
            var existingTransaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
            if (existingTransaction == null)
                return BadRequest(new {Error = "No transactions exist with this ID:" + transaction.Id});
            var transactionModel = Mapper.Map<Transaction>(transaction);
            existingTransaction = transactionModel;
            _dbContext.Transactions.Update(transactionModel);
            await _dbContext.SaveChangesAsync();
            PopulateLinks(transaction);
            return new AcceptedAtRouteResult(new {id = transactionModel.Id}, transaction);
        }
        
        // PUT: api/Transactions/5
        [HttpPut(Name="add-transaction")]
        public void PutTransaction(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}", Name="delete-transaction")]
        public void DeleteTransaction([FromRoute] Guid id)
        {
        }

        private void PopulateLinks(IEnumerable<TransactionDTO> dtos)
        {
            foreach (var item in dtos)
            {
                PopulateLinks(item);
            }
        }

        private void PopulateLinks(TransactionDTO dto, string id = null)
        {
            var rel = "get-account";
            if (!string.IsNullOrEmpty(id) && dto.Id.ToString() == id)
            {
                rel = "self";
            }
            
            dto._links = new List<Link>
            {
                new Link{Method = "GET", Url = _urlHelper.Action("GetTransaction", new {id = dto.Id}), Rel=rel },
                new Link{Method = "POST", Url = _urlHelper.Action("PostTransaction"), Rel="update-user"}
            };
        }
    }
}

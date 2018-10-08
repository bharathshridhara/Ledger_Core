using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CacheCow.Server.Core.Mvc;
using LedgerCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LedgerCore.Data.Entities;
using LedgerCore.Data.Repositories;
using LedgerCore.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LedgerCore.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/Accounts")]
    public class AccountsController : BaseController
    {
        public AccountsController(DBContext context, IUrlHelper urlHelper) : base(context, urlHelper)
        {
            
        }

        // GET: api/Accounts
        [HttpGet( Name ="get-accounts")]
        [HttpCacheFactory(300)]
        public async Task<IActionResult> GetAccounts()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.USER_ID)?.Value;
            var accountsModel = await _dbContext.Accounts.Where(a => a.UserId == Guid.Parse(userId)).ToListAsync();
            var accountsDto = new List<AccountDTO>();
            if (accountsModel != null)
            {
                foreach (var account in accountsModel)
                {
                    var accountDTO = Mapper.Map<AccountDTO>(account);
                    accountsDto.Add(accountDTO);
                }
            }

            PopulateLinks(accountsDto);

            return Ok(accountsDto);


        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "get-account")]
        public async Task<IActionResult> GetAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _dbContext.Accounts.SingleOrDefaultAsync(m => m.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount([FromRoute] Guid id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(account).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Accounts
        [HttpPost]
        
        public async Task<IActionResult> PostAccount([FromBody] AccountDTO account)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CustomClaimType.USER_ID)?.Value;
            if (string.IsNullOrEmpty(userId) || Guid.Parse(userId) != account.UserId)
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currency = await _dbContext.Currencies.FirstOrDefaultAsync(c => c.Symbol == account.CurrencyISO);
            if (currency == null)
            {
                _dbContext.Currencies.Add(new Currency
                {
                    Name = account.CurrencyISO,
                    Symbol = "$"
                });
            }
            _dbContext.Accounts.Add(Mapper.Map<Account>(account));
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _dbContext.Accounts.SingleOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();

            return Ok(account);
        }

        private bool AccountExists(Guid id)
        {
            return _dbContext.Accounts.Any(e => e.Id == id);
        }

        private void PopulateLinks(IEnumerable<AccountDTO> dtos)
        {
            foreach (var item in dtos)
            {
                PopulateLinks(item);
            }
        }

        private void PopulateLinks(AccountDTO dto, string id = null)
        {
            var rel = "get-account";
            if (!string.IsNullOrEmpty(id) && dto.Id.ToString() == id)
            {
                rel = "self";
            }

            dto.TransactionsURL = new Link
            {
                Method = "GET",
                //Url = _urlHelper.Action("GetTransaction", "Transactions", new {id = dto.Id}),
                Rel = "get-transactions"
            };
            dto._links = new List<Link>
            {
                new Link{Method = "GET", Url = _urlHelper.Action("GetAccount", new {id = dto.Id}), Rel=rel },
                new Link{Method = "POST", Url = _urlHelper.Action("PostAccount"), Rel="update-user"}
            };
        }
    }
}
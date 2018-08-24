using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LedgerCore.Authentication;
using LedgerCore.Authentication.Providers;
using LedgerCore.Data.Repositories;
using LedgerCore.Filters.ActionFilters;
using LedgerCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LedgerCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : BaseController
    {
        private JwtAuthProvider _authProvider = new JwtAuthProvider();
        public HomeController(DBContext context) : base(context)
        {
                
        }
        // GET: api/Home
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Home/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser)
        {
            var user = await _dbContext.AuthenticateAsync(loginUser.Username, loginUser.Password);
            if (user != null)
            {
                return Ok(_authProvider.GenerateNewToken(user));
            }
            else
            {
                return Unauthorized();
            }
        }
        // POST: api/Home
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Home/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

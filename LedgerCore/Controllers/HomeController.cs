using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LedgerCore.Authentication;
using LedgerCore.Authentication.Providers;
using LedgerCore.Data.Entities;
using LedgerCore.Data.Repositories;
using LedgerCore.Filters.ActionFilters;
using LedgerCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        [Route("Register")]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            var existinguser = await _dbContext.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefaultAsync();
            if (existinguser != null)
                return BadRequest("Email already exists");

            var userModel = Mapper.Map<User>(user);
            var result =  _dbContext.Users.AddAsync(userModel);
            
            var token = _authProvider.GenerateNewToken(userModel);
            await result;
            await _dbContext.SaveChangesAsync();
            return Ok(new {token = token, expiry = "30 minutes"});
        }

        [HttpPost]
        [Route("Login")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUser)
        {
            var user = await _dbContext.AuthenticateAsync(loginUser.Email, loginUser.Password);
            if (user != null)
            {
                return Ok(new
                {
                    token = _authProvider.GenerateNewToken(user), expiry = "30 minutes",
                    userId = user.Id.ToString()
                });

            }
            else
            {
                return Unauthorized();
            }
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

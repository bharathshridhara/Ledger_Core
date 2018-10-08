using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using AutoMapper;
using CacheCow.Server.Core.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LedgerCore.Data.Entities;
using LedgerCore.Data.Repositories;
using LedgerCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Azure.KeyVault;

namespace LedgerCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly DBContext _context;
        private IUrlHelper _urlHelper;

        public UsersController(DBContext context, IUrlHelper urlHelper)
        {
            _context = context;
            _urlHelper = urlHelper;
        }

        // GET: api/Users
        [HttpGet(Name="get-users")]
        [HttpCacheFactory(300)]
        public async Task<IActionResult> GetUsers()
        {
            var users = _context.Users;
            var usersModel = new List<UserDTO>();
            foreach (var user in users)
            {
                var uservm = Mapper.Map<UserDTO>(user);
                usersModel.Add(uservm);
            }
            
                PopulateLinks(usersModel);
                return Ok(new LinkHelper<IEnumerable<UserDTO>> { Value = usersModel});
        }

        // GET: api/Users/5
        [HttpGet(template: "{id}", Name="get-user")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var gid = new Guid(id);
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == gid);
            var userDto = Mapper.Map<UserDTO>(user);
            if (user == null)
            {
                return NotFound();
            }
            PopulateLinks(userDto, userDto.Id.ToString());
            return Ok(userDto);
        }

        // PUT: api/Users/5
        [HttpPut(template:"{id}", Name = "update-user")]
        public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private void PopulateLinks(IEnumerable<UserDTO> dtos)
        {
            foreach (var item in dtos)
            {
                PopulateLinks(item);
            }
        }

        private void PopulateLinks(UserDTO dto, string userId = null)
        {
            var rel = "get-user";
            if (!string.IsNullOrEmpty(userId) && dto.Id.ToString() == userId)
            {
                rel = "self";
            }
            
            dto._links = new List<Link>
            {
                new Link{Method = "GET", Url = _urlHelper.Action("GetUser", new {id = dto.Id}), Rel=rel },
                new Link{Method = "POST", Url = _urlHelper.Action("PutUser"), Rel="update-user"}
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LedgerCore.Data.Repositories;
using LedgerCore.ViewModels;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Controllers
{
    public abstract class BaseController : Controller
    {
        internal DBContext _dbContext;
        internal IUrlHelper _urlHelper;
        public BaseController(DBContext dbContext, IUrlHelper urlHelper)
        {
            _dbContext = dbContext;
            _dbContext.User = HttpContext?.User;
            _urlHelper = urlHelper;
        }

        public BaseController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public void PopulateLinks(IEnumerable<Acco> dtos)
        //{
        //    foreach (var item in dtos)
        //    {
        //        PopulateLinks(item);
        //    }
        //}

        //public void PopulateLinks<T>(T dto, string id = null)
        //{
        //    var rel = "get-user";
        //    if (!string.IsNullOrEmpty(id) && T.Id.ToString() == id)
        //    {
        //        rel = "self";
        //    }

        //    T._links = new List<Link>
        //    {
        //        new Link{Method = "GET", Url = _urlHelper.Action("GetUser", new {id = dto.Id}), Rel=rel },
        //        new Link{Method = "POST", Url = _urlHelper.Action("PutUser"), Rel="update-user"}
        //    };
        //}
    }
}

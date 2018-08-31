using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LedgerCore.Data.Repositories;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Controllers
{
    public abstract class BaseController : Controller
    {
        internal DBContext _dbContext;
        public BaseController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}

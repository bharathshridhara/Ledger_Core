using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.OData.Query.SemanticAst;

namespace LedgerCore.ViewModels
{
    public class BaseDto
    {
        public List<Link> Links { get; set; }

        public BaseDto()
        {
            Links = new List<Link>();
        }
    }

    
}

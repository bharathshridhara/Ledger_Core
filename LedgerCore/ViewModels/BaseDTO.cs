using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.OData.Query.SemanticAst;

namespace LedgerCore.ViewModels
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
        public List<Link> _links { get; set; } = new
            List<Link>();

        public BaseDto()
        {
        }
    }
}

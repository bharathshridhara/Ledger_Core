using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LedgerCore.ViewModels;

namespace LedgerCore.Factories
{
    public class LinkFactory
    {
        public static LinkHelper<T> GetLinkedEntity<T>(T entity, List<Link> links) where T : class
        {
            var returnValue = new LinkHelper<T>();
            returnValue.Value = entity;
            returnValue.Links = links;

            return returnValue;
        }
    }
}

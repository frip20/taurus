using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class UsoFactory : IUso
    {
        public IEnumerable<Uso> searchByDescription(string search)
        {
            search = search.ToLower();
            return Uso.FindAll().Where(p => p.Description.ToLower().Contains(search));
        }
    }
}
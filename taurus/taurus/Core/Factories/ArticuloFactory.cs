using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class ArticuloFactory : IArticulo
    {
        public IEnumerable<Entities.Articulo> searchByNameOrPart(string search)
        {
            search = search.ToLower();
            return Articulo.FindAll().Where(a => a.Description.ToLower().Contains(search) || a.Parte.ToLower().Contains(search)); 
        }

        public Entities.Articulo searchObjectById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
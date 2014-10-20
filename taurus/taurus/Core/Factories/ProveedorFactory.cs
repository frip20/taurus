using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class ProveedorFactory : IProveedor
    {
        public Proveedor searchObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> searchByNameOrRfc(string search) {
            search = search.ToLower();
            return Proveedor.FindAll().Where(p => p.Description.ToLower().Contains(search) || p.RFC.ToLower().Contains(search)); 
        }
    }
}
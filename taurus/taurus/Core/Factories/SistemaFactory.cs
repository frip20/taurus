using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class SistemaFactory : ISistema
    {
        public Sistema searchObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sistema> searchByNameOrClave(string search)
        {
            search = search.ToLower();
            return Sistema.FindAll().Where(p => p.Description.ToLower().Contains(search) || p.Clave.ToLower().Contains(search));
        }
    }
}
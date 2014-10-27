using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class EmpleadoFactory : IEmpleado
    {
        public IEnumerable<Empleado> searchByDescription(string search)
        {
            search = search.ToLower();
            return Empleado.FindAll().Where(p => p.Description.ToLower().Contains(search) && p.Enable);
        }

        public Empleado searchObjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
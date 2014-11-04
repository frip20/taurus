using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Services;
using taurus.Core.Exceptions;

namespace taurus.Core.Factories
{
    public class EmpleadoFactory : IEmpleado
    {
        public IEnumerable<Empleado> searchByDescription(string search)
        {
            if (search != null)
            {
                search = search.ToLower();
                return Empleado.FindAll().Where(p => p.Description.ToLower().Contains(search) && p.Enable);
            }
            else {
                return getAllEmpleados();
            }
        }

        public IEnumerable<Empleado> getAllEmpleados() {
            try
            {
                return Empleado.FindAll().Where(p => p.Enable);
            }
            catch (Exception ex) {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllEmpleados"), ex);
            }
        }


        public Empleado searchObjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
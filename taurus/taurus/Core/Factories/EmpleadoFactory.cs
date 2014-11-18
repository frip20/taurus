using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Services;
using taurus.Core.Exceptions;
using System.Collections;
using NHibernate.Criterion;

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

        public IEnumerable filterBy(object criterias)
        {
            if (criterias == null)
                return getAllEmpleados();

            Empleado emp = (Empleado)criterias;
            DetachedCriteria dc = DetachedCriteria.For<Empleado>().Add(Restrictions.Eq("Enable", true));
            if (emp.Description != null && emp.Description.Trim() != "")
                dc.Add(Restrictions.Like("Description", "%" + emp.Description + "%"));
            //if (art.Parte != null && art.Parte.Trim() != "")
            //    dc.Add(Restrictions.Like("Parte", "%" + art.Parte + "%"));
            return Empleado.FindAll(dc);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class DepartmentFactory : IDepartamento
    {
        public IList<Departamento> getAllDepartments()
        {
            return Departamento.FindAll();
        }

        public Departamento searchObjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
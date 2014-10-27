using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface IDepartamento : ISearchable<Departamento>
    {
        IList<Departamento> getAllDepartments();
    }
}

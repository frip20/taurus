using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface ISistema : ISearchable<Sistema>
    {
        IEnumerable<Sistema> searchByNameOrClave(string search);
        Sistema getSistemaByDescription(string description);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface IConsumo : ISearchable<Consumo>
    {
        IEnumerable<Consumo> getAllConceptos();
    }
}
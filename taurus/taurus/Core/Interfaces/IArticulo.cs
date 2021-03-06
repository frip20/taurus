﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface IArticulo : ISearchable<Articulo>, IFilterable
    {
        IEnumerable<Articulo> searchByNameOrPart(string search);

        IEnumerable<Articulo> getAllArticulos();

        Articulo getArticuloByDescription(string description);
    }
}

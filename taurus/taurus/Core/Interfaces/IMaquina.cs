﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface IMaquina : ISearchable<Maquina>, IFilterable
    {
        IEnumerable<Maquina> getAllMaquinaria();
    }
}

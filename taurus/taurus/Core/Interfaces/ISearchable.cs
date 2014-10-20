using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taurus.Core.Interfaces
{
    public interface ISearchable<T> where T : class
    {
        T searchObjectById(int id);
    }
}

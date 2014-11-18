using System;
using System.Collections;

namespace taurus.Core.Interfaces
{
    public interface IFilterable 
    {
        IEnumerable filterBy(object criterias); 
    }
}

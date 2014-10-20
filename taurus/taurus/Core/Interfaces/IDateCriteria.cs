using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taurus.Core.Interfaces
{
    public interface IDateCriteria
    {
        DateTime startDate { get; set; }
        DateTime endDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taurus.Core.Constants;

namespace taurus.Core.Interfaces
{
    public interface IApiRequest
    {
        APIActions Action { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface ICuenta
    {
        IEnumerable<Cuenta> searchByNameOrCode(string search);
    }
}
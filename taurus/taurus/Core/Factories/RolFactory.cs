using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;

namespace taurus.Core.Factories
{
    public class RolFactory : IRolFactory
    {
        public IList<Rol> getAllRoles()
        {
            return Rol.FindAll();
        }
    }
}
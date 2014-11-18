using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Exceptions;
using taurus.Core.Services;

namespace taurus.Core.Factories
{
    public class CuentaFactory : ICuenta
    {
        public IEnumerable<Cuenta> searchByNameOrCode(string search)
        {
            try
            {
                search = search.ToLower();
                return Cuenta.FindAll().Where(p => p.Description.ToLower().Contains(search) || p.Codigo.ToLower().Contains(search));
            }
            catch (Exception ex) {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "searchByNameOrCode"), ex);
            }
        }
    }
}
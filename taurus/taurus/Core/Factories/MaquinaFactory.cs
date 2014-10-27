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
    public class MaquinaFactory : IMaquina
    {
        public IEnumerable<Maquina> getAllMaquinaria()
        {
            try
            {
                return Maquina.FindAll();
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllMaquinaria"), ex);
            }
        }

        public Maquina searchObjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
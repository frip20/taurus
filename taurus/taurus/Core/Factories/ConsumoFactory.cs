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
    public class ConsumoFactory : IConsumo
    {
        public IEnumerable<Consumo> getAllConceptos()
        {
            try
            {
                return Consumo.FindAll();
            }
            catch (Exception ex) {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllConceptos"), ex);
            }
        }

        public Consumo searchObjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
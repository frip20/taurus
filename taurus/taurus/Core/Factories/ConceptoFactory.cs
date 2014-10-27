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
    public class ConceptoFactory : IConcepto
    {
        public IEnumerable<Concepto> getAllConceptos()
        {
            try
            {
                return Concepto.FindAll();
            }
            catch (Exception ex) {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllConceptos"), ex);
            }
        }

        public Concepto searchObjectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
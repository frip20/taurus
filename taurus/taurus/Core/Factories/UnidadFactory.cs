using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Exceptions;
using taurus.Core.Services;
using NHibernate.Criterion;

namespace taurus.Core.Factories
{
    public class UnidadFactory : IUnidad
    {

        public IEnumerable<Unidad> getAllUnidades()
        {
            try
            {
                return Unidad.FindAll().Where(a => a.Enable);
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllUnidades"), ex);
            }
        }


        public Unidad getUnidadByDescription(string description)
        {
            try
            {
                return Unidad.FindFirst(DetachedCriteria.For<Unidad>().Add(Restrictions.Eq("Description", description)));
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getUnidadByDescription"), ex);
            }
        }
    }
}
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
    public class CategoriaFactory : ICategoria
    {

        public IEnumerable<Categoria> getAllCategorias()
        {
            try
            {
                return Categoria.FindAll().Where(a => a.Enable);
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllCategorias"), ex);
            }
        }


        public Categoria getCategoriaByDescription(string description)
        {
            try
            {
                return Categoria.FindFirst(DetachedCriteria.For<Categoria>().Add(Restrictions.Eq("Description", description)));
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getCategoriaByDescription"), ex);
            }
        }
    }
}
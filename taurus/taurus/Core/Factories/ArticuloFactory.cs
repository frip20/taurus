using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Services;
using taurus.Core.Exceptions;
using NHibernate.Criterion;

namespace taurus.Core.Factories
{
    public class ArticuloFactory : IArticulo
    {
        public IEnumerable<Articulo> searchByNameOrPart(string search)
        {
            if (search != null)
            {
                search = search.ToLower();
                return Articulo.FindAll().Where(a => (a.Description.ToLower().Contains(search) || a.Parte.ToLower().Contains(search)) && a.Enable);
            }
            else {
                return getAllArticulos();
            }
        }

        public Entities.Articulo searchObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Articulo> getAllArticulos()
        {
            try
            {
                return Articulo.FindAll().Where(a => a.Enable);
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllArticulos"), ex);
            }
        }


        public Articulo getArticuloByDescription(string description)
        {
            try
            {
                return Articulo.FindFirst(DetachedCriteria.For<Articulo>().Add(Restrictions.Eq("Description", description)));
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getArticuloByDescription"), ex);
            }
        }


        public System.Collections.IEnumerable filterBy(object criterias)
        {
            if (criterias == null)
                return getAllArticulos();

            Articulo art = (Articulo)criterias;
            DetachedCriteria dc = DetachedCriteria.For<Articulo>().Add(Restrictions.Eq("Enable", true));
            if (art.Description != null && art.Description.Trim() != "")
                dc.Add(Restrictions.Like("Description", "%"+art.Description+"%"));
            if (art.Parte != null && art.Parte.Trim() != "")
                dc.Add(Restrictions.Like("Parte", "%" + art.Parte + "%"));
            return Articulo.FindAll(dc);
        }
    }
}
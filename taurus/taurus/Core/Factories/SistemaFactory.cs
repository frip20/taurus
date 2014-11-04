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
    public class SistemaFactory : ISistema
    {
        public Sistema searchObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sistema> searchByNameOrClave(string search)
        {
            if (search != null)
            {
                try
                {
                    search = search.ToLower();
                    return Sistema.FindAll().Where(p => p.Description.ToLower().Contains(search) || p.Clave.ToLower().Contains(search));
                }
                catch (Exception ex)
                {
                    throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAreaByDescription"), ex);
                }
            }
            else {
                return getAllSistemas();
            }
        }

        public IEnumerable<Sistema> getAllSistemas()
        {
            try
            {
                return Sistema.FindAll().Where(a => a.Enable);
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllSistemas"), ex);
            }
        }

        public Sistema getSistemaByDescription(string description)
        {
            try
            {
                return Sistema.FindFirst(DetachedCriteria.For<Sistema>().Add(Restrictions.Eq("Description", description)));
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getSistemaByDescription"), ex);
            }
        }
    }
}
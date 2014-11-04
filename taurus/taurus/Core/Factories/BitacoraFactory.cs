using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Services;
using taurus.Core.Exceptions;
using NHibernate.Criterion;
using taurus.Core.Constants;

namespace taurus.Core.Factories
{
    public class BitacoraFactory : IBitacora
    {
        public IEnumerable<Bitacora> getBitacoraByMaquina(int maquinaId)
        {
            try
            {
                DetachedCriteria dc = DetachedCriteria.For<Bitacora>().Add(Restrictions.Eq("Enable", true));
                dc.SetMaxResults(ConfigurationService.Instance.getPropertyAsInt(ConfigurationConstants.MAX_RESULT_DATABASE));
                dc.CreateAlias("Maquina", "m");
                dc.Add(Restrictions.Eq("m.Id", maquinaId));
                dc.AddOrder(Order.Desc("createDate"));
                return Bitacora.FindAll(dc);
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getBitacoraByMaquina"), ex);
            }

        }

        public bool Save<T>(CastleProvider<T> entity)
        {
            try
            {
                entity.Enable = true;
                entity.SaveAndFlush();
                return true;
            }
            catch (Exception ex)
            {
                
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SAVE_ERROR, "Save"), ex);
            }

        }


        public bool Update<T>(CastleProvider<T> entity)
        {
            try
            {
                entity.UpdateAndFlush();
                return true;
            }
            catch (Exception ex)
            {

                throw new CastleActivityException(string.Format(MessageService.CASTLE_UPDATE_ERROR, "Update"), ex);
            }
        }

        public bool Delete<T>(CastleProvider<T> entity)
        {
            try
            {
                entity.DeleteAndFlush();
                return true;
            }
            catch (Exception ex)
            {

                throw new CastleActivityException(string.Format(MessageService.CASTLE_DELETE_ERROR, "Delete"), ex);
            }
        }
    }
}
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
    public class CastleProviderFactory : ICastleProvider
    {
        private int userId;

        public CastleProviderFactory() {
            HttpContext httpContext = HttpContext.Current;
            if (httpContext.ApplicationInstance.Session.Count > 0)
                userId = int.Parse(httpContext.ApplicationInstance.Session["uid"].ToString());
        }

        public bool Save<T>(CastleProvider<T> entity)
        {
            try
            {
                entity.Enable = true;
                entity.SaveAndFlush();
                entity.Refresh();

                AuditService.Instance.registerEvent(Constants.EventType.SAVE_CASTLE_ITEM, userId, entity.Id,
                    string.Format(MessageService.SAVE_CASTLE_ITEM, this.castleType(entity)));
                
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
                entity.Refresh();

                AuditService.Instance.registerEvent(Constants.EventType.UPDATE_CASTLE_ITEM, userId, entity.Id,
                    string.Format(MessageService.UPDATE_CASTLE_ITEM, this.castleType(entity)));

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

                AuditService.Instance.registerEvent(Constants.EventType.DELETE_CASTLE_ITEM, userId, entity.Id,
                    string.Format(MessageService.DELETE_CASTLE_ITEM, this.castleType(entity)));

                return true;
            }
            catch (Exception ex)
            {

                throw new CastleActivityException(string.Format(MessageService.CASTLE_DELETE_ERROR, "Delete"), ex);
            }
        }

        public int userInAction { get { return userId; } }

        private string castleType<T>(CastleProvider<T> entity) {
            return entity.GetType().Name;
        }
    }
}
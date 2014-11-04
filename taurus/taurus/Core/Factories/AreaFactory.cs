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
    public class AreaFactory : IArea
    {
        public IEnumerable<Area> getAllAreas()
        {
            try
            {
                return Area.FindAll().Where(a => a.Enable);
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAllAreas"), ex);
            }
        }


        public Area getAreaByDescription(string description)
        {
            try
            {
                return Area.FindFirst(DetachedCriteria.For<Area>().Add(Restrictions.Eq("Description", description)));
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_SEARCH_ERROR, "getAreaByDescription"), ex);
            }
        }
    }
}
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
    public class MaquinaFactory : IMaquina
    {
        public IEnumerable<Maquina> getAllMaquinaria()
        {
            try
            {
                return Maquina.FindAll().Where(m => m.Enable);
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

        public System.Collections.IEnumerable filterBy(object criterias)
        {
            if (criterias == null)
                return Maquina.FindAll().Where(m => m.Enable);

            Maquina maq = (Maquina)criterias;
            DetachedCriteria dc = DetachedCriteria.For<Maquina>().Add(Restrictions.Eq("Enable", true));
            if (maq.Description != null && maq.Description.Trim() != "")
                dc.Add(Restrictions.Like("Description", "%" + maq.Description + "%"));
            if (maq.Placa != null && maq.Placa.Trim() != "")
                dc.Add(Restrictions.Like("Placa", "%" + maq.Placa + "%"));
            if (maq.Operador != null && maq.Operador.Description != null) {
                dc.CreateAlias("Operador", "op").Add(Restrictions.Like("op.Description", "%" + maq.Operador.Description + "%"));
            }
                
            return Maquina.FindAll(dc);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Dbutil;
using NHibernate.Criterion;
using taurus.Core.Services;
using taurus.Core.Constants;

namespace taurus.Core.Factories
{
    public class StockFactory : IStock
    {
        public void addStock(Stock stock)
        {
            if (stock != null) {
                stock.Enable = true;
                stock.SaveAndFlush();

                if (stock.Items != null && stock.Items.Count > 0) {
                    foreach (StockItem item in stock.Items)
                    {
                        item.Enable = true;
                        item.Stock = stock;
                        item.SaveAndFlush();
                    }
                }
            }
        }
        
        public IEnumerable<Stock> getStockMovs(StockMovCriteria criteria)
        {
            try { 
                DetachedCriteria dc = DetachedCriteria.For<Stock>().Add(Restrictions.Eq("Enable", true));
                dc.SetMaxResults(ConfigurationService.Instance.getPropertyAsInt(ConfigurationConstants.MAX_RESULT_DATABASE));
                if (criteria != null) {
                    if (criteria.Proveedor != null) {
                        dc.Add(Restrictions.Eq("ProveedorId", criteria.Proveedor.Id));
                    }

                    if (criteria.Factura != null && criteria.Factura.Trim() != "") {
                        dc.Add(Restrictions.Like("Factura", "%"+criteria.Factura+"%"));
                    }

                    if (criteria.startDate != null && criteria.startDate.Year >= 2001) { 
                        dc.Add(Restrictions.Between("CreateDate", criteria.startDate.Date, criteria.endDate.Date.AddDays(1)));
                    }
                }
                dc.AddOrder(Order.Desc("CreateDate"));
                return Stock.FindAll(dc);
            }catch(Exception ex){
                throw ex;
            }
        }

        public Stock searchObjectById(int id)
        {
            return Stock.Find(id);
        }
    }
}
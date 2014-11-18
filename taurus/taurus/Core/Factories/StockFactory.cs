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
using taurus.Core.Exceptions;

namespace taurus.Core.Factories
{
    public class StockFactory : IStock
    {
        public void afectarSaldo(StockItem item, StockType type) {
            try
            {
                Saldo saldo = Saldo.FindFirst(DetachedCriteria.For<Saldo>().CreateAlias("Articulo", "art").Add(Restrictions.Eq("art.Id", item.Articulo.Id)));

                if (saldo == null || saldo.Id <= 0)
                {
                    saldo = new Saldo()
                    {
                        Articulo = item.Articulo,
                        Enable = true,
                        Entradas = 0,
                        Salidas = 0,
                        Unitario = 0
                    };
                }

                if (item.Enable)
                {
                    if (item.Id > 0)
                    {
                        if (saldo.Unitario > 0)
                            removeSaldo(item, type, saldo);
                    }
                    addSaldo(item, type, saldo);
                }
                else
                {
                    removeSaldo(item, type, saldo);
                }

                saldo.SaveAndFlush();
            }
            catch (Exception ex)
            {
                throw new CastleActivityException(string.Format(MessageService.AFFECT_SALDO_ISSUE, item.Articulo.Description), ex);
            }
            
        }

        private static void removeSaldo(StockItem item, StockType type, Saldo saldo)
        {
            if (type == StockType.ENTRADA){
                float _total = ((saldo.Entradas * saldo.Unitario) - (item.Unitario * item.Cantidad));
                saldo.Entradas -= item.Cantidad;
                saldo.Unitario = (saldo.Entradas > 0) ? (float)(_total / saldo.Entradas) : 0;
            }else{
                saldo.Salidas -= item.Cantidad;
            }
        }

        private static void addSaldo(StockItem item, StockType type, Saldo saldo)
        {
            if (type == StockType.ENTRADA){
                float _total = ((saldo.Entradas * saldo.Unitario) + (item.Unitario * item.Cantidad));
                saldo.Entradas += item.Cantidad;
                saldo.Unitario = (_total / saldo.Entradas);
            }else{
                saldo.Salidas += item.Cantidad;
            }
        }

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
                        this.afectarSaldo(item, stock.Type);
                    }
                }
            }
        }

        public void updateStock(Stock stock) {
            if (stock != null)
            {
                stock.Enable = true;
                stock.UpdateAndFlush();

                if (stock.Items != null && stock.Items.Count > 0)
                {
                    foreach (StockItem item in stock.Items)
                    {
                        if (item.Id > 0) {
                            item.Enable = true;
                            item.Stock = stock;
                            item.UpdateAndFlush();
                        }
                        else
                        {
                            item.Enable = true;
                            item.Stock = stock;
                            item.SaveAndFlush();
                        }
                        afectarSaldo(item, stock.Type);
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

        public void deleteStock(Stock stock)
        {
            try
            {
                stock = Stock.Find(stock.Id);
                foreach (StockItem item in stock.Items)
                {
                    item.Enable = false;
                    item.SaveAndFlush();
                    afectarSaldo(item, stock.Type);
                }
                stock.Enable=false;
                stock.SaveAndFlush();
            }
            catch (Exception ex) {
                throw new CastleActivityException(string.Format(MessageService.CASTLE_DELETE_ERROR, "deleteStock"), ex);
            }

        }

        public void saveUso(Uso uso)
        {
            try
            {
                uso.SaveAndFlush();
            }
            catch (Exception ex)
            {

                throw new CastleActivityException(string.Format(MessageService.CASTLE_SAVE_ERROR, "saveUso"), ex);
            }
        }
    }
}
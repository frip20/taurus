using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taurus.Core.Entities;
using taurus.Core.Dbutil;
using taurus.Core.Constants;

namespace taurus.Core.Interfaces
{
    public interface IStock : ISearchable<Stock>
    {
        void addStock(Stock stock);

        void updateStock(Stock stock);

        void deleteStock(Stock stock);

        IEnumerable<Stock> getStockMovs(StockMovCriteria criteria);

        void saveUso(Uso uso);

        void afectarSaldo(StockItem item, StockType type); 
    }
}

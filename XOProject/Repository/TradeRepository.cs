using System;
using System.Collections.Generic;
using System.Linq;
namespace XOProject
{
    public class TradeRepository : GenericRepository<Trade>, ITradeRepository
    {
        public TradeRepository(ExchangeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Trade GetById(int tradeId)
        {
            if (tradeId == 0)
                throw new ArgumentNullException();
            var query = _dbContext.Trades;
            return query.FirstOrDefault(x => x.Id == tradeId);
        }

        public IQueryable<Trade> GetTradesBySymbol(string symbol)
        {
            if (symbol == "")
                throw new ArgumentNullException("symbol");

            var query = _dbContext.Trades;
            return query.Where(x => x.Symbol == symbol);
           
        }
    }
}
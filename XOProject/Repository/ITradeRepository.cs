using System.Collections.Generic;
using System.Linq;

namespace XOProject
{
    public interface ITradeRepository : IGenericRepository<Trade>
    {
        IQueryable<Trade> GetTradesBySymbol(string symbol);
        Trade GetById(int tradeId);
    }
}
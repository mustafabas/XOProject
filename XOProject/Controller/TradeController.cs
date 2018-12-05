using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace XOProject.Controller
{
    [Route("api/Trade/")]
    public class TradeController : ControllerBase
    {
        private IShareRepository _shareRepository { get; set; }
        private ITradeRepository _tradeRepository { get; set; }
        private IPortfolioRepository _portfolioRepository { get; set; }

        public TradeController(IShareRepository shareRepository, ITradeRepository tradeRepository, IPortfolioRepository portfolioRepository)
        {
            _shareRepository = shareRepository;
            _tradeRepository = tradeRepository;
            _portfolioRepository = portfolioRepository;
        }


        [HttpGet("{portFolioid}")]
        public async Task<IActionResult> GetAllTradings([FromRoute]int portFolioid)
        {
            var trades = _tradeRepository.Query().Where(x => x.PortfolioId.Equals(portFolioid)).ToList();

            if (trades.Count == 0) {
                return NotFound();
            }

            return Ok(trades);
        }
        [HttpGet("tradeId")]
        public async Task<IActionResult> Get([FromRoute]int tradeId)
            {
            var trade = _tradeRepository.GetById(tradeId);
            if (trade != null)
                return Ok(trade);
            else
                return NotFound();

            }
        [HttpPost]
        public async Task<IActionResult> InsertTrade([FromRoute]TradeModel tradeModel)
        {
            if (ModelState.IsValid)
            {
                Trade trade = new Trade();
                trade.Action = tradeModel.Action;
                trade.NoOfShares = tradeModel.NoOfShares;
              
                trade.Symbol = tradeModel.Symbol;
                trade.PortfolioId = tradeModel.PortfolioId;
                await _tradeRepository.InsertAsync(trade);
                return Created("api/Trade/" + trade.Id,trade);
            }
            else
                return BadRequest();

        }
        /// <summary>
        /// For a given symbol of share, get the statistics for that particular share calculating the maximum, minimum, average and sum of price for all the trades that happened for that share. 
        /// Group statistics individually for all BUY trades and SELL trades separately.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>

        [HttpGet("Analysis/{symbol}")]
        public async Task<IActionResult> GetAnalysis([FromRoute]string symbol)
        {
            var list = new List<TradeAnalysis>();
            var tradeBuys =  _tradeRepository.GetTradesBySymbol(symbol);
            var sumForBuy = tradeBuys.GroupBy(x => x.Action).Select(t => new { action=t.Key,sum = t.Sum(i => i.Price), avarage = t.Average(i => i.Price), max = t.Max(i => i.Price), min = t.Min(i => i.Price) });
            if (sumForBuy.ToList().Count>0)
            {
                foreach (var item in sumForBuy)
                {
                    TradeAnalysis tradeAnalysis = new TradeAnalysis();
                    tradeAnalysis.Maximum = item.max;
                    tradeAnalysis.Minimum = item.min;
                    tradeAnalysis.Sum = item.sum;
                    tradeAnalysis.Average = item.avarage;
                    tradeAnalysis.Action = item.action;
                    list.Add(tradeAnalysis);

                }
                return Ok(list);
            }
            else
            {
                return NotFound();
            }
     
         
          
        }


    }
}

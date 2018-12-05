using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XOProject.Controller;

namespace XOProject.Tests
{
  
   public class TradeControllerTest
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
        private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();
        private readonly TradeController _tradeController;


        public TradeControllerTest()
        {
            var tradeList = new List<Trade> {
                new Trade {Action="BUY",Id=1,PortfolioId=1,Price=1500,Symbol="REL",NoOfShares=50 },
                 new Trade {Action="SELL",Id=1,PortfolioId=1,Price=2600,Symbol="REL",NoOfShares=10 },
                 new Trade {Action="BUY",Id=1,PortfolioId=1,Price=3600,Symbol="REL",NoOfShares=90 }
            };
            _tradeRepositoryMock.Setup(e => e.Query()).Returns(tradeList.AsQueryable());
            _tradeController = new TradeController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);
        }

        [Test]
        public async Task Trade_GET_ANALYSIS()
        {
            string symbol = "REL";

            var tradeList = new List<Trade> {
                new Trade {Action="BUY",Id=1,PortfolioId=1,Price=1500,Symbol="REL",NoOfShares=50 },
                 new Trade {Action="SELL",Id=1,PortfolioId=1,Price=2600,Symbol="REL",NoOfShares=10 },
                 new Trade {Action="BUY",Id=1,PortfolioId=1,Price=3600,Symbol="REL",NoOfShares=90 }
            };
            _tradeRepositoryMock.Setup(e => e.GetTradesBySymbol(symbol)).Returns(tradeList.AsQueryable());

            var tradeController = new TradeController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);

            OkObjectResult result = await tradeController.GetAnalysis(symbol) as OkObjectResult;

            Assert.AreEqual(result.StatusCode, 200);


        }
        [Test]
        public async Task TradeGetPortfolioIdValu1()
        {
            int prid = 1;
                
            var result = await _tradeController.GetAllTradings(prid);
            OkObjectResult okresul = result as OkObjectResult;

            Assert.IsNotNull(okresul.Value);
         
        }
        [Test]
        public async Task GetTradeByID()
        {
            var id = 1;
            var trade = new Trade { Action = "BUY", Id = 1, PortfolioId = 1, Price = 1500, Symbol = "REL", NoOfShares = 50 };

            _tradeRepositoryMock.Setup(t => t.GetById(id)).Returns(trade);

            var tradeController = new TradeController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);
            var result = await tradeController.Get(id);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

        }
        [Test]
        public async Task Post_TradeModelWithRequirements()
        {
            var tradeModel = new TradeModel();
            tradeModel.Action = "BUY";
            tradeModel.NoOfShares = 50;
            tradeModel.PortfolioId = 1;
            tradeModel.Symbol = "REL";
            var result =await _tradeController.InsertTrade(tradeModel);
            Assert.NotNull(result);
            CreatedResult createdResult = result as CreatedResult;

            Assert.AreEqual(createdResult.StatusCode, 201);

        }
     

    }
}

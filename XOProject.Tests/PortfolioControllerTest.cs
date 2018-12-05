using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOProject.Controller;

namespace XOProject.Tests
{
    public class PortfolioControllerTest
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();
        private readonly Mock<ITradeRepository> _tradeRepositoryMock = new Mock<ITradeRepository>();
        private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new Mock<IPortfolioRepository>();
        private readonly PortfolioController _portfolioController;
        public PortfolioControllerTest()
        {
            var portfolios = new List<Portfolio>
            {
                new Portfolio{Id=1,Name="Dominika",Trade=new List<Trade>()},
                new Portfolio{Id=2, Name="Mustafa", Trade=new List<Trade>()}

            };
            _portfolioRepositoryMock.Setup(p => p.GetAll()).Returns(portfolios.AsQueryable());
            _portfolioController = new PortfolioController(_shareRepositoryMock.Object, _tradeRepositoryMock.Object, _portfolioRepositoryMock.Object);
        }

        [Test]
        public async Task Post_PortfolioShouldWithName()
        {
            var portifolio = new Portfolio { Name = "Dominika", Trade = new List<Trade>() };
           var result=await _portfolioController.Post(portifolio);
            var createdResult = result as CreatedResult;
            Assert.AreEqual(createdResult.StatusCode, 201);
        }
        [Test]
        public async Task Get_Portfolio()
        {

            var result = await _portfolioController.GetPortfolioInfo(1);
            OkObjectResult okObjectResult = result as OkObjectResult;
            Assert.AreEqual(okObjectResult.StatusCode, 200);
        }
   
    }
}

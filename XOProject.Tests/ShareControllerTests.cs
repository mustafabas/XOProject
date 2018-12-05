using System;
using System.Threading.Tasks;
using XOProject.Controller;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace XOProject.Tests
{
    public class ShareControllerTests
    {
        private readonly Mock<IShareRepository> _shareRepositoryMock = new Mock<IShareRepository>();

        private readonly ShareController _shareController;

        public ShareControllerTests()
        {
            var shareList = new List<HourlyShareRate> {
                new HourlyShareRate {Id=5,Rate=10.0M,Symbol="REL",TimeStamp=DateTime.Now },
                new HourlyShareRate {Id=6,Rate=10.0M,Symbol="REL",TimeStamp=DateTime.Now }
            };
            _shareRepositoryMock.Setup(e => e.Query()).Returns(shareList.AsQueryable());
        

            _shareController = new ShareController(_shareRepositoryMock.Object);
        }

        [Test]
        public async Task Post_ShouldInsertHourlySharePrice()
        {
            var hourRate = new HourlyShareRate
            {
                Symbol = "CBI",
                Rate = 330.0M,
                TimeStamp = new DateTime(2018, 08, 17, 5, 0, 0)
            };

            // Arrange

            // Act
            var result = await _shareController.Post(hourRate);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
        }
       [Test]
        public async Task TheReturnGetModel()
        {
            string symbol = "REL";

            OkObjectResult result =await _shareController.Get(symbol) as OkObjectResult;
           
            Assert.NotNull(result.Value);
 
            //var model = result.v as List<HourlyShareRate>;
            //Assert.True(model.Count>0);
        }
        [Test]
        public async Task Get_LatestPrice()
        {
            string symbol = "REL";
            var result = await _shareController.GetLatestPrice(symbol);
            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult.Value);
        }
        [Test]
        public async Task PostUpdateLastPrice()
        {
            string symbol = "REL";
            var result=await _shareController.UpdateLastPrice(symbol);
            Assert.AreEqual(result, true);

        }

    }
}

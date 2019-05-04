using camera_trigger_api_core.Contexts;
using camera_trigger_api_core.Models;
using camera_trigger_api_core.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Services
{
    [TestClass]
    public class ReportsServiceTests
    {
        public ReportsService service;
        public Mock<ITriggerContext> ctx;

        [TestInitialize]
        public void Setup()
        {
            ctx = new Mock<ITriggerContext>();
            var triggers = new List<Trigger>()
            {
                new Trigger(){
                Id =1,
                CameraName = "Cam1",
                TimeStamp = DateTime.Now
                },
                new Trigger(){
                Id =2,
                CameraName = "Cam1",
                TimeStamp = DateTime.Now
                },
                new Trigger()
                {
                    Id=3,
                    CameraName = "Cam1",
                    TimeStamp = DateTime.Now.AddDays(-5)
                },
                new Trigger()
                {
                    Id = 4,
                    CameraName = "Cam1",
                    TimeStamp = DateTime.Now.AddDays(-30)
                },
                new Trigger()
                {
                    Id = 5,
                    CameraName = "Cam1",
                    TimeStamp = DateTime.Now.AddDays(-40)
                }
            };

            var mock = triggers.AsQueryable().BuildMockDbSet();
            ctx.Setup(x => x.Triggers).Returns(mock.Object);
            service = new ReportsService(ctx.Object);
        }

        [TestClass]
        public class WhenCallingFullReports : ReportsServiceTests
        {
            [TestMethod]
            public async Task ShouldReturnThreeResults()
            {
                var res = await service.GetFullReport();
                res.Count().Should().Be(3);
            }
        }

        [TestClass]
        public class WhenCallingWeeklyReports : ReportsServiceTests
        {
            [TestMethod]
            public async Task ShouldReturnTwoResults()
            {
                var res = await service.GetWeeklyReport();
                res.Count().Should().Be(2);
            }
        }

        [TestClass]
        public class WhenDbThrowsException : ReportsServiceTests
        {
            [TestInitialize]
            public void SetupFailingDb()
            {
                ctx.Setup(x => x.Triggers).Throws(new Exception());
            }

            [TestMethod]
            public async Task FullReportShouldReturnNull()
            {
                var res = await service.GetFullReport();
                res.Should().BeNull();
            }

            [TestMethod]
            public async Task WeeklyReportCallShouldReturnNull()
            {
                var res = await service.GetWeeklyReport();
                res.Should().BeNull();
            }
        }

        [TestClass]
        public class WhenDbIsEmpty : ReportsServiceTests
        {
            [TestInitialize]
            public void EmptyDb()
            {
                var triggers = new List<Trigger>()
                    .AsQueryable()
                    .BuildMockDbSet()
                    .Object;
                ctx.Setup(x => x.Triggers).Returns(triggers);
            }

            [TestMethod]
            public async Task WeekylyReportReturnsEmptyListAsync()
            {
                var res = await service.GetWeeklyReport();
                res.Count().Should().Be(0);
            }

            [TestMethod]
            public async Task FullReportReturnsEmptyListAsync()
            {
                var res = await service.GetFullReport();
                res.Count().Should().Be(0);
            }
        }
    }
}

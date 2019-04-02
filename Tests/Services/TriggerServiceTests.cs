using camera_trigger_api_core.Contexts;
using camera_trigger_api_core.DTOs;
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
    public class TriggerServiceTests
    {
        public TriggerService service;
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
                    TimeStamp = DateTime.Now.AddDays(-20)
                }
            };

            var mock = triggers.AsQueryable().BuildMockDbSet();
            ctx.Setup(x => x.Triggers).Returns(mock.Object);
            service = new TriggerService(ctx.Object);
        }

        [TestClass]
        public class PostTriggers : TriggerServiceTests
        {
            private TriggerDto dto;

            [TestInitialize]
            public void SetupDto()
            {
                dto = new TriggerDto()
                {
                    CameraName = "NewEntry",
                    TimeStamp = DateTime.Now
                };
            }

            [TestMethod]
            public async Task ShouldCallSaveAsync()
            {
                var result = await service.AddTriggerAsync(dto);
                ctx.Verify(x => x.SaveChangesAsync(), Times.Once);
            }

            [TestClass]
            public class WhenSavingFails : PostTriggers
            {
                [TestInitialize]
                public void SetupFail()
                {
                    ctx.Setup(x => x.SaveChangesAsync()).Returns(async () => -1);
                }

                [TestMethod]
                public async Task ShouldReturnNegativeOneAsync()
                {
                    var result = await service.AddTriggerAsync(dto);
                    result.Should().Be(-1);
                }
            }
        }

        [TestClass]
        public class FindTriggers : TriggerServiceTests
        {
            private const long goodId = 1;
            private const long badId = 9999;

            [TestMethod]
            public async Task FindAllReturnsAllAsync()
            {
                var res = await service.GetAllTriggersAsync();
                res.Count().Should().Be(4);
            }

            [TestMethod]
            public async Task FindByIdReturnsOneAsync()
            {
                var res = await service.FindByIdAsync(goodId);
                Assert.IsNotNull(res);

                res.CameraName.Should().Be("Cam1");
            }

            [TestMethod]
            public async Task NotFoundIdShouldReturnNullAsync()
            {
                var res = await service.FindByIdAsync(badId);
                Assert.IsNull(res);
            }
        }
    }
}

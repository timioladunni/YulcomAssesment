using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YulcomAssesment.API.Data;
using YulcomAssesment.API.Models.Response;
using YulcomAssesment.API.Models.Yulcom;
using YulcomAssesment.API.Response;
using YulcomAssesment.API.Services.Implementation;

namespace YucomAssesment.UnitTest.ServiceTest
{
    [TestFixture]
    internal class YulcomServiceTest
    {
        #region
        private DbContextOptions<YulcomAssesmentContext> dbContextOptions;
        private YulcomAssesmentContext dbContext;
        #endregion

        [OneTimeSetUp]
        public void Setup()
        {
            dbContextOptions = new DbContextOptionsBuilder<YulcomAssesmentContext>()
            .UseInMemoryDatabase(databaseName: "TestDB").Options;

            dbContext = new YulcomAssesmentContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            SeedDatabase();
        }

        [Test]
        public async Task CreateYulcomCommand_ReturnsASuccessResponse()
        {
            CreateYulcomCommand command = new()
            {
                CreateEntity = new YulcomModel
                {
                    Attack = "test",
                    Defense = "test",
                    SpDefense = "test",
                    Generation = "test",
                    SpAttack = "test",
                    Speed = "test",
                    HP = "test",
                    Legendary = "test",
                    Name = "test",
                    Total = "test",
                    Type1 = "test",
                    Type2 = "test"
                }
            };

            ILogger<YulcomHandlerService> logger = Substitute.For<ILogger<YulcomHandlerService>>();
            YulcomHandlerService handler = new(dbContext, logger);

            ApiResponse createCommandResponse = await handler.Handle(command, new CancellationToken());
            Assert.That(createCommandResponse.Message, Is.EqualTo(ResponseStatus.Successful.ToString()));
            Assert.That(createCommandResponse.Status, Is.EqualTo(true));
            Assert.That(createCommandResponse.Data, Is.EqualTo("Succesfully Created Entry"));

            GetAllQuery getAllQuery = new()
            {
                PageNumber = 1,
                PageSize = 20,
            };

            ApiResponse getAllQueryResponse = await handler.Handle(getAllQuery, new CancellationToken());
            Assert.That(getAllQueryResponse.Message, Is.EqualTo(ResponseStatus.Successful.ToString()));
            Assert.That(getAllQueryResponse.Status, Is.EqualTo(true));
            List<YulcomModel> data = (List<YulcomModel>)getAllQueryResponse.Data;
            Assert.That(data, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task UpdateYulcomCommand_ReturnsAnUpdatedData()
        {
            UpdateYulcomCommand command = new()
            {
                UpdateEntity = new YulcomModel
                {
                    Attack = "testing",
                    Defense = "test",
                    SpDefense = "test",
                    Generation = "test",
                    SpAttack = "test",
                    Speed = "test",
                    HP = "test",
                    Legendary = "test",
                    Name = "test",
                    Total = "test",
                    Type1 = "test",
                    Type2 = "test"
                },
                Id = 1
            };

            ILogger<YulcomHandlerService> logger = Substitute.For<ILogger<YulcomHandlerService>>();
            YulcomHandlerService handler = new(dbContext, logger);

            ApiResponse updateCommandResponse = await handler.Handle(command, new CancellationToken());

            Assert.That(updateCommandResponse.Message, Is.EqualTo(ResponseStatus.Successful.ToString()));
            Assert.That(updateCommandResponse.Status, Is.EqualTo(true));
            Assert.That(updateCommandResponse.Data, Is.EqualTo("Succesfully Updated Entry"));

            GetByIdQuery getByIdQuery = new()
            {
                Id = 1,
            };

            ApiResponse getByIdQueryResponse = await handler.Handle(getByIdQuery, new CancellationToken());
            Assert.That(getByIdQueryResponse.Message, Is.EqualTo(ResponseStatus.Successful.ToString()));
            Assert.That(getByIdQueryResponse.Status, Is.EqualTo(true));
            YulcomModel data = (YulcomModel)getByIdQueryResponse.Data;

            //update to a value when done
            Assert.That(data.Attack, Is.EqualTo("testing"));
        }


        private void SeedDatabase()
        {
            var data = new List<YulcomAssesmentData>()
            {
                new YulcomAssesmentData()
                {
                    Id = 1,
                    Attack = "test",
                    Defense = "test",
                    SpDefense = "test",
                    Generation = "test",
                    SpAttack = "test",
                    Speed = "test",
                    HP = "test",
                    Legendary = "test",
                    Name = "test",
                    Total = "test",
                    Type1 = "test",
                    Type2 = "test"
                }
            };
            dbContext.AddRange(data);
            dbContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp() => dbContext.Database.EnsureDeleted();
    }
}

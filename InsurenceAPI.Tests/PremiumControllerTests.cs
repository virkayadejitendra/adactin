using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml;
using InsurenceAPI.Controllers;
using InsurenceAPI.Data;
using InsurenceAPI.DTOs;
using InsurenceAPI.Entities;
using InsurenceAPI.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InsurenceAPI.Tests
{
    public class PremiumControllerTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly PremiumController _controller;

        public PremiumControllerTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            _context = new DataContext(options);
            _controller = new PremiumController(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CalculatePremium_ReturnsOkResult()
        {
            // Arrange
            var input = new PremiumInputDto
            {
                Age = 30,
                Occupation = "Doctor",
                SumInsured = 10000
            };

            // Create mock DbContext
            var mockDbContext = new Mock<DataContext>();
            // Sample data
            var sampleDataOccupation = new List<Occupation> { new Occupation { Id = 1, Name = "Doctor", RatingId = 1 } }.AsQueryable();
            var sampleDataRatingFactor = new List<RatingFactor> { new RatingFactor { Id = 1, Factor = 1, Description = "Professional" } }.AsQueryable();


            // Create mock DbSet
            var mockDbSet = new Mock<DbSet<Occupation>>();
        
            // Setup mock DbSet to return sample data
            mockDbSet.As<IQueryable<Occupation>>().Setup(m => m.Provider).Returns(sampleDataOccupation.Provider);
            mockDbSet.As<IQueryable<Occupation>>().Setup(m => m.Expression).Returns(sampleDataOccupation.Expression);
            mockDbSet.As<IQueryable<Occupation>>().Setup(m => m.ElementType).Returns(sampleDataOccupation.ElementType);
            mockDbSet.As<IQueryable<Occupation>>().Setup(m => m.GetEnumerator()).Returns(sampleDataOccupation.GetEnumerator());

            var mockDbSetRatingFactors = new Mock<DbSet<RatingFactor>>();
            mockDbSet.As<IQueryable<RatingFactor>>().Setup(m => m.Provider).Returns(sampleDataRatingFactor.Provider);
            mockDbSet.As<IQueryable<RatingFactor>>().Setup(m => m.Expression).Returns(sampleDataRatingFactor.Expression);
            mockDbSet.As<IQueryable<RatingFactor>>().Setup(m => m.ElementType).Returns(sampleDataRatingFactor.ElementType);
            mockDbSet.As<IQueryable<RatingFactor>>().Setup(m => m.GetEnumerator()).Returns(sampleDataRatingFactor.GetEnumerator());

            // Setup mock DbContext to return mock DbSet
            mockDbContext.Setup(m => m.Occupations).Returns(mockDbSet.Object);
            mockDbContext.Setup(m => m.RatingFactors).Returns(mockDbSetRatingFactors.Object);

            // Act
            var result = await _controller.CalculatePremium(input);

            // Assert
            //Assert.IsType<OkObjectResult>(result);
            var premiumResult = (PremiumResultDto)((OkObjectResult)result).Value;
            Assert.NotNull(premiumResult);
            Assert.Equal(350m, premiumResult.DeathPremium);
            Assert.Equal(8.10261724200513m, premiumResult.TpdPremium);
        }

        [Fact]
        public async Task CalculatePremium_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var input = new PremiumInputDto
            {
                Age = 30,
                Occupation = "Doctor",
                SumInsured = -10000 // Invalid value
            };
            _controller.ModelState.AddModelError(nameof(PremiumInputDto.SumInsured), "The SumInsured field must be greater than 0.");

            // Act
            var result = await _controller.CalculatePremium(input);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var response = (BadRequestObjectResult)result;
            Assert.IsType<SerializableError>(response.Value);
        }

        [Fact]
        public async Task CalculatePremium_ReturnsBadRequest_WhenOccupationNotFound()
        {
            // Arrange
            var input = new PremiumInputDto
            {
                Age = 30,
                Occupation = "Unknown",
                SumInsured = 10000
            };

            // Act
            var result = await _controller.CalculatePremium(input);

            // Assert
            Assert.IsType<ApiException>(result);
            var response = (ApiException)result;
            Assert.Equal("Mentioned occupation not found in database", response.Message);
        }


    }
}

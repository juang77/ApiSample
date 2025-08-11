using ApiSample.Controllers;
using ApiSample.Models;
using ApiSample.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiSample.Tests
{
    public class CountryControllerTests
    {
        private readonly Mock<ICountryService> _serviceMock;
        private readonly CountryController _controller;

        public CountryControllerTests()
        {
            _serviceMock = new Mock<ICountryService>();
            _controller = new CountryController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenFound()
        {
            var c = new Country(1, "X");
            _serviceMock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(c);

            var res = await _controller.GetById(1);
            var ok = res as OkObjectResult;
            ok.Should().NotBeNull();
            ((Country)ok!.Value!).Name.Should().Be("X");
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            var country = new Country(0, null!);
            _controller.ModelState.AddModelError("Name", "Required");

            var res = await _controller.Create(country);
            res.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}

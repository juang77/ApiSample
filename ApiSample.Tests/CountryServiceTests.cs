using ApiSample.Data;
using ApiSample.Models;
using ApiSample.Services;
using FluentAssertions;
using Moq;

namespace ApiSample.Tests
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryRepository> _repoMock;
        private readonly CountryService _service;

        public CountryServiceTests()
        {
            _repoMock = new Mock<ICountryRepository>();
            _service = new CountryService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsCountries()
        {
            var data = new List<Country> { new Country(1, "Col", null) };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(data);

            var res = await _service.GetAllAsync();
            res.Should().BeEquivalentTo(data);
        }

        [Fact]
        public async Task CreateAsync_ReturnsId()
        {
            var c = new Country(0, "N");
            _repoMock.Setup(r => r.CreateAsync(c)).ReturnsAsync(11);

            var id = await _service.CreateAsync(c);
            id.Should().Be(11);
        }
    }
}

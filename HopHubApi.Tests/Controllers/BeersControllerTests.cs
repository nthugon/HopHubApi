using NUnit.Framework;
using NSubstitute;
using HopHubApi.Controllers;
using HopHubApi.Services;
using System.Threading.Tasks;
using System;
using HopHubApi.Models;

namespace HopHubApi.Tests.Controllers
{
    [TestFixture]
    public class BeersControllerTests
    {
        private IBeerService _beerService;
        private BeersController _controller;
        private readonly long _beerId = 1;
        private readonly Beer _beer = new Beer
        {
            Name = "Beer Name",
            Brewery = "Test Brewery",
            Style = "Test Beer",
            Abv = 4.5m
        };

        [SetUp]
        public void Setup()
        {
            _beerService = Substitute.For<IBeerService>();
            _controller = new BeersController(_beerService);
        }

        [Test]
        public async Task GetAll_WhenCalled_CallsBeerServiceGetAllAsync()
        {
            await _controller.GetAllAsync();

            await _beerService.Received(1).GetAllAsync();
        }

        [Test]
        public async Task GetByIdAsync_WhenCalled_CallsBeerServiceGetByIdAsync()
        {
            await _controller.GetByIdAsync(_beerId);

            await _beerService.Received(1).GetByIdAsync(_beerId);
        }

        [Test]
        public async Task CreateAsync_WhenCalledWithValidBeer_CallsBeerServiceCreateAsync()
        {
            await _controller.CreateAsync(_beer);

            await _beerService.Received(1).CreateAsync(_beer);

        }

    }
}
using NUnit.Framework;
using NSubstitute;
using HopHubApi.Controllers;
using HopHubApi.Services;
using System.Threading.Tasks;
using System;

namespace HopHubApi.Tests.Controllers
{
    [TestFixture]
    public class BeersControllerTests
    {
        private IBeerService _beerService;
        private BeersController _controller;
        private readonly long _beerId = 1;

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

    }
}
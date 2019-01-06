using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NSubstitute;
using HopHubApi.Controllers;
using HopHubApi.Services;
using System.Threading.Tasks;

namespace HopHubApi.Tests.Controllers
{
    [TestFixture]
    public class BeersControllerTests
    {
        private IBeerService _beerService;
        private BeersController _controller;

        [SetUp]
        public void Setup()
        {
            _beerService = Substitute.For<IBeerService>();
            _controller = new BeersController(_beerService);
        }

        [Test]
        public async Task GetAll_WhenCalled_CallsBeerServiceGetAllAsync()
        {
            await _controller.GetAll();

            await _beerService.Received(1).GetAllAsync();
        }
       
    }
}
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using HopHubApi.Controllers;
using HopHubApi.Services;
using System.Threading.Tasks;
using System;
using HopHubApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;

namespace HopHubApi.Tests.Controllers
{
    [TestFixture]
    public class BeersControllerTests
    {
        private IBeerService _beerService;
        private BeersController _controller;
        private readonly Beer _beer = new Beer
        {
            BeerId = 1,
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
        public async Task GetAllAsync_WhenCalled_CallsBeerServiceGetAllAsync()
        {
            await _controller.GetAllAsync();

            await _beerService.Received(1).GetAllAsync();
        }

        [Test]
        public async Task GetAllWithReviewsAsync_WhenCalled_CallsBeerServiceGetAllWithReviewsAsync()
        {
            await _controller.GetAllWithReviewsAsync();

            await _beerService.Received(1).GetAllWithReviewsAsync();
        }

        [Test]
        public async Task GetByIdAsync_WhenCalled_CallsBeerServiceGetByIdAsync()
        {
            await _controller.GetByIdAsync(_beer.BeerId);

            await _beerService.Received(1).GetByIdAsync(_beer.BeerId);
        }

        [Test]
        public async Task GetByIdAsync_WhenCalledWithInvalidId_Returns404()
        {
            _beerService.GetByIdAsync(_beer.BeerId).Throws(new KeyNotFoundException());

            var response = await _controller.GetByIdAsync(_beer.BeerId);
            var statusCodeResult = response.Result as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task CreateAsync_WhenCalledWithValidBeer_CallsBeerServiceCreateAsync()
        {
            await _controller.CreateAsync(_beer);

            await _beerService.Received(1).CreateAsync(_beer);
        }

        [Test]
        public async Task CreateAsync_WhenBeerServiceThrows_Returns500()
        {
            _beerService.CreateAsync(_beer).Throws(new Exception("blah"));

            var response = await _controller.CreateAsync(_beer);
            var statusCodeResult = response.Result as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task UpdateAsync_WhenCalledWithValidBeer_CallsBeerServiceUpdateAsync()
        {
            await _controller.UpdateAsync(_beer.BeerId, _beer);

            await _beerService.Received(1).UpdateAsync(_beer.BeerId, _beer);
        }

        [Test]
        public async Task UpdateAsync_WhenCalledWithInvalidBeer_Returns404()
        {
            _beerService.UpdateAsync(_beer.BeerId, _beer).Throws(new KeyNotFoundException());

            var response = await _controller.UpdateAsync(_beer.BeerId, _beer);
            var statusCodeResult = response as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithValidId_CallsBeerServiceDeleteAsync()
        {
            await _controller.DeleteAsync(_beer.BeerId);

            await _beerService.Received(1).DeleteAsync(_beer.BeerId);
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithInvalidId_Returns404()
        {
            _beerService.DeleteAsync(_beer.BeerId).Throws(new KeyNotFoundException());

            var response = await _controller.DeleteAsync(_beer.BeerId);
            var statusCodeResult = response as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
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
using NSubstitute.Core.Arguments;
using Serilog;

namespace HopHubApi.Tests.Controllers
{
    [TestFixture]
    public class BeersControllerTests
    {
        private IBeerService _beerService;
        private ILogger _logger;
        private BeersController _controller;
        private BeerRequest _beerRequest;
        private Beer _beer;

        [SetUp]
        public void Setup()
        {
            _beerService = Substitute.For<IBeerService>();
            _logger = Substitute.For<ILogger>();
            _controller = new BeersController(_beerService, _logger);
            _beerRequest = new BeerRequest
            {
                Name = "Beer Name",
                Brewery = "Test Brewery",
                Style = "Test Beer",
                Abv = 4.5m
            };
            _beer = new Beer
            {
                BeerId = 1,
                Name = _beerRequest.Name,
                Brewery = _beerRequest.Brewery,
                Style = _beerRequest.Style,
                Abv = _beerRequest.Abv
            };
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_CallsBeerServiceGetAllAsync()
        {
            await _controller.GetAllAsync();

            await _beerService.Received(1).GetAllAsync();
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_LogsInformation()
        {
            await _controller.GetAllAsync();

            _logger.Received(1).Information(Arg.Any<string>());
        }

        [Test]
        public async Task GetAllAsync_WhenFailed_Returns500()
        {
            _beerService.GetAllAsync().Throws(new Exception());

            var response = await _controller.GetAllAsync();
            var statusCodeResult = response.Result as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task GetAllAsync_WhenFailed_LogsError()
        {
            _beerService.GetAllAsync().Throws(new Exception());

            await _controller.GetAllAsync();

            _logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [Test]
        public async Task GetAllWithReviewsAsync_WhenCalled_CallsBeerServiceGetAllWithReviewsAsync()
        {
            await _controller.GetAllWithReviewsAsync();

            await _beerService.Received(1).GetAllWithReviewsAsync();
        }

        [Test]
        public async Task GetAllWithReviewsAsync_WhenCalled_LogsInformation()
        {
            await _controller.GetAllWithReviewsAsync();

            _logger.Received(1).Information(Arg.Any<string>());
        }

        [Test]
        public async Task GetAllWithReviewsAsync_WhenFailed_Returns500()
        {
            _beerService.GetAllWithReviewsAsync().Throws(new Exception());

            var response = await _controller.GetAllWithReviewsAsync();
            var statusCodeResult = response.Result as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task GetAllWithReviewsAsync_WhenFailed_LogsError()
        {
            _beerService.GetAllWithReviewsAsync().Throws(new Exception());

            await _controller.GetAllWithReviewsAsync();

            _logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [Test]
        public async Task GetByIdAsync_WhenCalled_CallsBeerServiceGetByIdAsync()
        {
            await _controller.GetByIdAsync(_beer.BeerId);

            await _beerService.Received(1).GetByIdAsync(_beer.BeerId);
        }

        [Test]
        public async Task GetByIdAsync_WhenCalled_LogsInformation()
        {
            await _controller.GetByIdAsync(_beer.BeerId);

            _logger.Received(1).Information(Arg.Any<string>());
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
        public async Task GetByIdAsync_WhenCalledWithInvalidId_LogsWarning()
        {
            _beerService.GetByIdAsync(_beer.BeerId).Throws(new KeyNotFoundException());

            await _controller.GetByIdAsync(_beer.BeerId);

            _logger.Received(1).Warning(Arg.Any<KeyNotFoundException>(), Arg.Any<string>());
        }

        [Test]
        public async Task GetByIdAsync_WhenFailed_Returns500()
        {
            _beerService.GetByIdAsync(_beer.BeerId).Throws(new Exception());

            var response = await _controller.GetByIdAsync(_beer.BeerId);
            var statusCodeResult = response.Result as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task GetByIdAsync_WhenFailed_LogsError()
        {
            _beerService.GetByIdAsync(_beer.BeerId).Throws(new Exception());

            await _controller.GetByIdAsync(_beer.BeerId);

            _logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [Test]
        public async Task CreateAsync_WhenCalledWithValidBeer_CallsBeerServiceCreateAsync()
        {
            await _controller.CreateAsync(_beerRequest);

            await _beerService.Received(1).CreateAsync(Arg.Is<Beer>(x => 
                x.Name == _beer.Name &&
                x.Style == _beer.Style &&
                x.Brewery == _beer.Brewery &&
                x.Abv == _beer.Abv));
        }

        [Test]
        public async Task CreateAsync_WhenCalledWithValidBeer_LogsInformation()
        {
            _beerService.CreateAsync(Arg.Any<Beer>()).Returns(_beer);

            await _controller.CreateAsync(_beerRequest);

            _logger.Received(2).Information(Arg.Any<string>());
        }

        [Test]
        public async Task CreateAsync_WhenFailed_Returns500()
        {
            _beerService.CreateAsync(_beer).Throws(new Exception());

            var response = await _controller.CreateAsync(_beerRequest);
            var statusCodeResult = response.Result as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task CreateAsync_WhenFailed_LogsError()
        {
            _beerService.CreateAsync(_beer).Throws(new Exception());

            await _controller.CreateAsync(_beerRequest);

            _logger.Received(1).Error(Arg.Any<Exception>() ,Arg.Any<string>());
        }

        [Test]
        public async Task UpdateAsync_WhenCalledWithValidBeer_CallsBeerServiceUpdateAsync()
        {
            await _controller.UpdateAsync(_beer.BeerId, _beerRequest);

            await _beerService.Received(1).UpdateAsync(_beer.BeerId, _beerRequest);
        }

        [Test]
        public async Task UpdateAsync_WhenCalledWithValidBeer_LogInformation()
        {
            await _controller.UpdateAsync(_beer.BeerId, _beerRequest);

            _logger.Received(2).Information(Arg.Any<string>());
        }

        [Test]
        public async Task UpdateAsync_WhenCalledWithInvalidBeer_Returns404()
        {
            _beerService.UpdateAsync(_beer.BeerId, _beerRequest).Throws(new KeyNotFoundException());

            var response = await _controller.UpdateAsync(_beer.BeerId, _beerRequest);
            var statusCodeResult = response as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task UpdateAsync_WhenCalledWithInvalidBeer_LogsWarning()
        {
            _beerService.UpdateAsync(_beer.BeerId, _beerRequest).Throws(new KeyNotFoundException());

            await _controller.UpdateAsync(_beer.BeerId, _beerRequest);

            _logger.Received(1).Warning(Arg.Any<KeyNotFoundException>(), Arg.Any<string>());
        }

        [Test]
        public async Task UpdateAsync_WhenFailed_Returns500()
        {
            _beerService.UpdateAsync(_beer.BeerId, _beerRequest).Throws(new Exception());

            var response = await _controller.UpdateAsync(_beer.BeerId, _beerRequest);
            var statusCodeResult = response as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task UpdateAsync_WhenFailed_LogsError()
        {
            _beerService.UpdateAsync(_beer.BeerId, _beerRequest).Throws(new Exception());

            await _controller.UpdateAsync(_beer.BeerId, _beerRequest);

            _logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithValidId_CallsBeerServiceDeleteAsync()
        {
            await _controller.DeleteAsync(_beer.BeerId);

            await _beerService.Received(1).DeleteAsync(_beer.BeerId);
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithValidId_LogsInformation()
        {
            await _controller.DeleteAsync(_beer.BeerId);

            _logger.Received(2).Information(Arg.Any<string>());
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithInvalidId_Returns404()
        {
            _beerService.DeleteAsync(_beer.BeerId).Throws(new KeyNotFoundException());

            var response = await _controller.DeleteAsync(_beer.BeerId);
            var statusCodeResult = response as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteAsync_WhenCalledWithInvalidId_LogsWarning()
        {
            _beerService.DeleteAsync(_beer.BeerId).Throws(new KeyNotFoundException());

            await _controller.DeleteAsync(_beer.BeerId);

            _logger.Received(1).Warning(Arg.Any<Exception>() ,Arg.Any<string>());
        }

        [Test]
        public async Task DeleteAsync_WhenFailed_Returns500()
        {
            _beerService.DeleteAsync(_beer.BeerId).Throws(new Exception());

            var response = await _controller.DeleteAsync(_beer.BeerId);
            var statusCodeResult = response as StatusCodeResult;

            Assert.AreEqual((HttpStatusCode)statusCodeResult.StatusCode, HttpStatusCode.InternalServerError);
        }

        [Test]
        public async Task DelateAsync_WhenFailed_LogsError()
        {
            _beerService.DeleteAsync(_beer.BeerId).Throws(new Exception());

            await _controller.DeleteAsync(_beer.BeerId);

            _logger.Received(1).Error(Arg.Any<Exception>(), Arg.Any<string>());
        }
    }
}
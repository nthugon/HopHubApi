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
    public class ReviewsControllerTests
    {
        private IReviewService _reviewService;
        private ReviewsController _controller;

        [SetUp]
        public void Setup()
        {
            _reviewService = Substitute.For<IReviewService>();
            _controller = new ReviewsController(_reviewService);
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_CallsReviewServiceGetAllAsync()
        {
            await _controller.GetAllAsync();

            await _reviewService.Received(1).GetAllAsync();
        }
    }
}

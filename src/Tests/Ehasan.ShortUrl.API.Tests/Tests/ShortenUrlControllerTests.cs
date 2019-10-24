using Ehasan.ShortUrl.API.Controllers;
using Ehasan.ShortUrl.Core.Business_Interface;
using Ehasan.ShortUrl.Core.Entities;
using Ehasan.ShortUrl.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Ehasan.ShortUrl.API.Tests.Tests
{
    public class ShortenUrlControllerTests
    {
        public Mock<IShortenUrlService> shortenUrlService;

        private ShortenUrlController sut;
        public ShortenUrlControllerTests()
        {
            this.shortenUrlService = new Mock<IShortenUrlService>();
            sut = new ShortenUrlController(this.shortenUrlService.Object);

        }
        [Fact]
        public void post_should_return_shorten_url_if_input_url_is_valid_long_url()
        {
            var alterUrl = "https://shortenurl.com/85249ecd-943c-4e53-8a03-d5a412b1d09b";
            var shortenUrl = new ShortenUrlInputModel() { Url = "https://www.linkedin.com/in/ehasanulhoque/" };
            this.shortenUrlService.Setup(x => x.AlterUrl(It.IsAny<ShortenUrlInputModel>())).Returns(alterUrl);
            var result = sut.Post(shortenUrl) as OkObjectResult;
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(alterUrl);
        }
        [Fact]
        public void post_should_return_long_url_if_input_url_is_valid_short_url()
        {
            var alterUrl = "https://www.linkedin.com/in/ehasanulhoque/";
            var shortenUrl = new ShortenUrlInputModel() { Url = "https://shortenurl.com/85249ecd-943c-4e53-8a03-d5a412b1d09b" };
            this.shortenUrlService.Setup(x => x.AlterUrl(It.IsAny<ShortenUrlInputModel>())).Returns(alterUrl);
            var result = sut.Post(shortenUrl) as OkObjectResult;
            result.StatusCode.Should().Be(201);
            result.Value.Should().Be(alterUrl);
        }
        [Fact]
        public void post_should_return_not_found_if_input_short_url_is_not_registered()
        {
            var shortenUrl = new ShortenUrlInputModel() { Url = "https://shortenurl.com/85249ecd-943c-4e53-8a03-d5a412b1d09b" };
            this.shortenUrlService.Setup(x => x.AlterUrl(It.IsAny<ShortenUrlInputModel>())).Returns(string.Empty);
            var result = sut.Post(shortenUrl) as NotFoundResult;
            result.StatusCode.Should().Be(404);
        }
        [Fact]
        public void post_should_return_result_as_bad_request_if_input_url_is_empty()
        {
            var shortenUrl = new ShortenUrlInputModel() { Url = "https://shortenurl.com/85249ecd-943c-4e53-8a03-d5a412b1d09b" };
            this.shortenUrlService.Setup(x => x.AlterUrl(It.IsAny<ShortenUrlInputModel>())).Throws(new Exception("Url should not be null or empty"));
            var result = sut.Post(shortenUrl) as BadRequestObjectResult;
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Url should not be null or empty");
        }
        [Fact]
        public void post_should_return_not_found_if_input_url_is_invalid()
        {
            var shortenUrl = new ShortenUrlInputModel() { Url = "https://shortenurl.com/85249ecd-943c-4e53-8a03-d5a412b1d09b" };
            this.shortenUrlService.Setup(x => x.AlterUrl(It.IsAny<ShortenUrlInputModel>())).Throws(new Exception("Url is invalid"));
            var result = sut.Post(shortenUrl) as BadRequestObjectResult;
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Url is invalid");
        }
    }
}

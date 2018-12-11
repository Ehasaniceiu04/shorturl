using Ehasan.ShortUrl.Business.Command;
using Ehasan.ShortUrl.Core.Business_Interface;
using Ehasan.ShortUrl.Core.Entities;
using Ehasan.ShortUrl.Core.Model;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace Ehasan.ShortUrl.Business.Tests.Tests.Command
{
    public class ShortUrlCommandTests
    {
        public Mock<IShortenUrlServiceQuery> shortenUrlServiceQuery;
        private Mock<IOptions<ShortenUrlSetting>> shortenUrlSetting;
        private IUrlCommand sut;
        public ShortUrlCommandTests()
        {
            this.shortenUrlServiceQuery = new Mock<IShortenUrlServiceQuery>();
            this.shortenUrlSetting = new Mock<IOptions<ShortenUrlSetting>>();
        }
        [Fact]
        public void ConvertUrl_should_convert_short_to_long_by_getting_from_db_if_already_stored_in_db()
        {
            var shortenUrlSetting = new ShortenUrlSetting();
            shortenUrlSetting.DomainName = "http://shortenurl.my/";
            var shortenUrl = new ShortenUrl() { ShortUrl = "testUrl", LongUrl= "https://www.linkedin.com/in/ehasanulhoque/" };
            this.shortenUrlSetting.Setup(x => x.Value).Returns(shortenUrlSetting);
            sut = new ShortUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(shortenUrl);
            var result = sut.ConvertUrl(shortenUrlSetting.DomainName+shortenUrl.ShortUrl);
            result.Should().Contain(shortenUrl.LongUrl);
        }
        [Fact]
        public void ConvertUrl_should_convert_short_to_long_for_url_without_http_by_getting_from_db_if_already_stored_in_db()
        {
            var shortenUrlSetting = new ShortenUrlSetting();
            shortenUrlSetting.DomainName = "http://shortenurl.my/";
            var shortenUrl = new ShortenUrl() { ShortUrl = "testUrl", LongUrl = "https://www.linkedin.com/in/ehasanulhoque/" };
            this.shortenUrlSetting.Setup(x => x.Value).Returns(shortenUrlSetting);
            sut = new ShortUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(shortenUrl);
            var result = sut.ConvertUrl("shortenurl.my/" + shortenUrl.ShortUrl);
            result.Should().Contain(shortenUrl.LongUrl);
        }
        [Fact]
        public void ConvertUrl_should_convert_empty_longurl_if_not_stored_in_db()
        {
            var shortenUrlSetting = new ShortenUrlSetting();
            shortenUrlSetting.DomainName = "http://shortenurl.my/";
            var shortenUrl = new ShortenUrl() { ShortUrl = "testUrl", LongUrl = "https://www.linkedin.com/in/ehasanulhoque/" };
            this.shortenUrlSetting.Setup(x => x.Value).Returns(shortenUrlSetting);
            sut = new ShortUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByShortUrl(It.IsAny<string>())).Returns(It.IsAny<ShortenUrl>());
            var result = sut.ConvertUrl(shortenUrlSetting.DomainName + shortenUrl.ShortUrl);
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ConvertUrl_should_not_convert_long_to_short_and_found_exception_if_appsettings_key_absence()
        {
            //var shortenUrlSetting = new ShortenUrlSetting();
            var shortenUrl = new ShortenUrl() { ShortUrl = "testurl" };
            this.shortenUrlSetting.Setup(x => x.Value).Returns(It.IsAny<ShortenUrlSetting>());
            sut = new ShortUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByLongUrl(It.IsAny<string>())).Returns(shortenUrl);
            var exp = Assert.Throws<NullReferenceException>(() => sut.ConvertUrl("testurl"));
        }
    }
}

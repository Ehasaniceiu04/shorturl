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
    public class LongUrlCommandTest
    {
        public Mock<IShortenUrlServiceQuery> shortenUrlServiceQuery;
        private Mock<IOptions<ShortenUrlSetting>> shortenUrlSetting;
        private IUrlCommand sut;
        public LongUrlCommandTest()
        {
            this.shortenUrlServiceQuery = new Mock<IShortenUrlServiceQuery>();
            this.shortenUrlSetting = new Mock<IOptions<ShortenUrlSetting>>();
           
        }
        [Fact]
        public void ConvertUrl_should_convert_long_to_short_by_getting_last_part_of_short_url_from_db_if_already_stored_in_db()
        {
            var shortenUrlSetting = new ShortenUrlSetting();
            shortenUrlSetting.DomainName = "http://shortenurl.my/";
            var shortenUrl = new ShortenUrl() { ShortUrl = "testurl" };
            this.shortenUrlSetting.Setup(x => x.Value).Returns(shortenUrlSetting);
            sut = new LongUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByLongUrl(It.IsAny<string>())).Returns(shortenUrl);
            var result = sut.ConvertUrl("testurl");
            result.Should().Contain(shortenUrl.ShortUrl);
        }
        [Fact]
        public void ConvertUrl_should_convert_long_to_short_by_making_last_part_of_short_url_and_stored_into_db_if_not_stored_in_db()
        {
            var shortenUrlSetting = new ShortenUrlSetting();
            shortenUrlSetting.DomainName = "http://shortenurl.my/";
            this.shortenUrlSetting.Setup(x => x.Value).Returns(shortenUrlSetting);
            sut = new LongUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByLongUrl(It.IsAny<string>())).Returns(It.IsAny<ShortenUrl>());
            var result = sut.ConvertUrl("testurl");
            result.Length.Should().BeGreaterThan(shortenUrlSetting.DomainName.Length);
        }
        [Fact]
        public void ConvertUrl_should_not_convert_long_to_short_and_found_exception_if_appsettings_key_absence()
        {
            //var shortenUrlSetting = new ShortenUrlSetting();
            var shortenUrl = new ShortenUrl() { ShortUrl = "testurl" };
            this.shortenUrlSetting.Setup(x => x.Value).Returns(It.IsAny<ShortenUrlSetting>());
            sut = new LongUrlCommand(this.shortenUrlServiceQuery.Object, this.shortenUrlSetting.Object);
            this.shortenUrlServiceQuery.Setup(x => x.GetByLongUrl(It.IsAny<string>())).Returns(shortenUrl);
            var exp = Assert.Throws<NullReferenceException>(() => sut.ConvertUrl("testurl"));
        }
    }
}

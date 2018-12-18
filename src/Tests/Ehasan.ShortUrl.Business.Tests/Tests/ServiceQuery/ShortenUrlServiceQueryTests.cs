using Ehasan.Core.Repository_Interfaces;
using Ehasan.ShortUrl.Core.Business_Interface;
using Moq;
using Xunit;

namespace Ehasan.ShortUrl.Business.Tests.Tests.ServiceQuery
{
    public class ShortenUrlServiceQueryTests
    {
        public Mock<IUnitOfWork> unitOfWork;
        private IShortenUrlServiceQuery sut;
        public ShortenUrlServiceQueryTests()
        {
            this.unitOfWork = new Mock<IUnitOfWork>();
        }
        [Fact]
        public void GetAll_should_return_all_data_from_db()
        {
            //var shortenUrls = new List<ShortenUrl>();
            //shortenUrls.Add(new ShortenUrl { ShortUrl = "ehasan" });
            //shortenUrls.Add(new ShortenUrl { ShortUrl = "ehasan" });
            //this.unitOfWork.Setup(x => x.Repository<ShortenUrl>().Get().ToList()).Returns(shortenUrls);
            //sut = new ShortenUrlServiceQuery(unitOfWork.Object);
            //sut.GetAll().Should().BeEquivalentTo(2);
        }
    }
}

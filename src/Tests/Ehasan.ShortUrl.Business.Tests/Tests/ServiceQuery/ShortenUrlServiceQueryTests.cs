using BUZ.Authorization.Core.Repository_Interfaces;
using Ehasan.ShortUrl.Core.Business_Interface;
using Ehasan.ShortUrl.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Ehasan.ShortUrl.Business.ServiceQuery;
using FluentAssertions;

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

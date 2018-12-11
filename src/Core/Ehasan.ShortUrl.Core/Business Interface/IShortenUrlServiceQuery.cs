using Ehasan.ShortUrl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.ShortUrl.Core.Business_Interface
{
    public interface IShortenUrlServiceQuery
    {
        ShortenUrl GetByShortUrl(string url);
        ShortenUrl GetByLongUrl(string url);
        ShortenUrl GetById(int id);
        IEnumerable<ShortenUrl> GetAll();
        void Add(ShortenUrl shortenUrl);
    }
}

using Ehasan.ShortUrl.Core.Entities;
using Ehasan.ShortUrl.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.ShortUrl.Core.Business_Interface
{
    public interface IShortenUrlService
    {
        string ShortenUrl(ShortenUrlInputModel shortenUrlInputModel);
    }
}

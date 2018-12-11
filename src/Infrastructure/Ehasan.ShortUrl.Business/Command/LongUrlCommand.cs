using Ehasan.ShortUrl.Core.Business_Interface;
using Ehasan.ShortUrl.Core.Entities;
using Ehasan.ShortUrl.Core.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.ShortUrl.Business.Command
{
    public class LongUrlCommand : IUrlCommand
    {
        private readonly IShortenUrlServiceQuery shortenUrlServiceQuery;
        private readonly ShortenUrlSetting shortenUrlSetting;
        public LongUrlCommand(IShortenUrlServiceQuery shortenUrlServiceQuery,IOptions<ShortenUrlSetting> shortenUrlSettingOptions)
        {
            this.shortenUrlServiceQuery = shortenUrlServiceQuery;
            this.shortenUrlSetting = shortenUrlSettingOptions.Value;
        }
        private string ShortUrl => Guid.NewGuid().ToString();
        private Func<string, string, string> FullShortUrl = (urlRoot, shortUrl) =>
        {
            return $"{urlRoot}{shortUrl}";
        };
        string IUrlCommand.ConvertUrl(string url)
        {
            try
            {
                var shortenUrl = this.shortenUrlServiceQuery.GetByLongUrl(url);
                if (shortenUrl == null)
                {
                    shortenUrl = new ShortenUrl()
                    {
                        LongUrl = url,
                        ShortUrl = ShortUrl,
                        CreatedDate = DateTime.Now
                    };
                    this.shortenUrlServiceQuery.Add(shortenUrl);
                }
                return FullShortUrl(this.shortenUrlSetting.DomainName, shortenUrl.ShortUrl);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        
    }
}

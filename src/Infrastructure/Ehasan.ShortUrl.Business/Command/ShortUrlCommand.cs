using Ehasan.ShortUrl.Core.Business_Interface;
using Ehasan.ShortUrl.Core.Model;
using Microsoft.Extensions.Options;

namespace Ehasan.ShortUrl.Business.Command
{
    public class ShortUrlCommand : IUrlCommand
    {
        private readonly IShortenUrlServiceQuery shortenUrlServiceQuery;
        private readonly ShortenUrlSetting shortenUrlSetting;

        public ShortUrlCommand(IShortenUrlServiceQuery shortenUrlServiceQuery, IOptions<ShortenUrlSetting> shortenUrlSettingOptions)
        {
            this.shortenUrlServiceQuery = shortenUrlServiceQuery;
            this.shortenUrlSetting = shortenUrlSettingOptions.Value;
        }
        string IUrlCommand.ConvertUrl(string url)
        {
            var shortUrl = string.Empty;
            var longUrl = string.Empty;
            try
            {
                var domainNamehttp = this.shortenUrlSetting.DomainName.Split("//")[0] + "//";
                if (url.StartsWith(domainNamehttp))
                {
                    shortUrl = url.Substring(this.shortenUrlSetting.DomainName.Length);
                }
                else
                {
                    shortUrl = url.Substring(this.shortenUrlSetting.DomainName.Length - domainNamehttp.Length);
                }
                var shortenUrl = this.shortenUrlServiceQuery.GetByShortUrl(shortUrl);
                if (shortenUrl != null)
                {
                    longUrl = shortenUrl.LongUrl;
                }
                return longUrl;
            }
            catch (System.Exception)
            {

                throw;
            }
           
        }
    }
}

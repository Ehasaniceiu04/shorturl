using BUZ.Authorization.Core.Repository_Interfaces;
using Ehasan.ShortUrl.Core.Business_Interface;
using Ehasan.ShortUrl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;
using Ehasan.ShortUrl.Core.Model;
using Microsoft.Extensions.Options;
using Autofac.Core;
using Autofac;
using Ehasan.ShortUrl.Business.Command;

namespace Ehasan.ShortUrl.Business
{
    public sealed class ShortenUrlService : IShortenUrlService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ShortenUrlSetting shortenUrlSetting;
        private readonly IUrlCommandFactory urlCommandFactory;

        public ShortenUrlService(IUnitOfWork unitOfWork, IOptions<ShortenUrlSetting> shortenUrlSettingOption, IUrlCommandFactory urlCommandFactory)
        {
            this.unitOfWork = unitOfWork;
            this.shortenUrlSetting = shortenUrlSettingOption.Value;
            this.urlCommandFactory = urlCommandFactory;
        }

        private Func<string, bool> _isValidUrl = (url) =>
         {
             var pattern = @"^((http|https|ftp|www):\/\/)?([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)(\.)([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)";
             var reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
             var result = reg.IsMatch(url);
             return result;
         };
        private bool IsLongUrl(string url)
        {
            return !url.Contains(this.shortenUrlSetting.DomainName) &&
               !_isUrlStratWithShortUrlRootPath(url, this.shortenUrlSetting.DomainName);
        }
        private string GetServiceName(string url)
        {
            if (IsLongUrl(url))
                return "long";
            else
                return "short";
        }
  

        private Func<string, string, bool> _isUrlStratWithShortUrlRootPath = (url, shortUrlRoot) =>
         {
           return url.StartsWith(shortUrlRoot) || url.StartsWith(shortUrlRoot.Split("//")[1]);
         };


        string IShortenUrlService.AlterUrl(ShortenUrlInputModel shortenUrlInputModel)
        {
            try
            {
                if (shortenUrlInputModel == null || string.IsNullOrWhiteSpace(shortenUrlInputModel.Url))
                    throw new Exception("Url should not be null or empty");
                if (!_isValidUrl(shortenUrlInputModel.Url))
                    throw new Exception("Url is invalid");

                return this.urlCommandFactory.CreateCommand(GetServiceName(shortenUrlInputModel.Url)).ConvertUrl(shortenUrlInputModel.Url);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
  
}

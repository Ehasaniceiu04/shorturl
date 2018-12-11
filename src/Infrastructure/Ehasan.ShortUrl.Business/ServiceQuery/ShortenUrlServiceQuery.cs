using Ehasan.ShortUrl.Core.Business_Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Ehasan.ShortUrl.Core.Entities;
using System.Linq.Expressions;
using BUZ.Authorization.Core.Repository_Interfaces;
using System.Linq;

namespace Ehasan.ShortUrl.Business.ServiceQuery
{
    public class ShortenUrlServiceQuery : IShortenUrlServiceQuery
    {
        private readonly IUnitOfWork unitOfWork;
        public ShortenUrlServiceQuery(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        void IShortenUrlServiceQuery.Add(ShortenUrl shortenUrl)
        {
            try
            {
                unitOfWork.Repository<ShortenUrl>().Add(shortenUrl);
                unitOfWork.SaveChanges();
            }
            catch
            {
                throw;
            }
            
        }

        IEnumerable<ShortenUrl> IShortenUrlServiceQuery.GetAll()
        {
            try
            {
                var shortenUrls = this.unitOfWork.Repository<ShortenUrl>().Get().ToList();
                return shortenUrls;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        ShortenUrl IShortenUrlServiceQuery.GetById(int id)
        {
            try
            {
                Expression<Func<ShortenUrl, bool>> funcPred = p => p.Id == id;
                var shortenUrl = this.unitOfWork.Repository<ShortenUrl>().Get().Where(funcPred).FirstOrDefault();
                return shortenUrl;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        ShortenUrl IShortenUrlServiceQuery.GetByLongUrl(string url)
        {
            try
            {
                Expression<Func<ShortenUrl, bool>> funcPred = p => p.LongUrl == url;
                var shortenUrl = this.unitOfWork.Repository<ShortenUrl>().Get().Where(funcPred).FirstOrDefault();
                return shortenUrl;
            }
            catch (Exception exp)
            {

                throw;
            }
           
        }

        ShortenUrl IShortenUrlServiceQuery.GetByShortUrl(string url)
        {
            try
            {
                Expression<Func<ShortenUrl, bool>> funcPred = p => p.ShortUrl == url;
                var shortenUrl = this.unitOfWork.Repository<ShortenUrl>().Get().Where(funcPred).FirstOrDefault();
                return shortenUrl;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}

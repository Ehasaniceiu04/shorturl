using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ehasan.ShortUrl.Core.Model;
using Ehasan.ShortUrl.Core.Business_Interface;
using System.ComponentModel;

namespace Ehasan.ShortUrl.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ShortenUrl")]
    public class ShortenUrlController : Controller
    {
        private readonly IShortenUrlService shortenUrlService;

        public ShortenUrlController(IShortenUrlService shortenUrlService)
        {
            this.shortenUrlService = shortenUrlService;
        }
        
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Post([FromBody]ShortenUrlInputModel shortenUrlInputModel)
        {
            var url = string.Empty;
            try
            {
                url=this.shortenUrlService.AlterUrl(shortenUrlInputModel);
                if(string.IsNullOrWhiteSpace(url))
                {
                    return NotFound();
                }
                return Ok(url);
            }
            catch (Exception exp)
            {
                //TODO: Log exception here
                return BadRequest(exp.Message);
                
            }
           
            
        }
        
      
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.ShortUrl.Business.Command
{
    public interface IUrlCommand
    {
        //If input url is short
        string ConvertUrl(string url);
    }
}

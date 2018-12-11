using Ehasan.ShortUrl.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.ShortUrl.DataRepositories.Context
{
    public class ShortUrlDbContext: DbContext
    {
        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options):base(options)
        {
        }
        public virtual DbSet<ShortenUrl> ShortenUrl { get; set; }
       
    }
}

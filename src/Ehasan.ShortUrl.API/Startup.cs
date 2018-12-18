using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ehasan.ShortUrl.Core.Model;
using Ehasan.ShortUrl.DependencyResolver;
using Ehasan.ShortUrl.DataRepositories.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Ehasan.ShortUrl.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShortUrlDbContext>(o=>o.UseSqlServer(Configuration.GetConnectionString("ShortenUrlConnectionStringAws")));
           // services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMvc();
            var builder = new ContainerBuilder();

            services.Configure<ShortenUrlSetting>(Configuration.GetSection("ShortenUrlSetting"));

            RegisterSwagger(services);

           
            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            builder = services.CreateAutofacBusinessContainer(builder);
            builder = services.CreateAutofacRepositoryContainer(builder, Configuration.GetConnectionString("ShortenUrlConnectionStringAws"));

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            var builder2 = new ContainerBuilder();
            builder2.RegisterInstance<IContainer>(this.ApplicationContainer);
            builder2.Update(this.ApplicationContainer);

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Url Shortening API V1");
                c.RoutePrefix = string.Empty;
            });


            app.UseMvc();
        }
        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = $"{Configuration.GetSection("Application:FriendlyName").Value} API",
                    Description = Configuration.GetSection("Application:Description").Value,
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Ehasanul Hoque",
                        Email = "ehice04@gmail.com",
                        Url = "https://www.linkedin.com/in/ehasanulhoque/"
                    }
                });
            });
   
        }

    }
}

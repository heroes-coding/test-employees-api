using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Employees.API.Mappers;
using Employees.API.Helpers;
using Microsoft.AspNetCore.Http;

namespace Employees.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      var mappingConfig = new MapperConfiguration(mc =>
        mc.AddProfile(new MappingProfile())
      );
      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      services.AddCors();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler(builder =>
        {
          builder.Run(async context =>
          {

            var error = context.Features.Get<IExceptionHandlerFeature>();

            // The is statement below is not working for me, so I used the full string method...
            // Every exception that is deliberately thrown is an ArgumentException, which is really
            // a user input error, so a bad request is returned.  Otherwise, an internal server error is sent back.
            context.Response.StatusCode = error.Error.GetType().FullName == "System.ArgumentException" ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError;
            if (error != null)
            {
              context.Response.AddApplicationError(error.Error.Message);
              await context.Response.WriteAsync(error.Error.Message);
            }
          });
        });
      }

      // app.UseHttpsRedirection();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseMvc();
    }
  }
}

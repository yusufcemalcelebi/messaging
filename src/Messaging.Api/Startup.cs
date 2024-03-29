using System;
using System.IO;
using System.Linq;
using AutoMapper;
using FluentValidation.AspNetCore;
using Messaging.Api.Helpers;
using Messaging.Api.Models.Settings;
using Messaging.Core.Abstractions.Service;
using Messaging.Data;
using Messaging.Service;
using Messaging.Service.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Messaging.Api
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
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperContainer());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<MessagingDbContext>(db =>
                db.UseSqlServer(Configuration.GetConnectionString("MessagingDB")));

            // DI
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessagingService, MessagingService>();
            services.AddScoped<IBlockingService, BlockingService>();
            services.AddScoped<ISpamDetectionService, SpamDetectionService>();

            // Settings
            services.Configure<AuthenticationSettings>(Configuration.GetSection("AuthenticationSettings"));
            services.Configure<SpamDetectionSettings>(Configuration.GetSection("SpamDetectionSettings"));

            services.AddCors();
            services.AddControllers();

            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddControllers().ConfigureApiBehaviorOptions(op => op.InvalidModelStateResponseFactory = c =>
            {
                var result = new
                {
                    ErrorMessages = c.ModelState.Values.Where(x => x.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList()
                };
                return new BadRequestObjectResult(result);
            });

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Messaging API",
                    Version = "v1",
                    Description = "Messaging Application with basic Auth functionality"
                });
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messaing API V1");
                c.RoutePrefix = "";
                c.DocExpansion(DocExpansion.None);
            });
        }

        private static string GetXmlCommentsPath()
        {
            return Path.Combine(AppContext.BaseDirectory, "Messaging.Api.xml");
        }
    }
}

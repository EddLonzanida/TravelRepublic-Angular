using Eml.ClassFactory.Contracts;
using Eml.ControllerBase;
using Eml.Mef;
using Eml.MefDependencyResolver.Api;
using TravelRepublic.Api.Configurations;
using TravelRepublic.Api.Helpers;
using TravelRepublic.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;

namespace TravelRepublic.Api
{
    public class Startup
    {
        private const string SWAGGER_DOC_VERSION = "v1";

        private const string API_NAME = "TravelRepublic";

        private const string LAUNCH_URL = "docs";

        public static IConfiguration Configuration { get; private set; }

        public static ILoggerFactory LoggerFactory { get; private set; }

        public static IClassFactory ClassFactory { get; private set; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SWAGGER_DOC_VERSION,
                    new Info
                    {
                        Title = API_NAME,
                        Version = SWAGGER_DOC_VERSION,
                        Contact = new Contact { Name = "Eddie Lonzanida", Email = "EddieLonzanida@hotmail.com" },
                        Description = "SOA Solution using .Net Core 2.2. Featuring Angular7, Etags, RateLimits, IoC/DI using MEF, EFCore, DataMigrations, SwaggerUI, XUnit , DataRepository & Mediator pattern, NLog, HealthChecks and more.."
                    });
               
                c.OperationFilter<SwashbuckleSummaryOperationFilter>();
                c.DocumentFilter<LowercaseDocumentFilter>();
                c.DocumentFilter<SortOperationDocumentFilter>();
            });

            ClassFactory = services.AddMef(() =>
            {
                // Register instances as shared.
                var instanceRegistrations = new List<Func<ContainerConfiguration, ExportDescriptorProvider>>
                {
                    r => r.WithInstance(LoggerFactory),
                    r => r.WithInstance(Configuration)
                };

                // Create Mef container
                return Bootstrapper.Init(API_NAME, instanceRegistrations);
            });

			services.AddHealthChecks().AddDbContextCheck<TravelRepublicDb>();

            // var rateLimits = ClassFactory.GetExport<RateLimitsConfig>();

            // services.AddMemoryCache();
            // services.Configure<IpRateLimitOptions>(options =>
            // {
            //     options.GeneralRules = rateLimits.Value;
            // });
            // services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            // services.AddHttpCacheHeaders(
            //     expirationModelOptions =>
            //     {
            //         expirationModelOptions.MaxAge = 600;
            //         expirationModelOptions.SharedMaxAge = 300;
            //     },
            //     validationModelOptions =>
            //     {
            //        validationModelOptions.AddMustRevalidate = true;
            //        validationModelOptions.AddProxyRevalidate = true;
            //    });
            // services.AddResponseCaching();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/ready");
            app.ConfigureExceptionHandler(LoggerFactory);

            // app.UseIpRateLimiting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SWAGGER_DOC_VERSION}/swagger.json", API_NAME);
                c.RoutePrefix = LAUNCH_URL;
                c.EnableFilter();
                c.DocExpansion(DocExpansion.None);
            });

            // Check the port in TravelRepublic.Spa -> Properties -> launchSettings.json and update TravelRepublic.Api -> appsettings.json -> WhiteList entry
            var whiteListConfig = new WhiteListConfig(Configuration);

            app.UseCors(builder => builder.WithOrigins(whiteListConfig.Value.ToArray())
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            // app.UseResponseCaching();
            // app.UseHttpCacheHeaders(); 
            app.UseMvc();
        }
    }
}

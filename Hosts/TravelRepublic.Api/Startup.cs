using Eml.ClassFactory.Contracts;
using Eml.ControllerBase;
using Eml.Mef;
using Eml.MefDependencyResolver.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelRepublic.Api.Helpers;
using TravelRepublic.Infrastructure;

namespace TravelRepublic.Api
{
    public class Startup
    {
        private const string SWAGGER_DOC_VERSION = "v1";

        private const string LAUNCH_URL = "docs";

        public static ILoggerFactory LoggerFactory { get; private set; }

        public static IClassFactory ClassFactory { get; private set; }

        public Startup(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext = actionContext as ActionExecutingContext;

                    if (actionExecutingContext?.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });
            services.AddMvc(options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();
                    options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; //optional
				});

            var assemblyVersion = typeof(Startup).Assembly.GetName().Version.ToString();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SWAGGER_DOC_VERSION,
                    new Info
                    {
                        Title = Constants.ApplicationId,
                        Version = $"Build: {assemblyVersion}",
                        Contact = new Contact { Name = "Eddie Lonzanida", Email = "EddieLonzanida@hotmail.com" },
                        Description = "SOA Solution using .Net Core 2.2. Featuring Angular8, Etags, RateLimits, IoC/DI using MEF, EFCore, DataMigrations, SwaggerUI, XUnit , DataRepository & Mediator pattern, NLog, HealthChecks and more.."
                    });
               
                c.OperationFilter<SwashbuckleSummaryOperationFilter>();
                c.DocumentFilter<LowercaseDocumentFilter>();
                c.DocumentFilter<SortOperationDocumentFilter>();
                c.DescribeAllParametersInCamelCase();
                c.DescribeStringEnumsInCamelCase();
            });

            ClassFactory = services.AddMef(() =>
            {
				// Any changes here should also be reflected in the integration test fixtures.
                // Register instances as shared.
                var instanceRegistrations = new List<Func<ContainerConfiguration, ExportDescriptorProvider>>
                {
                    r => r.WithInstance(LoggerFactory),
                    r => r.WithInstance(ApplicationSettings.Configuration)
                };

                // Create Mef container
                return Bootstrapper.Init(Constants.ApplicationId, instanceRegistrations);
            });

			//services.AddHealthChecks().AddDbContextCheck<TravelRepublicDb>();

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
                c.SwaggerEndpoint($"/swagger/{SWAGGER_DOC_VERSION}/swagger.json", Constants.ApplicationId);
                c.RoutePrefix = LAUNCH_URL;
                c.EnableFilter();
                c.DocExpansion(DocExpansion.None);
            });

#if DEBUG
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
#else
            // Ports can be modified in TravelRepublic.Spa -> Properties -> launchSettings.json and update TravelRepublic.Api -> appsettings.json -> WhiteList entry
            var whiteListConfig = ApplicationSettings.Config.WhiteList;
           
			app.UseCors(builder => builder.WithOrigins(whiteListConfig.ToArray()));
#endif

            // app.UseResponseCaching();
            // app.UseHttpCacheHeaders(); 
            app.UseMvc();
        }
    }
}

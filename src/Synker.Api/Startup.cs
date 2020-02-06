using AutoMapper;
using BeatPulse.UI;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using Synker.Api.Filters;
using Synker.Application.DataSources.Queries.GetDatasource;
using Synker.Application.Infrastructure.AutoMapper;
using Synker.Application.Infrastructure.FluentValidationBehaviors;
using Synker.Application.Interfaces;
using Synker.Application.Playlists.Commands;
using Synker.Infrastructure;
using Synker.Persistence;
using System;
using System.Reflection;
using Xtream.Client;

namespace Synker.Api
{
    public class Startup
    {
        private volatile static string assemblyVersion = typeof(Startup).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string AssemblyVersion { get => assemblyVersion; set => assemblyVersion = value; }

        // This method gets called by the runtime. Use this method   to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(x=>x.EnableEndpointRouting=false)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(UpdatePlaylistValidator)));
            services.AddControllers(o => o.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                //options.ClientErrorMapping[404].Link =
                //    "https://httpstatuses.com/404";
            });

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            //Some problems with the SynkerDBContext migration
            //services.AddBeatPulseUI();

            services.AddMediatR(typeof(GetDataSourceQuery).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<IDataSourceReaderFactory, DefaultDataSourceReaderFactory>();
            services.AddTransient<IXtreamClient, XtreamClient>();
            services.AddTransient<IHttpClientFactory, DefaultHttpClientFactory>();
            
            services.AddDbContext<ISynkerDbContext, SynkerDbContext>(options =>
            {
                var config = Configuration.GetConnectionString("PlDatabase");
                options.UseNpgsql(Configuration.GetConnectionString("PlDatabase"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(SynkerDbContext).GetTypeInfo().Assembly.GetName().Name);
                    //Configuring Connection Resiliency:
                    sqlOptions.
                    EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
                });

                //// Changing default behavior when client evaluation occurs to throw.
                //// Default in EFCore would be to log warning when client evaluation is done.
                //options.ConfigureWarnings(warnings => warnings.Throw(
                //RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddApiVersioning(o =>
            {
                //o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Synker API",
                        Description = $"Synchronize playlists API {AssemblyVersion}",
                        TermsOfService = "None",
                        Contact = new OpenApiContact { Name = "Synker", Email = "contact@synker.ovh", Url = "https://www.github.com/fazzani/synker2" },
                        License = new OpenApiLicense { Name = "Use under MIT", Url = "" },
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseMvc()
               .UseApiVersioning()
               .UseMvcWithDefaultRoute();

            app.UseRouting();

            app.UseBeatPulseUI();
            app.UseStaticFiles();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                //settings.DocumentPath = "/api/specification.json";
            });

        }
    }
}

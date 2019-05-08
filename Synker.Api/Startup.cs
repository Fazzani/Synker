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
using Synker.Application.DataSources.Commands.Create;
using Synker.Application.DataSources.Queries.GetDatasource;
using Synker.Application.Infrastructure.AutoMapper;
using Synker.Application.Infrastructure.FluentValidationBehaviors;
using Synker.Application.Interfaces;
using Synker.Infrastructure;
using Synker.Persistence;
using System;
using System.Reflection;
using Xtream.Client;

namespace Synker.Api
{
    public class Startup
    {
        public volatile static string AssemblyVersion = typeof(Startup).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method   to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateDataSourceValidator>());

            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            //Some problems with the SynkerDBContext migration
            services.AddBeatPulseUI();

            services.AddMediatR(typeof(GetDataSourceQuery).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<IDataSourceReaderFactory, DefaultDataSourceReaderFactory>();
            services.AddTransient<IXtreamClient, XtreamClient>();
            
            services.AddDbContext<ISynkerDbContext, SynkerDbContext>(options =>
            {
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
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info = new SwaggerInfo
                    {
                        Version = "v1",
                        Title = "Synker API",
                        Description = $"Synchronize playlists API {AssemblyVersion}",
                        TermsOfService = "None",
                        Contact = new SwaggerContact { Name = "Synker", Email = "contact@synker.ovh", Url = "https://www.github.com/fazzani/synker2" },
                        License = new SwaggerLicense { Name = "Use under MIT", Url = "" },
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseBeatPulseUI();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                //settings.DocumentPath = "/api/specification.json";
            });

            app.UseMvc();
        }
    }
}

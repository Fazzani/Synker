﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BeatPulse.UI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Synker.Application.DataSources.Queries;
using Synker.Application.Interfaces;
using Synker.Persistence;

namespace Synker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method   to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Some problems with the SynkerDBContext migration
            services.AddBeatPulseUI();

            services.AddMediatR(typeof(GetDataSourceQueryHandler).GetTypeInfo().Assembly);

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseBeatPulseUI();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseMvc();
        }
    }
}

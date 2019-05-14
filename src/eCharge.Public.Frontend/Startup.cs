#region [ COPYRIGHT ]

// <copyright file="Startup.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-03-15</date>
// <summary></summary>

#endregion

#region [ References ]

#endregion

namespace ECharge.Public.Frontend
{
    #region [ References ]

    using System;
    using System.Globalization;
    using System.IO.Compression;
    using System.Linq;
    using ECharge.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.AspNetCore.SpaServices.Webpack;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Startup
    {
        #region [ Constructor ]

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public IConfiguration Configuration { get; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        // ReSharper disable once UnusedMember.Global
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddControllersAsServices()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options => { options.EnableForHttps = true; });
            services.AddOptions();
            services.Configure<ApplicationSettings>(this.Configuration.GetSection("Application"));
        }

        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ConfigFile = "./build/webpack.dev.conf.js"
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseResponseCompression();
            CultureInfo[] supportedCultures = new[]
            {
                "de-DE",
                "en-US"
            }.Select(code => new CultureInfo(code)).ToArray();
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    if (string.IsNullOrEmpty(context.Context.Request.Query["v"]))
                    {
                        return;
                    }

                    context.Context.Response.Headers.Add("cache-control", new[]
                    {
                        "public,max-age=31536000"
                    });
                    context.Context.Response.Headers.Add("Expires",
                        new[]
                        {
                            DateTime.UtcNow.AddYears(1).ToString("R")
                        });
                }
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new
                {
                    controller = "Home",
                    action = "Index"
                });
            });
        }

        #endregion [ Public methods ]
    }
}
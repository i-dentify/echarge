#region [ COPYRIGHT ]

// <copyright file="ApplicationBuilderExtensions.cs" company="i-dentify Software Development">
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

namespace ECharge.Web.Core.Extensions
{
    #region [ References ]

    using System.Globalization;
    using System.Linq;
    using ECharge.Authentication;
    using ECharge.Web.Core.Middlewares;
    using ECharge.Web.Core.Models;
    using EventFlow.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.SwaggerUI;

    #endregion

    public static class ApplicationBuilderExtensions
    {
        #region [ Public methods ]

        public static void UseMicroservice(this IApplicationBuilder app, IHostingEnvironment env,
            MicroserviceOptions microserviceOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
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
            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Authentication-Hash")
                .AllowCredentials());
            app.UseResponseCompression();
            JsonExceptionMiddleware jsonExceptionMiddleware =
                new JsonExceptionMiddleware(app.ApplicationServices.GetRequiredService<IHostingEnvironment>());
            app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = jsonExceptionMiddleware.Invoke });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/1.0/swagger.json", microserviceOptions.Swagger.Title);
                options.DocExpansion(DocExpansion.List);
            });

            app.Use(async (context, next) =>
            {
                CurrentUserProvider currentUserProvider = app.ApplicationServices.GetService<CurrentUserProvider>();

                if (currentUserProvider != null)
                {
                    context.Response.Headers.Add("X-Authentication-Hash", currentUserProvider.User.Name.ToSha256());
                }

                await next();
            });
            app.UseMvcWithDefaultRoute();
        }

        #endregion [ Public methods ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="ServiceCollectionExtensions.cs" company="i-dentify Software Development">
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

    using System;
    using System.Collections.Generic;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using ECharge.Authentication;
    using ECharge.Core.Rendering;
    using ECharge.IoC.Modules;
    using ECharge.Messaging.Clients;
    using ECharge.Messaging.Plugins.Email.SendGrid;
    using ECharge.Models;
    using ECharge.Web.Core.Models;
    using EventFlow;
    using EventFlow.Autofac.Extensions;
    using EventFlow.EntityFramework;
    using EventFlow.EntityFramework.Extensions;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Swashbuckle.AspNetCore.Swagger;

    #endregion

    public static class ServiceCollectionExtensions
    {
        #region [ Public methods ]

        public static IServiceProvider AddMicroservice(this IServiceCollection services, IConfiguration configuration,
            MicroserviceOptions microserviceOptions)
        {
            services.AddOptions();
            services.Configure<SendGridOptions>(configuration.GetSection("SendGrid"));
            services.Configure<ApplicationSettings>(configuration.GetSection("Application"));
            services.Configure<DatabaseOptions>(options =>
                options.ConnectionString = microserviceOptions.Database.ConnectionString);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = microserviceOptions.Authentication.Authority;
                options.Audience = microserviceOptions.Authentication.Audience;
            });
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddControllersAsServices()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddFluentValidation();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(microserviceOptions.Swagger.Version, new Info
                {
                    Title = microserviceOptions.Swagger.Title,
                    Version = microserviceOptions.Swagger.Version
                });
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer", new string[] { }
                    }
                });
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddCors();
            services.AddResponseCompression(options => { options.EnableForHttps = true; });
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(microserviceOptions)
                .AsSelf()
                .SingleInstance();
            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CurrentUserProvider>()
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.Register(context =>
                {
                    CurrentUserProvider provider = context.Resolve<CurrentUserProvider>();
                    return provider.User;
                })
                .AsSelf()
                .InstancePerDependency();
            builder.RegisterType<RazorViewRenderer>()
                .As<IViewRenderer>()
                .InstancePerLifetimeScope();
            builder.Register(context =>
                {
                    IOptions<SendGridOptions> options = context.Resolve<IOptions<SendGridOptions>>();
                    return new SendGridClient(options.Value);
                })
                .As<IEmailClient>()
                .InstancePerLifetimeScope();
            builder.RegisterModule(new Mapping(microserviceOptions.DomainAssemblies.Select(Assembly.Load)));
            builder.RegisterModule(new Validation(microserviceOptions.DomainAssemblies.Select(Assembly.Load)));
            builder.Populate(services);
            IContainer container = EventFlowOptions.New
                .UseAutofacContainerBuilder(builder)
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .ConfigureDomain(microserviceOptions.DomainRegistration)
                .CreateContainer();
            return container.Resolve<IServiceProvider>();
        }

        #endregion [ Public methods ]
    }
}
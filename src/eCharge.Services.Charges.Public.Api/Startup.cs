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

namespace ECharge.Services.Charges.Public.Api
{
    #region [ References ]

    using System;
    using System.Linq;
    using ECharge.Models;
    using ECharge.Services.Charges.Domain.Data.Persistence.Context;
    using ECharge.Services.Charges.Domain.Extensions;
    using ECharge.Web.Core.Extensions;
    using ECharge.Web.Core.Models;
    using ECharge.Web.Core.Models.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;

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

        #region [ Private properties ]

        private IConfiguration Configuration { get; }

        #endregion [ Private properties ]

        #region [ Public methods ]

        // ReSharper disable once UnusedMember.Global
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(this.Configuration["Database:ConnectionStrings:Charges"]));

            return services.AddMicroservice(this.Configuration, new MicroserviceOptions
            {
                Authentication = new OAuthOptions
                {
                    Authority = $"https://{this.Configuration["Auth0:Authority"]}",
                    Audience = this.Configuration["Auth0:Audience"]
                },
                Database = new DatabaseOptions
                {
                    ConnectionString = this.Configuration["Database:ConnectionStrings:Charges"]
                },
                DomainAssemblies = DependencyContext.Default.GetDefaultAssemblyNames()
                    .Where(assembly => assembly.Name.Equals("ECharge.Services.Charges.Domain")).ToList(),
                Swagger = new SwaggerOptions
                {
                    Version = "1.0",
                    Title = "eCharge Services: Charges"
                },
                DomainRegistration = options => options.ConfigureDomain()
            });
        }

        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MicroserviceOptions microserviceOptions)
        {
            app.UseMicroservice(env, microserviceOptions);
        }

        #endregion [ Public methods ]
    }
}
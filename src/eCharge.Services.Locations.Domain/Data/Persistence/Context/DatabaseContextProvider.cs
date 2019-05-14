#region [ COPYRIGHT ]

// <copyright file="DatabaseContextProvider.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-12-30</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Locations.Domain.Data.Persistence.Context
{
    #region [ References ]

    using ECharge.Models;
    using EventFlow.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    #endregion

    public class DatabaseContextProvider : IDbContextProvider<ApplicationContext>
    {
        #region [ Private attributes ]

        private readonly DbContextOptions<ApplicationContext> dbContextOptions;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public DatabaseContextProvider(IOptions<DatabaseOptions> options)
        {
            this.dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql(options.Value.ConnectionString)
                .Options;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public ApplicationContext CreateContext()
        {
            return new ApplicationContext(this.dbContextOptions);
        }

        #endregion [ Public methods ]
    }
}
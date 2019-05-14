#region [ COPYRIGHT ]

// <copyright file="CarByName.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Queries.Handlers
{
    #region [ References ]

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Authentication;
    using ECharge.Services.Cars.Domain.Cars.ViewModels;
    using ECharge.Services.Cars.Domain.Data.Persistence.Context;
    using EventFlow.EntityFramework;
    using EventFlow.Extensions;
    using EventFlow.Queries;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class CarByName : IQueryHandler<Queries.CarByName, Car>
    {
        #region [ Constructor ]

        public CarByName(IMapper mapper, CurrentUserProvider currentUserProvider,
            IDbContextProvider<ApplicationContext> databaseContextProvider)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.databaseContextProvider = databaseContextProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task<Car> ExecuteQueryAsync(Queries.CarByName query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query?.Name) ||
                string.IsNullOrWhiteSpace(this.currentUserProvider?.User?.Name))
            {
                return null;
            }

            using (ApplicationContext context = this.databaseContextProvider.CreateContext())
            {
                ReadModels.Car model = await context.Cars
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        entity => entity.Name.Equals(query.Name, StringComparison.InvariantCultureIgnoreCase) &&
                                  entity.Owner.Equals(this.currentUserProvider.User.Name.ToSha256(),
                                      StringComparison.InvariantCultureIgnoreCase), cancellationToken)
                    .ConfigureAwait(false);
                return model == null ? null : this.mapper.Map<Car>(model);
            }
        }

        #endregion [ Public methods ]

        #region [ Private attributes ]

        private readonly IMapper mapper;
        private readonly CurrentUserProvider currentUserProvider;
        private readonly IDbContextProvider<ApplicationContext> databaseContextProvider;

        #endregion [ Private attributes ]
    }
}
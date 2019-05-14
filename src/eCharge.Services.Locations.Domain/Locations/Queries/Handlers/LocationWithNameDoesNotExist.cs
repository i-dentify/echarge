#region [ COPYRIGHT ]

// <copyright file="LocationWithNameDoesNotExist.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Queries.Handlers
{
    #region [ References ]

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Authentication;
    using ECharge.Services.Locations.Domain.Data.Persistence.Context;
    using ECharge.Services.Locations.Domain.Locations.ReadModels;
    using EventFlow.EntityFramework;
    using EventFlow.Extensions;
    using EventFlow.Queries;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class LocationWithNameDoesNotExist : IQueryHandler<Queries.LocationWithNameDoesNotExist, bool>
    {
        #region [ Constructor ]

        public LocationWithNameDoesNotExist(CurrentUserProvider currentUserProvider,
            IDbContextProvider<ApplicationContext> databaseContextProvider)
        {
            this.currentUserProvider = currentUserProvider;
            this.databaseContextProvider = databaseContextProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task<bool> ExecuteQueryAsync(Queries.LocationWithNameDoesNotExist query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query?.Name) ||
                string.IsNullOrWhiteSpace(this.currentUserProvider?.User?.Name))
            {
                return false;
            }

            using (ApplicationContext context = this.databaseContextProvider.CreateContext())
            {
                ExpressionStarter<Location> predicate = PredicateBuilder.New<Location>(entity =>
                    entity.Owner.Equals(this.currentUserProvider.User.Name.ToSha256(),
                        StringComparison.InvariantCultureIgnoreCase) &&
                    entity.Name.Equals(query.Name, StringComparison.InvariantCultureIgnoreCase));

                if (!string.IsNullOrWhiteSpace(query.ExceptId))
                {
                    predicate = predicate.And(entity =>
                        !entity.Id.Equals(query.ExceptId, StringComparison.InvariantCultureIgnoreCase));
                }

                return !await context.Locations.AnyAsync(predicate, cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion [ Public methods ]

        #region [ Private attributes ]

        private readonly CurrentUserProvider currentUserProvider;
        private readonly IDbContextProvider<ApplicationContext> databaseContextProvider;

        #endregion [ Private attributes ]
    }
}
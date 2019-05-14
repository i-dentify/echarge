#region [ COPYRIGHT ]

// <copyright file="InvitationForEmailDoesNotExist.cs" company="i-dentify Software Development">
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
    using EventFlow.EntityFramework;
    using EventFlow.Queries;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class InvitationForEmailDoesNotExist : IQueryHandler<Queries.InvitationForEmailDoesNotExist, bool>
    {
        #region [ Constructor ]

        public InvitationForEmailDoesNotExist(CurrentUserProvider currentUserProvider,
            IDbContextProvider<ApplicationContext> databaseContextProvider)
        {
            this.currentUserProvider = currentUserProvider;
            this.databaseContextProvider = databaseContextProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task<bool> ExecuteQueryAsync(Queries.InvitationForEmailDoesNotExist query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query?.Location) || string.IsNullOrWhiteSpace(query?.Email) ||
                string.IsNullOrWhiteSpace(this.currentUserProvider?.User?.Name))
            {
                return false;
            }

            using (ApplicationContext context = this.databaseContextProvider.CreateContext())
            {
                return !await context.LocationInvitations
                    .AnyAsync(
                        entity => entity.Email.Equals(query.Email, StringComparison.InvariantCultureIgnoreCase) &&
                                  entity.LocationId.Equals(query.Location, StringComparison.InvariantCultureIgnoreCase),
                        cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        #endregion [ Public methods ]

        #region [ Private attributes ]

        private readonly CurrentUserProvider currentUserProvider;
        private readonly IDbContextProvider<ApplicationContext> databaseContextProvider;

        #endregion [ Private attributes ]
    }
}
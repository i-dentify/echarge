#region [ COPYRIGHT ]

// <copyright file="OpenInvitationExists.cs" company="i-dentify Software Development">
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
    using ECharge.Services.Locations.Domain.Data.Persistence.Context;
    using EventFlow.EntityFramework;
    using EventFlow.Queries;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class OpenInvitationExists : IQueryHandler<Queries.OpenInvitationExists, bool>
    {
        #region [ Private attributes ]

        private readonly IDbContextProvider<ApplicationContext> databaseContextProvider;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public OpenInvitationExists(IDbContextProvider<ApplicationContext> databaseContextProvider)
        {
            this.databaseContextProvider = databaseContextProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task<bool> ExecuteQueryAsync(Queries.OpenInvitationExists query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query?.Invitation))
            {
                return false;
            }

            using (ApplicationContext context = this.databaseContextProvider.CreateContext())
            {
                return await context.LocationInvitations
                    .AnyAsync(
                        entity => entity.Id.Equals(query.Invitation, StringComparison.InvariantCultureIgnoreCase) &&
                                  !entity.Accepted, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        #endregion [ Public methods ]
    }
}
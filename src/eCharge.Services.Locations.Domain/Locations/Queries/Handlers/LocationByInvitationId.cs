#region [ COPYRIGHT ]

// <copyright file="LocationByInvitationId.cs" company="i-dentify Software Development">
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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Services.Locations.Domain.Data.Persistence.Context;
    using ECharge.Services.Locations.Domain.Locations.ViewModels;
    using EventFlow.EntityFramework;
    using EventFlow.Queries;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class LocationByInvitationId : IQueryHandler<Queries.LocationByInvitationId, Location>
    {
        #region [ Constructor ]

        public LocationByInvitationId(IMapper mapper, IDbContextProvider<ApplicationContext> databaseContextProvider)
        {
            this.mapper = mapper;
            this.databaseContextProvider = databaseContextProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task<Location> ExecuteQueryAsync(Queries.LocationByInvitationId query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query?.Id))
            {
                return null;
            }

            using (ApplicationContext context = this.databaseContextProvider.CreateContext())
            {
                ReadModels.Location model = await context.Locations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        entity => entity.Invitations.Any(invitation =>
                            invitation.Id.Equals(query.Id, StringComparison.InvariantCultureIgnoreCase) &&
                            !invitation.Accepted), cancellationToken)
                    .ConfigureAwait(false);
                return model == null ? null : this.mapper.Map<Location>(model);
            }
        }

        #endregion [ Public methods ]

        #region [ Private attributes ]

        private readonly IMapper mapper;
        private readonly IDbContextProvider<ApplicationContext> databaseContextProvider;

        #endregion [ Private attributes ]
    }
}
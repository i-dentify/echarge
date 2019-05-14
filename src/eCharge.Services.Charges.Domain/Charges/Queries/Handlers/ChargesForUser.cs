#region [ COPYRIGHT ]

// <copyright file="ChargesForUser.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Queries.Handlers
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Authentication;
    using ECharge.Services.Charges.Domain.Charges.ViewModels;
    using ECharge.Services.Charges.Domain.Data.Persistence.Context;
    using EventFlow.EntityFramework;
    using EventFlow.Extensions;
    using EventFlow.Queries;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class ChargesForUser : IQueryHandler<Queries.ChargesForUser, IReadOnlyCollection<Charge>>
    {
        #region [ Constructor ]

        public ChargesForUser(IMapper mapper, CurrentUserProvider currentUserProvider,
            IDbContextProvider<ApplicationContext> databaseContextProvider)
        {
            this.mapper = mapper;
            this.currentUserProvider = currentUserProvider;
            this.databaseContextProvider = databaseContextProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task<IReadOnlyCollection<Charge>> ExecuteQueryAsync(Queries.ChargesForUser query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(this.currentUserProvider?.User?.Name))
            {
                return new ReadOnlyCollection<Charge>(new List<Charge>());
            }

            using (ApplicationContext context = this.databaseContextProvider.CreateContext())
            {
                return new ReadOnlyCollection<Charge>(this.mapper.Map<List<Charge>>(await context.Charges
                    .AsNoTracking()
                    .Where(entity => entity.Owner.Equals(this.currentUserProvider.User.Name.ToSha256(),
                        StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(entity => entity.Date)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)));
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
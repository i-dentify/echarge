#region [ COPYRIGHT ]

// <copyright file="DeleteLocation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Commands.Handlers
{
    #region [ References ]

    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Authentication;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class DeleteLocation : CommandHandler<Location, LocationId, Commands.DeleteLocation>
    {
        #region [ Private attributes ]

        private readonly CurrentUserProvider currentUserProvider;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public DeleteLocation(CurrentUserProvider currentUserProvider)
        {
            this.currentUserProvider = currentUserProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public override Task ExecuteAsync(Location aggregate, Commands.DeleteLocation command,
            CancellationToken cancellationToken)
        {
            aggregate.Delete(new User(this.currentUserProvider.User.Name));
            return Task.FromResult(0);
        }

        #endregion [ Public methods ]
    }
}
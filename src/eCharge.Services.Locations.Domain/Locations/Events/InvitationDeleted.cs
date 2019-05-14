#region [ COPYRIGHT ]

// <copyright file="InvitationDeleted.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Events
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("InvitationDeleted", 1)]
    public class InvitationDeleted : AggregateEvent<Location, LocationId>
    {
        #region [ Constructor ]

        public InvitationDeleted(InvitationId invitationId)
        {
            this.InvitationId = invitationId;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the invitation id.
        /// </summary>
        public InvitationId InvitationId { get; }

        #endregion [ Public properties ]
    }
}
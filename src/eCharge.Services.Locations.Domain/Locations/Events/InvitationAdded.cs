#region [ COPYRIGHT ]

// <copyright file="InvitationAdded.cs" company="i-dentify Software Development">
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

    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("InvitationAdded", 1)]
    public class InvitationAdded : AggregateEvent<Location, LocationId>
    {
        #region [ Constructor ]

        public InvitationAdded(InvitationId invitationId, Email email)
        {
            this.InvitationId = invitationId;
            this.Email = email;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the invitation id.
        /// </summary>
        public InvitationId InvitationId { get; }

        /// <summary>
        ///     Gets the email address of the invitation recipient.
        /// </summary>
        public Email Email { get; }

        #endregion [ Public properties ]
    }
}
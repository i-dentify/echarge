#region [ COPYRIGHT ]

// <copyright file="Invitations.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.ReadModels.Locators
{
    #region [ References ]

    using System.Collections.Generic;
    using ECharge.Services.Locations.Domain.Locations.Events;
    using EventFlow.Aggregates;
    using EventFlow.ReadStores;

    #endregion

    public class Invitations : IReadModelLocator
    {
        #region [ Public methods ]

        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            IAggregateEvent aggregateEvent = domainEvent.GetAggregateEvent();

            switch (aggregateEvent)
            {
                case InvitationAdded @event:
                    yield return @event.InvitationId.Value;
                    break;
                case InvitationDeleted @event:
                    yield return @event.InvitationId.Value;
                    break;
                case InvitationAccepted @event:
                    yield return @event.InvitationId.Value;
                    break;
                case InvitationRejected @event:
                    yield return @event.InvitationId.Value;
                    break;
            }
        }

        #endregion [ Public methods ]
    }
}
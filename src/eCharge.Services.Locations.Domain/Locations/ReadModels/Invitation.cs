#region [ COPYRIGHT ]

// <copyright file="Invitation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.ReadModels
{
    #region [ References ]

    using System.ComponentModel.DataAnnotations;
    using ECharge.Services.Locations.Domain.Locations.Events;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.ReadStores;

    #endregion

    public class Invitation : IReadModel,
        IAmReadModelFor<Aggregates.Location, LocationId, InvitationAdded>,
        IAmReadModelFor<Aggregates.Location, LocationId, InvitationDeleted>,
        IAmReadModelFor<Aggregates.Location, LocationId, InvitationAccepted>,
        IAmReadModelFor<Aggregates.Location, LocationId, InvitationRejected>
    {
        #region [ Public methods ]

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, InvitationAdded> @event)
        {
            this.LocationId = @event.AggregateIdentity.Value;
            this.Email = @event.AggregateEvent.Email.Value;
            this.Accepted = false;
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, InvitationDeleted> @event)
        {
            context.MarkForDeletion();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, InvitationAccepted> @event)
        {
            this.Accepted = true;
            this.User = @event.AggregateEvent.User.Value.ToSha256();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, InvitationRejected> @event)
        {
            this.Accepted = false;
            this.User = @event.AggregateEvent.User.Value.ToSha256();
        }

        #endregion [ Public methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the invitation id.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the location id.
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        ///     Gets or sets the email address of the invitation recipient.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the invited user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the user has accepted the invitation.
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        ///     Gets or sets the related location.
        /// </summary>
        public virtual Location Location { get; set; }

        #endregion [ Public properties ]
    }
}
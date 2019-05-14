#region [ COPYRIGHT ]

// <copyright file="Location.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Aggregates
{
    #region [ References ]

    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Domain.Aggregates;
    using ECharge.Domain.Specifications;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.Events;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.Snapshots;
    using EventFlow.Snapshots.Strategies;

    #endregion

    public class Location : SnapshotAggregateRoot<Location, LocationId, Snapshots.Location>,
        IOwnedAggregate<LocationId>,
        IApply<LocationAdded>,
        IApply<LocationEdited>,
        IApply<LocationDeleted>,
        IApply<InvitationAdded>,
        IApply<InvitationDeleted>,
        IApply<InvitationAccepted>,
        IApply<InvitationRejected>
    {
        #region [ Private attributes ]

        private const int SnapshotEveryVersion = 10;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public Location(LocationId id) : base(id, SnapshotEveryFewVersionsStrategy.With(SnapshotEveryVersion))
        {
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override Task<Snapshots.Location> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new Snapshots.Location(this));
        }

        protected override Task LoadSnapshotAsync(Snapshots.Location snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            this.Title = snapshot.Title;
            this.Address = snapshot.Address;
            this.Latitude = snapshot.Latitude;
            this.Longitude = snapshot.Longitude;
            this.PricePerKw = snapshot.PricePerKw;
            this.Owner = snapshot.Owner;
            this.Invitations = snapshot.Invitations;
            return Task.FromResult(0);
        }

        #endregion [ Protected methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the location name.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     Gets or sets the location address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        ///     Gets or sets the location latitude.
        /// </summary>
        public Latitude Latitude { get; private set; }

        /// <summary>
        ///     Gets or sets the location longitude.
        /// </summary>
        public Longitude Longitude { get; private set; }

        /// <summary>
        ///     Gets or sets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; private set; }

        /// <summary>
        ///     Gets or sets the location owner.
        /// </summary>
        public User Owner { get; private set; }

        /// <summary>
        ///     Gets or sets the location invitations.
        /// </summary>
        public Invitations Invitations { get; private set; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        public void Create(string name, string address, Latitude latitude, Longitude longitude, decimal pricePerKw, User owner)
        {
            DefaultAggregateSpecifications.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
            this.Emit(new LocationAdded(name, address, latitude, longitude, pricePerKw, owner));
        }

        public void Edit(string name, string address, Latitude latitude, Longitude longitude, decimal pricePerKw, User user)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            OwnedAggregateSpecifications.AggregateIsOwnedByUser(this).ThrowDomainErrorIfNotSatisfied(user);
            this.Emit(new LocationEdited(name, address, latitude, longitude, pricePerKw));
        }

        public void Delete(User user)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            OwnedAggregateSpecifications.AggregateIsOwnedByUser(this).ThrowDomainErrorIfNotSatisfied(user);
            this.Emit(new LocationDeleted());
        }

        public void InviteUser(User user, Email email)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            OwnedAggregateSpecifications.AggregateIsOwnedByUser(this).ThrowDomainErrorIfNotSatisfied(user);
            this.Invitations.UserHasNotBeenInvitedYet().ThrowDomainErrorIfNotSatisfied(email);
            this.Emit(new InvitationAdded(InvitationId.NewComb(), email));
        }

        public void DeleteInvitation(User user, InvitationId id)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            OwnedAggregateSpecifications.AggregateIsOwnedByUser(this).ThrowDomainErrorIfNotSatisfied(user);
            this.Invitations.UserHasBeenInvited().ThrowDomainErrorIfNotSatisfied(id);
            this.Emit(new InvitationDeleted(id));
        }

        public void AcceptInvitation(InvitationId id, User user)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            this.Invitations.UserHasBeenInvited().ThrowDomainErrorIfNotSatisfied(id);
            this.Emit(new InvitationAccepted(id, user));
        }

        public void RejectInvitation(InvitationId id, User user)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            this.Invitations.UserHasBeenInvited().ThrowDomainErrorIfNotSatisfied(id);
            this.Emit(new InvitationRejected(id, user));
        }

        public void Apply(LocationAdded @event)
        {
            this.Title = @event.Title;
            this.Address = @event.Address;
            this.Latitude = @event.Latitude;
            this.Longitude = @event.Longitude;
            this.PricePerKw = @event.PricePerKw;
            this.Owner = @event.Owner;
            this.Invitations = new Invitations();
        }

        public void Apply(LocationEdited @event)
        {
            this.Title = @event.Title;
            this.Address = @event.Address;
            this.Latitude = @event.Latitude;
            this.Longitude = @event.Longitude;
            this.PricePerKw = @event.PricePerKw;
        }

        public void Apply(LocationDeleted @event)
        {
        }

        public void Apply(InvitationAdded @event)
        {
            if (this.Invitations == null)
            {
                this.Invitations = new Invitations();
            }

            this.Invitations.Items.Add(new Invitation
            {
                Id = @event.InvitationId,
                Email = @event.Email
            });
        }

        public void Apply(InvitationDeleted @event)
        {
            if (this.Invitations == null)
            {
                return;
            }

            this.Invitations.Items.RemoveAll(invitation => invitation.Id.Equals(@event.InvitationId));
        }

        public void Apply(InvitationAccepted @event)
        {
            Invitation invitation = this.Invitations?.Items.FirstOrDefault(item => item.Id.Equals(@event.InvitationId));

            if (invitation == null)
            {
                return;
            }

            invitation.Accepted = true;
            invitation.User = @event.User;
        }

        public void Apply(InvitationRejected @event)
        {
            Invitation invitation = this.Invitations?.Items.FirstOrDefault(item => item.Id.Equals(@event.InvitationId));

            if (invitation == null)
            {
                return;
            }

            invitation.Accepted = false;
            invitation.User = @event.User;
        }

        #endregion [ Public methods ]
    }
}
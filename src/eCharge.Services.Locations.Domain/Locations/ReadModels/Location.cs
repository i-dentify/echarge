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

namespace ECharge.Services.Locations.Domain.Locations.ReadModels
{
    #region [ References ]

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ECharge.Domain.ReadModels;
    using ECharge.Services.Locations.Domain.Locations.Events;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.ReadStores;

    #endregion

    public class Location : IReadModel, IOwnedReadModel,
        IAmReadModelFor<Aggregates.Location, LocationId, LocationAdded>,
        IAmReadModelFor<Aggregates.Location, LocationId, LocationEdited>,
        IAmReadModelFor<Aggregates.Location, LocationId, LocationDeleted>
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the location id.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the entity version.
        /// </summary>
        [ConcurrencyCheck]
        public long Version { get; set; }

        /// <summary>
        ///     Gets or sets the location name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the location address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Gets or sets the location latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///     Gets or sets the location longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        ///     Gets or sets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; set; }

        /// <summary>
        ///     Gets or sets the location owner.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        ///     Gets or sets the location invitations.
        /// </summary>
        public virtual List<Invitation> Invitations { get; set; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, LocationAdded> @event)
        {
            this.Id = @event.AggregateIdentity.Value;
            this.Name = @event.AggregateEvent.Title;
            this.Address = @event.AggregateEvent.Address;
            this.Latitude = @event.AggregateEvent.Latitude.Value;
            this.Longitude = @event.AggregateEvent.Longitude.Value;
            this.PricePerKw = @event.AggregateEvent.PricePerKw;
            this.Owner = @event.AggregateEvent.Owner.Value.ToSha256();
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, LocationEdited> @event)
        {
            this.Name = @event.AggregateEvent.Title;
            this.Address = @event.AggregateEvent.Address;
            this.Latitude = @event.AggregateEvent.Latitude.Value;
            this.Longitude = @event.AggregateEvent.Longitude.Value;
            this.PricePerKw = @event.AggregateEvent.PricePerKw;
        }

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Location, LocationId, LocationDeleted> @event)
        {
            context.MarkForDeletion();
        }

        #endregion [ Public methods ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="Car.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.ReadModels
{
    #region [ References ]

    using System.ComponentModel.DataAnnotations;
    using ECharge.Domain.ReadModels;
    using ECharge.Services.Cars.Domain.Cars.Events;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.ReadStores;

    #endregion

    public class Car : IReadModel, IOwnedReadModel,
        IAmReadModelFor<Aggregates.Car, CarId, CarAdded>,
        IAmReadModelFor<Aggregates.Car, CarId, CarEdited>,
        IAmReadModelFor<Aggregates.Car, CarId, CarDeleted>
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the car id.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the entity version.
        /// </summary>
        [ConcurrencyCheck]
        public long Version { get; set; }

        /// <summary>
        ///     Gets or sets the car name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the battery capacity.
        /// </summary>
        public int BatteryCapacity { get; set; }

        /// <summary>
        ///     Gets or sets the car owner.
        /// </summary>
        public string Owner { get; set; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        public void Apply(IReadModelContext context, IDomainEvent<Aggregates.Car, CarId, CarAdded> @event)
        {
            this.Id = @event.AggregateIdentity.Value;
            this.Name = @event.AggregateEvent.Title;
            this.BatteryCapacity = @event.AggregateEvent.BatteryCapacity.Value;
            this.Owner = @event.AggregateEvent.Owner.Value.ToSha256();
        }

        public void Apply(IReadModelContext context, IDomainEvent<Aggregates.Car, CarId, CarEdited> @event)
        {
            this.Name = @event.AggregateEvent.Title;
            this.BatteryCapacity = @event.AggregateEvent.BatteryCapacity.Value;
        }

        public void Apply(IReadModelContext context, IDomainEvent<Aggregates.Car, CarId, CarDeleted> @event)
        {
            context.MarkForDeletion();
        }

        #endregion [ Public methods ]
    }
}
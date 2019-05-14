#region [ COPYRIGHT ]

// <copyright file="Charge.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.ReadModels
{
    #region [ References ]

    using System;
    using System.ComponentModel.DataAnnotations;
    using ECharge.Domain.ReadModels;
    using ECharge.Services.Charges.Domain.Charges.Events;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.ReadStores;

    #endregion

    public class Charge : IReadModel, IOwnedReadModel,
        IAmReadModelFor<Aggregates.Charge, ChargeId, ChargeAdded>,
        IAmReadModelFor<Aggregates.Charge, ChargeId, ChargeClosed>
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the Charge id.
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the entity version.
        /// </summary>
        [ConcurrencyCheck]
        public long Version { get; set; }

        /// <summary>
        ///     Gets or sets the owner of the charge.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        ///     Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        ///     Gets or sets the car.
        /// </summary>
        public string Car { get; set; }

        /// <summary>
        ///     Gets or sets the date when the charge was started.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the percentual battery load at begin of charging.
        /// </summary>
        public int LoadStart { get; set; }

        /// <summary>
        ///     Gets or sets the percentual battery load at end of charging.
        /// </summary>
        public int LoadEnd { get; set; }

        /// <summary>
        ///     Gets or sets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; set; }

        /// <summary>
        ///     Gets or sets the battery capacity.
        /// </summary>
        public int BatteryCapacity { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the charge has been cleared.
        /// </summary>
        public bool Cleared { get; set; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        public void Apply(IReadModelContext context,
            IDomainEvent<Aggregates.Charge, ChargeId, ChargeAdded> @event)
        {
            this.Id = @event.AggregateIdentity.Value;
            this.Owner = @event.AggregateEvent.Owner.Value.ToSha256();
            this.Location = @event.AggregateEvent.Location;
            this.Car = @event.AggregateEvent.Car;
            this.Date = @event.AggregateEvent.Date;
            this.LoadStart = @event.AggregateEvent.LoadStart.Value;
            this.LoadEnd = @event.AggregateEvent.LoadEnd.Value;
            this.PricePerKw = @event.AggregateEvent.PricePerKw;
            this.BatteryCapacity = @event.AggregateEvent.BatteryCapacity.Value;
            this.Cleared = false;
        }

        public void Apply(IReadModelContext context, IDomainEvent<Aggregates.Charge, ChargeId, ChargeClosed> @event)
        {
            this.Cleared = true;
        }

        #endregion [ Public methods ]
    }
}
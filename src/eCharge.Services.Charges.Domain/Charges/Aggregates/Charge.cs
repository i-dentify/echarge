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

namespace ECharge.Services.Charges.Domain.Charges.Aggregates
{
    #region [ References ]

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Domain.Aggregates;
    using ECharge.Domain.Specifications;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Charges.Domain.Charges.Events;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.Snapshots;
    using EventFlow.Snapshots.Strategies;

    #endregion

    public class Charge : SnapshotAggregateRoot<Charge, ChargeId, Snapshots.Charge>,
        IOwnedAggregate<ChargeId>,
        IApply<ChargeAdded>,
        IApply<ChargeClosed>
    {
        #region [ Private attributes ]

        private const int SnapshotEveryVersion = 10;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public Charge(ChargeId id) : base(id, SnapshotEveryFewVersionsStrategy.With(SnapshotEveryVersion))
        {
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override Task<Snapshots.Charge> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new Snapshots.Charge(this));
        }

        protected override Task LoadSnapshotAsync(Snapshots.Charge snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            this.Owner = snapshot.Owner;
            this.Location = snapshot.Location;
            this.Car = snapshot.Car;
            this.Date = snapshot.Date;
            this.LoadStart = snapshot.LoadStart;
            this.LoadEnd = snapshot.LoadEnd;
            this.PricePerKw = snapshot.PricePerKw;
            this.BatteryCapacity = snapshot.BatteryCapacity;
            this.Cleared = snapshot.Cleared;
            return Task.FromResult(0);
        }

        #endregion [ Protected methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the owner of the charge.
        /// </summary>
        public User Owner { get; private set; }

        /// <summary>
        ///     Gets or sets the location.
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        ///     Gets or sets the car.
        /// </summary>
        public string Car { get; private set; }

        /// <summary>
        ///     Gets or sets the date when the charge was started.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        ///     Gets or sets the percentual battery load at begin of charging.
        /// </summary>
        public Percentage LoadStart { get; private set; }

        /// <summary>
        ///     Gets or sets the percentual battery load at end of charging.
        /// </summary>
        public Percentage LoadEnd { get; private set; }

        /// <summary>
        ///     Gets or sets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; private set; }

        /// <summary>
        ///     Gets or sets the battery capacity.
        /// </summary>
        public BatteryCapacity BatteryCapacity { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the charge has been cleared.
        /// </summary>
        public bool Cleared { get; private set; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        public void Create(string location, string car, DateTime date, int loadStart, int loadEnd, decimal pricePerKw,
            int batteryCapacity, string owner)
        {
            DefaultAggregateSpecifications.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
            this.Emit(new ChargeAdded(new User(owner), location, car, date, new Percentage(loadStart),
                new Percentage(loadEnd), pricePerKw, new BatteryCapacity(batteryCapacity)));
        }

        public void Close()
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            this.Emit(new ChargeClosed());
        }

        public void Apply(ChargeAdded @event)
        {
            this.Owner = @event.Owner;
            this.Location = @event.Location;
            this.Car = @event.Car;
            this.Date = @event.Date;
            this.LoadStart = @event.LoadStart;
            this.LoadEnd = @event.LoadEnd;
            this.PricePerKw = @event.PricePerKw;
            this.BatteryCapacity = @event.BatteryCapacity;
            this.Cleared = false;
        }

        public void Apply(ChargeClosed @event)
        {
            this.Cleared = true;
        }

        #endregion [ Public methods ]
    }
}
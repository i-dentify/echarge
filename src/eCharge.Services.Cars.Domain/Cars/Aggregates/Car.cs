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

namespace ECharge.Services.Cars.Domain.Cars.Aggregates
{
    #region [ References ]

    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Domain.Aggregates;
    using ECharge.Domain.Specifications;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Cars.Domain.Cars.Events;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Extensions;
    using EventFlow.Snapshots;
    using EventFlow.Snapshots.Strategies;

    #endregion

    public class Car : SnapshotAggregateRoot<Car, CarId, Snapshots.Car>,
        IOwnedAggregate<CarId>,
        IApply<CarAdded>,
        IApply<CarEdited>,
        IApply<CarDeleted>
    {
        #region [ Private attributes ]

        private const int SnapshotEveryVersion = 10;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public Car(CarId id) : base(id, SnapshotEveryFewVersionsStrategy.With(SnapshotEveryVersion))
        {
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override Task<Snapshots.Car> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new Snapshots.Car(this));
        }

        protected override Task LoadSnapshotAsync(Snapshots.Car snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            this.Title = snapshot.Title;
            this.BatteryCapacity = snapshot.BatteryCapacity;
            this.Owner = snapshot.Owner;
            return Task.FromResult(0);
        }

        #endregion [ Protected methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car name.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     Gets the battery capacity.
        /// </summary>
        public BatteryCapacity BatteryCapacity { get; private set; }

        /// <summary>
        ///     Gets the car owner.
        /// </summary>
        public User Owner { get; private set; }

        #endregion [ Public properties ]

        #region [ Public methods ]

        public void Create(string name, int batteryCapacity, string owner)
        {
            DefaultAggregateSpecifications.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
            this.Emit(new CarAdded(name, new BatteryCapacity(batteryCapacity), new User(owner)));
        }

        public void Edit(string name, int batteryCapacity, string user)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            OwnedAggregateSpecifications.AggregateIsOwnedByUser(this).ThrowDomainErrorIfNotSatisfied(new User(user));
            this.Emit(new CarEdited(name, new BatteryCapacity(batteryCapacity)));
        }

        public void Delete(string user)
        {
            DefaultAggregateSpecifications.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
            OwnedAggregateSpecifications.AggregateIsOwnedByUser(this).ThrowDomainErrorIfNotSatisfied(new User(user));
            this.Emit(new CarDeleted());
        }

        public void Apply(CarAdded @event)
        {
            this.Title = @event.Title;
            this.BatteryCapacity = @event.BatteryCapacity;
            this.Owner = @event.Owner;
        }

        public void Apply(CarEdited @event)
        {
            this.Title = @event.Title;
            this.BatteryCapacity = @event.BatteryCapacity;
        }

        public void Apply(CarDeleted @event)
        {
        }

        #endregion [ Public methods ]
    }
}
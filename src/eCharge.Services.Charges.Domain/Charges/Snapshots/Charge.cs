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

namespace ECharge.Services.Charges.Domain.Charges.Snapshots
{
    #region [ References ]

    using System;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using EventFlow.Snapshots;
    using Newtonsoft.Json;

    #endregion

    [SnapshotVersion("charge", 1)]
    public class Charge : ISnapshot
    {
        #region [ Constructor ]

        public Charge(Aggregates.Charge aggregate)
        {
            this.Owner = aggregate.Owner;
            this.Location = aggregate.Location;
            this.Car = aggregate.Car;
            this.Date = aggregate.Date;
            this.LoadStart = aggregate.LoadStart;
            this.LoadEnd = aggregate.LoadEnd;
            this.PricePerKw = aggregate.PricePerKw;
            this.BatteryCapacity = aggregate.BatteryCapacity;
            this.Cleared = aggregate.Cleared;
        }

        [JsonConstructor]
        public Charge(User owner, string location, string car, DateTime date, Percentage loadStart, Percentage loadEnd,
            decimal pricePerKw, BatteryCapacity batteryCapacity, bool cleared)
        {
            this.Owner = owner;
            this.Location = location;
            this.Car = car;
            this.Date = date;
            this.LoadStart = loadStart;
            this.LoadEnd = loadEnd;
            this.PricePerKw = pricePerKw;
            this.BatteryCapacity = batteryCapacity;
            this.Cleared = cleared;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the owner of the charge.
        /// </summary>
        public User Owner { get; }

        /// <summary>
        ///     Gets the location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        ///     Gets the car.
        /// </summary>
        public string Car { get; }

        /// <summary>
        ///     Gets the date when the charge was started.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        ///     Gets the percentual battery load at begin of charging.
        /// </summary>
        public Percentage LoadStart { get; }

        /// <summary>
        ///     Gets the percentual battery load at end of charging.
        /// </summary>
        public Percentage LoadEnd { get; }

        /// <summary>
        ///     Gets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; }

        /// <summary>
        ///     Gets the battery capacity.
        /// </summary>
        public BatteryCapacity BatteryCapacity { get; }

        /// <summary>
        ///     Gets a value indicating whether the charge has been cleared.
        /// </summary>
        public bool Cleared { get; }

        #endregion [ Public properties ]
    }
}
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

namespace ECharge.Services.Cars.Domain.Cars.Snapshots
{
    #region [ References ]

    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Snapshots;
    using Newtonsoft.Json;

    #endregion

    [SnapshotVersion("car", 1)]
    public class Car : ISnapshot
    {
        #region [ Constructor ]

        public Car(Aggregates.Car aggregate)
        {
            this.Title = aggregate.Title;
            this.BatteryCapacity = aggregate.BatteryCapacity;
            this.Owner = aggregate.Owner;
        }

        [JsonConstructor]
        public Car(string title, BatteryCapacity batteryCapacity, User owner)
        {
            this.Title = title;
            this.BatteryCapacity = batteryCapacity;
            this.Owner = owner;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car name.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     Gets the battery capacity.
        /// </summary>
        public BatteryCapacity BatteryCapacity { get; }

        /// <summary>
        ///     Gets the car owner.
        /// </summary>
        public User Owner { get; }

        #endregion [ Public properties ]
    }
}
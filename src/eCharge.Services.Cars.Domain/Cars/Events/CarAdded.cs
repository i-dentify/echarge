#region [ COPYRIGHT ]

// <copyright file="CarAdded.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-03-15</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Cars.Domain.Cars.Events
{
    #region [ References ]

    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Cars.Domain.Cars.Aggregates;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("CarAdded", 1)]
    public class CarAdded : AggregateEvent<Car, CarId>
    {
        #region [ Constructor ]

        public CarAdded(string title, BatteryCapacity batteryCapacity, User owner)
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
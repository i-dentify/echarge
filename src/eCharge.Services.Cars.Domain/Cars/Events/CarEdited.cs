#region [ COPYRIGHT ]

// <copyright file="CarEdited.cs" company="i-dentify Software Development">
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

    using ECharge.Services.Cars.Domain.Cars.Aggregates;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("CarEdited", 1)]
    public class CarEdited : AggregateEvent<Car, CarId>
    {
        #region [ Constructor ]

        public CarEdited(string title, BatteryCapacity batteryCapacity)
        {
            this.Title = title;
            this.BatteryCapacity = batteryCapacity;
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

        #endregion [ Public properties ]
    }
}
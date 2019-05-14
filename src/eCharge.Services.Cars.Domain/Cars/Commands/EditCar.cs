#region [ COPYRIGHT ]

// <copyright file="EditCar.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Commands
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.Aggregates;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class EditCar : Command<Car, CarId>
    {
        #region [ Constructor ]

        public EditCar(CarId aggregateId, string name, int batteryCapacity) : base(aggregateId)
        {
            this.Name = name;
            this.BatteryCapacity = batteryCapacity;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the battery capacity.
        /// </summary>
        public int BatteryCapacity { get; }

        #endregion [ Public properties ]
    }
}
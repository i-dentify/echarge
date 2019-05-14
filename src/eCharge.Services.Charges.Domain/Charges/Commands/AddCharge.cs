#region [ COPYRIGHT ]

// <copyright file="AddCharge.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Commands
{
    #region [ References ]

    using System;
    using ECharge.Services.Charges.Domain.Charges.Aggregates;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class AddCharge : Command<Charge, ChargeId>
    {
        #region [ Constructor ]

        public AddCharge(ChargeId aggregateId, string location, string car, DateTime date, int loadStart, int loadEnd,
            decimal pricePerKw, int batteryCapacity) :
            base(aggregateId)
        {
            this.Location = location;
            this.Car = car;
            this.Date = date;
            this.LoadStart = loadStart;
            this.LoadEnd = loadEnd;
            this.PricePerKw = pricePerKw;
            this.BatteryCapacity = batteryCapacity;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        ///     Gets the car.
        /// </summary>
        public string Car { get; }

        /// <summary>
        ///     Gets date when the charge was started.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        ///     Gets the percentual battery load at begin of charging.
        /// </summary>
        public int LoadStart { get; }

        /// <summary>
        ///     Gets the percentual battery load at end of charging.
        /// </summary>
        public int LoadEnd { get; }

        /// <summary>
        ///     Gets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; }

        /// <summary>
        ///     Gets the battery capacity.
        /// </summary>
        public int BatteryCapacity { get; }

        #endregion [ Public properties ]
    }
}
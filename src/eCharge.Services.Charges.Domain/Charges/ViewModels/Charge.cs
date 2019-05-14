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
// <date>2018-03-15</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Charges.Domain.Charges.ViewModels
{
    #region [ References ]

    using System;

    #endregion

    public class Charge
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the charge id.
        /// </summary>
        public string Id { get; set; }

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
    }
}
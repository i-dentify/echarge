﻿#region [ COPYRIGHT ]

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

namespace ECharge.Services.Cars.Domain.Cars.ViewModels
{
    #region [ References ]

    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class EditCar
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the car id.
        /// </summary>
        [FromRoute(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the car name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the battery capacity.
        /// </summary>
        public int BatteryCapacity { get; set; }

        #endregion [ Public properties ]
    }
}
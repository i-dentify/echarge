#region [ COPYRIGHT ]

// <copyright file="EditLocation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.ViewModels
{
    #region [ References ]

    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class EditLocation
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the location id.
        /// </summary>
        [FromRoute(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the location name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the location address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///     Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        ///     Gets or sets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; set; }

        #endregion [ Public properties ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="AddLocation.cs" company="i-dentify Software Development">
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

    public class AddLocation
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the location name.
        /// </summary>
        [FromBody]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the location address.
        /// </summary>
        [FromBody]
        public string Address { get; set; }

        /// <summary>
        ///     Gets or sets the latitude.
        /// </summary>
        [FromBody]
        public double Latitude { get; set; }

        /// <summary>
        ///     Gets or sets the longitude.
        /// </summary>
        [FromBody]
        public double Longitude { get; set; }

        /// <summary>
        ///     Gets or sets the charging price per KW.
        /// </summary>
        [FromBody]
        public decimal PricePerKw { get; set; }

        #endregion [ Public properties ]
    }
}
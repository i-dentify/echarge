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
// <date>2018-12-30</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Locations.Domain.Locations.Commands
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class EditLocation : Command<Location, LocationId>
    {
        #region [ Constructor ]

        public EditLocation(LocationId aggregateId, string name, string address, Latitude latitude, Longitude longitude,
            decimal pricePerKw) : base(aggregateId)
        {
            this.Name = name;
            this.Address = address;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.PricePerKw = pricePerKw;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the location address.
        /// </summary>
        public string Address { get; }

        /// <summary>
        ///     Gets the location latitude.
        /// </summary>
        public Latitude Latitude { get; }

        /// <summary>
        ///     Gets the location longitude.
        /// </summary>
        public Longitude Longitude { get; }

        /// <summary>
        ///     Gets the charging price per KW.
        /// </summary>
        public decimal PricePerKw { get; }

        #endregion [ Public properties ]
    }
}
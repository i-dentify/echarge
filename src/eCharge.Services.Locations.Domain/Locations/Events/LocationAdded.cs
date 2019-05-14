#region [ COPYRIGHT ]

// <copyright file="LocationAdded.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Events
{
    #region [ References ]

    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("LocationAdded", 1)]
    public class LocationAdded : AggregateEvent<Location, LocationId>
    {
        #region [ Constructor ]

        public LocationAdded(string title, string address, Latitude latitude, Longitude longitude, decimal pricePerKw,
            User owner)
        {
            this.Title = title;
            this.Address = address;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.PricePerKw = pricePerKw;
            this.Owner = owner;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location name.
        /// </summary>
        public string Title { get; }

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

        /// <summary>
        ///     Gets the location owner.
        /// </summary>
        public User Owner { get; }

        #endregion [ Public properties ]
    }
}
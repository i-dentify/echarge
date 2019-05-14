#region [ COPYRIGHT ]

// <copyright file="Location.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Snapshots
{
    #region [ References ]

    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Snapshots;
    using Newtonsoft.Json;

    #endregion

    [SnapshotVersion("location", 1)]
    public class Location : ISnapshot
    {
        #region [ Constructor ]

        public Location(Aggregates.Location aggregate)
        {
            this.Title = aggregate.Title;
            this.Address = aggregate.Address;
            this.Latitude = aggregate.Latitude;
            this.Longitude = aggregate.Longitude;
            this.PricePerKw = aggregate.PricePerKw;
            this.Owner = aggregate.Owner;
            this.Invitations = aggregate.Invitations;
        }

        [JsonConstructor]
        public Location(string title, string address, Latitude latitude, Longitude longitude, decimal pricePerKw,
            User owner, Invitations invitations)
        {
            this.Title = title;
            this.Address = address;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.PricePerKw = pricePerKw;
            this.Owner = owner;
            this.Invitations = invitations;
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

        /// <summary>
        ///     Gets the location invitations.
        /// </summary>
        public Invitations Invitations { get; }

        #endregion [ Public properties ]
    }
}
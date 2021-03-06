﻿#region [ COPYRIGHT ]

// <copyright file="LocationById.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Queries
{
    #region [ References ]

    using System;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.ViewModels;
    using EventFlow.Queries;

    #endregion

    public class LocationById : IQuery<Location>
    {
        #region [ Constructor ]

        public LocationById(string id)
        {
            this.Id = LocationId.With(Guid.Parse(id)).Value;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location id.
        /// </summary>
        public string Id { get; }

        #endregion [ Public properties ]
    }
}
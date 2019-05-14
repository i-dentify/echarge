#region [ COPYRIGHT ]

// <copyright file="LocationsByIds.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Queries
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.ViewModels;
    using EventFlow.Queries;

    #endregion

    public class LocationsByIds : IQuery<IReadOnlyCollection<Location>>
    {
        #region [ Constructor ]

        public LocationsByIds(List<string> ids)
        {
            this.Ids = ids?.Select(id => LocationId.With(Guid.Parse(id)).Value).Distinct().ToList() ??
                       new List<string>();
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location ids.
        /// </summary>
        public List<string> Ids { get; }

        #endregion [ Public properties ]
    }
}
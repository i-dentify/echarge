#region [ COPYRIGHT ]

// <copyright file="CarsByIds.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Queries
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using ECharge.Services.Cars.Domain.Cars.ViewModels;
    using EventFlow.Queries;

    #endregion

    public class CarsByIds : IQuery<IReadOnlyCollection<Car>>
    {
        #region [ Constructor ]

        public CarsByIds(List<string> ids)
        {
            this.Ids = ids?.Select(id => CarId.With(Guid.Parse(id)).Value).Distinct().ToList() ?? new List<string>();
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car ids.
        /// </summary>
        public List<string> Ids { get; }

        #endregion [ Public properties ]
    }
}
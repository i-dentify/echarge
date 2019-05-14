#region [ COPYRIGHT ]

// <copyright file="UserIsCarOwner.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Queries
{
    #region [ References ]

    using System;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Queries;

    #endregion

    public class UserIsCarOwner : IQuery<bool>
    {
        #region [ Constructor ]

        public UserIsCarOwner(string id)
        {
            this.Id = CarId.With(Guid.Parse(id)).Value;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car id.
        /// </summary>
        public string Id { get; }

        #endregion [ Public properties ]
    }
}
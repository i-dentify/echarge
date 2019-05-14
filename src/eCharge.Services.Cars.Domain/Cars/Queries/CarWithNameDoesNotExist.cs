#region [ COPYRIGHT ]

// <copyright file="CarWithNameDoesNotExist.cs" company="i-dentify Software Development">
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
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Queries;

    #endregion

    public class CarWithNameDoesNotExist : IQuery<bool>
    {
        #region [ Constructor ]

        public CarWithNameDoesNotExist(string name, string exceptId = null)
        {
            this.Name = name;
            this.ExceptId = string.IsNullOrWhiteSpace(exceptId) ? null : CarId.With(Guid.Parse(exceptId)).Value;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car id to exclude.
        /// </summary>
        public string ExceptId { get; }

        /// <summary>
        ///     Gets the car name.
        /// </summary>
        public string Name { get; }

        #endregion [ Public properties ]
    }
}
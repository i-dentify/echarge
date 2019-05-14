#region [ COPYRIGHT ]

// <copyright file="CarId.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.ValueObjects
{
    #region [ References ]

    using EventFlow.Core;
    using EventFlow.ValueObjects;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    ///     Immutable car Id.
    /// </summary>
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class CarId : Identity<CarId>
    {
        #region [ Constructor ]

        /// <summary>
        ///     Initializes a new instance of the <see cref="CarId" /> class.
        /// </summary>
        /// <param name="value">The immutable car id.</param>
        public CarId(string value) : base(value)
        {
        }

        #endregion [ Constructor ]
    }
}
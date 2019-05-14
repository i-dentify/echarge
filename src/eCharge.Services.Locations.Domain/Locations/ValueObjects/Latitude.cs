#region [ COPYRIGHT ]

// <copyright file="Latitude.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.ValueObjects
{
    #region [ References ]

    using EventFlow.Exceptions;
    using EventFlow.ValueObjects;
    using Newtonsoft.Json;

    #endregion

    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Latitude : SingleValueObject<double>
    {
        #region [ Constructor ]

        public Latitude(double value) : base(value)
        {
            if (value < -90 || value > 90)
            {
                throw DomainError.With($"Invalid latitude '{value}'");
            }
        }

        #endregion [ Constructor ]
    }
}
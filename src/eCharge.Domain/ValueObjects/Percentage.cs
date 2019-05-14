﻿#region [ COPYRIGHT ]

// <copyright file="Percentage.cs" company="i-dentify Software Development">
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

namespace ECharge.Domain.ValueObjects
{
    #region [ References ]

    using EventFlow.Exceptions;
    using EventFlow.ValueObjects;
    using Newtonsoft.Json;

    #endregion

    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class Percentage : SingleValueObject<int>
    {
        #region [ Constructor ]

        public Percentage(int value) : base(value)
        {
            if (value < 0 || value > 100)
            {
                throw DomainError.With($"Invalid percentage '{value}'");
            }
        }

        #endregion [ Constructor ]
    }
}
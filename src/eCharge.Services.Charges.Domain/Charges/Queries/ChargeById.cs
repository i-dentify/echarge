#region [ COPYRIGHT ]

// <copyright file="ChargeById.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Queries
{
    #region [ References ]

    using System;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using ECharge.Services.Charges.Domain.Charges.ViewModels;
    using EventFlow.Queries;

    #endregion

    public class ChargeById : IQuery<Charge>
    {
        #region [ Constructor ]

        public ChargeById(string id)
        {
            this.Id = ChargeId.With(Guid.Parse(id)).Value;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the charge id.
        /// </summary>
        public string Id { get; }

        #endregion [ Public properties ]
    }
}
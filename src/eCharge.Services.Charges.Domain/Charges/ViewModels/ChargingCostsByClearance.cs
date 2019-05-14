#region [ COPYRIGHT ]

// <copyright file="ChargingCostsByClearance.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.ViewModels
{
    public class ChargingCostsByClearance
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the open charging costs.
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        ///     Gets or sets the cleared charging costs.
        /// </summary>
        public decimal Cleared { get; set; }

        #endregion [ Public properties ]
    }
}
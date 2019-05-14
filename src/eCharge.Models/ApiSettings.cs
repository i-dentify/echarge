#region [ COPYRIGHT ]

// <copyright file="ApiSettings.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-03-19</date>
// <summary></summary>

#endregion

namespace ECharge.Models
{
    #region [ References ]

    using System;

    #endregion

    public class ApiSettings
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the uri to the locations service.
        /// </summary>
        public Uri Locations { get; set; }

        /// <summary>
        ///     Gets or sets the uri to the cars service.
        /// </summary>
        public Uri Cars { get; set; }

        /// <summary>
        ///     Gets or sets the uri to the charges service.
        /// </summary>
        public Uri Charges { get; set; }

        #endregion [ Public properties ]
    }
}
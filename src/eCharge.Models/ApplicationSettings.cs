#region [ COPYRIGHT ]

// <copyright file="ApplicationSettings.cs" company="i-dentify Software Development">
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

    public class ApplicationSettings
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the uri to the frontend
        /// </summary>
        public Uri Frontend { get; set; }

        /// <summary>
        ///     Gets or sets the api settings.
        /// </summary>
        public ApiSettings Api { get; set; }

        #endregion [ Public properties ]
    }
}
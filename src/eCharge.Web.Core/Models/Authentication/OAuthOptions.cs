#region [ COPYRIGHT ]

// <copyright file="OAuthOptions.cs" company="i-dentify Software Development">
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

namespace ECharge.Web.Core.Models.Authentication
{
    public class OAuthOptions
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the authority.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        ///     Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }

        #endregion [ Public properties ]
    }
}
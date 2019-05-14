#region [ COPYRIGHT ]

// <copyright file="SwaggerOptions.cs" company="i-dentify Software Development">
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

namespace ECharge.Web.Core.Models
{
    public class SwaggerOptions
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the Swagger configuration api version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Gets or sets the Swagger configuration title.
        /// </summary>
        public string Title { get; set; }

        #endregion [ Public properties ]
    }
}
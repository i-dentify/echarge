#region [ COPYRIGHT ]

// <copyright file="MicroserviceOptions.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-12-30</date>
// <summary></summary>

#endregion

namespace ECharge.Web.Core.Models
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using ECharge.Models;
    using ECharge.Web.Core.Models.Authentication;
    using EventFlow;

    #endregion

    public class MicroserviceOptions
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the authentication options.
        /// </summary>
        public OAuthOptions Authentication { get; set; }

        /// <summary>
        ///     Gets or sets the database options.
        /// </summary>
        public DatabaseOptions Database { get; set; }

        /// <summary>
        ///     Gets or sets a list of assembly names for domain objects.
        /// </summary>
        public List<AssemblyName> DomainAssemblies { get; set; }

        /// <summary>
        ///     Gets or sets the swagger options.
        /// </summary>
        public SwaggerOptions Swagger { get; set; }

        /// <summary>
        ///     Gets or sets a callback for domain registration.
        /// </summary>
        public Action<IEventFlowOptions> DomainRegistration { get; set; }

        #endregion [ Public properties ]
    }
}
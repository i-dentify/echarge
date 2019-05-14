#region [ COPYRIGHT ]

// <copyright file="CurrentUserProvider.cs" company="i-dentify Software Development">
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

namespace ECharge.Authentication
{
    #region [ References ]

    using System.Linq;
    using System.Security.Claims;
    using ECharge.Models;
    using Microsoft.AspNetCore.Http;

    #endregion

    public class CurrentUserProvider
    {
        #region [ Private attributes ]

        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        public CurrentUser User => new CurrentUser
        {
            Name = this.httpContextAccessor?.HttpContext?.User?.Claims
                       ?.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty
        };

        #endregion [ Public properties ]
    }
}
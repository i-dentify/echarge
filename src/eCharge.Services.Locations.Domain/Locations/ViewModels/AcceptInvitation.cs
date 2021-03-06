﻿#region [ COPYRIGHT ]

// <copyright file="AcceptInvitation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.ViewModels
{
    #region [ References ]

    using System;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    public class AcceptInvitation
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the invitation code.
        /// </summary>
        [FromRoute(Name = "code")]
        public Guid Code { get; set; }

        #endregion [ Public properties ]
    }
}
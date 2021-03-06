﻿#region [ COPYRIGHT ]

// <copyright file="Invitation.cs" company="i-dentify Software Development">
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
    public class Invitation
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the invitation id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the email address of the invitation recipient.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the invited user.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the user has accepted the invitation.
        /// </summary>
        public bool Accepted { get; set; }

        #endregion [ Public properties ]
    }
}
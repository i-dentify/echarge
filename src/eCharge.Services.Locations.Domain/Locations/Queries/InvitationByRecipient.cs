#region [ COPYRIGHT ]

// <copyright file="InvitationByRecipient.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Queries
{
    #region [ References ]

    using System;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Queries;
    using Invitation = ECharge.Services.Locations.Domain.Locations.ViewModels.Invitation;

    #endregion

    public class InvitationByRecipient : IQuery<Invitation>
    {
        #region [ Constructor ]

        public InvitationByRecipient(string location, string email)
        {
            this.Location = LocationId.With(Guid.Parse(location)).Value;
            this.Email = email;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location id.
        /// </summary>
        public string Location { get; }

        /// <summary>
        ///     Gets the email address of the invitation recipient.
        /// </summary>
        public string Email { get; }

        #endregion [ Public properties ]
    }
}
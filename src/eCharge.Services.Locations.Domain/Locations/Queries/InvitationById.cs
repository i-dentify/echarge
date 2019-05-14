#region [ COPYRIGHT ]

// <copyright file="InvitationById.cs" company="i-dentify Software Development">
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

    public class InvitationById : IQuery<Invitation>
    {
        #region [ Constructor ]

        public InvitationById(string location, string invitation)
        {
            this.Location = LocationId.With(Guid.Parse(location)).Value;
            this.Invitation = InvitationId.With(Guid.Parse(invitation)).Value;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the location id.
        /// </summary>
        public string Location { get; }

        /// <summary>
        ///     Gets the invitation id.
        /// </summary>
        public string Invitation { get; }

        #endregion [ Public properties ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="InviteUser.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Commands
{
    #region [ References ]

    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class InviteUser : Command<Location, LocationId>
    {
        #region [ Constructor ]

        public InviteUser(LocationId aggregateId, Email email) : base(aggregateId)
        {
            this.Email = email;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the email address of the invitation recipient.
        /// </summary>
        public Email Email { get; }

        #endregion [ Public properties ]
    }
}
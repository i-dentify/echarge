#region [ COPYRIGHT ]

// <copyright file="Invitations.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.ValueObjects
{
    #region [ References ]

    using System.Collections.Generic;
    using System.Linq;
    using ECharge.Services.Locations.Domain.Locations.Specifications;
    using EventFlow.ValueObjects;

    #endregion

    public class Invitations : ValueObject
    {
        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the invitations.
        /// </summary>
        public List<Invitation> Items { get; } = new List<Invitation>();

        #endregion [ Public properties ]

        #region [ Protected methods ]

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return this.Items.Select(item => item.Id.Value);
        }

        #endregion [ Protected methods ]

        #region [ Public methods ]

        public InvitationForEmail UserHasNotBeenInvitedYet()
        {
            return new InvitationForEmail(this);
        }

        public InvitationForId UserHasBeenInvited()
        {
            return new InvitationForId(this);
        }

        #endregion [ Public methods ]
    }
}
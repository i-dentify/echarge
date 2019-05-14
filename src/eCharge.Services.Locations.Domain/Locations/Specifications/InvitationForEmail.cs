#region [ COPYRIGHT ]

// <copyright file="InvitationForEmail.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Specifications
{
    #region [ References ]

    using System.Collections.Generic;
    using System.Linq;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Specifications;

    #endregion

    public class InvitationForEmail : Specification<Email>
    {
        #region [ Private attributes ]

        private readonly Invitations invitations;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public InvitationForEmail(Invitations invitations)
        {
            this.invitations = invitations;
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override IEnumerable<string> IsNotSatisfiedBecause(Email obj)
        {
            if (this.invitations.Items.Any(item => item.Email.Equals(obj)))
            {
                yield return $"There is already an invitation for {obj}";
            }
        }

        #endregion [ Protected methods ]
    }
}
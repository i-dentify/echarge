#region [ COPYRIGHT ]

// <copyright file="ChargesForUser.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Queries
{
    #region [ References ]

    using System.Collections.Generic;
    using ECharge.Services.Charges.Domain.Charges.ViewModels;
    using EventFlow.Queries;

    #endregion

    public class ChargesForUser : IQuery<IReadOnlyCollection<Charge>>
    {
    }
}
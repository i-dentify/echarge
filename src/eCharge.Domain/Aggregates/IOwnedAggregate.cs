#region [ COPYRIGHT ]

// <copyright file="IOwnedAggregate.cs" company="i-dentify Software Development">
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

namespace ECharge.Domain.Aggregates
{
    #region [ References ]

    using ECharge.Domain.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.Core;

    #endregion

    public interface IOwnedAggregate<out TIdentity> : IAggregateRoot<TIdentity>
        where TIdentity : IIdentity
    {
        #region [ Properties ]

        /// <summary>
        ///     Gets or sets the aggregate owner.
        /// </summary>
        User Owner { get; }

        #endregion [ Properties ]
    }
}
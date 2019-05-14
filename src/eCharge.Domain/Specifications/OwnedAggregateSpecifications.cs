#region [ COPYRIGHT ]

// <copyright file="OwnedAggregateSpecifications.cs" company="i-dentify Software Development">
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

namespace ECharge.Domain.Specifications
{
    #region [ References ]

    using System.Collections.Generic;
    using ECharge.Domain.Aggregates;
    using ECharge.Domain.ValueObjects;
    using EventFlow.Core;
    using EventFlow.Specifications;

    #endregion

    public static class OwnedAggregateSpecifications
    {
        #region [ Public properties ]

        public static ISpecification<User> AggregateIsOwnedByUser<TIdentity>(IOwnedAggregate<TIdentity> aggregate)
            where TIdentity : IIdentity
        {
            return new AggregateIsOwnedSpecification<TIdentity>(aggregate);
        }

        #endregion [ Public properties ]

        private class AggregateIsOwnedSpecification<TIdentity> : Specification<User> where TIdentity : IIdentity
        {
            private readonly IOwnedAggregate<TIdentity> aggregate;

            public AggregateIsOwnedSpecification(IOwnedAggregate<TIdentity> aggregate)
            {
                this.aggregate = aggregate;
            }

            protected override IEnumerable<string> IsNotSatisfiedBecause(User obj)
            {
                if (this.aggregate.Owner != null && !this.aggregate.Owner.Equals(obj))
                {
                    yield return $"{obj} is not the owner of the aggregate with id '{this.aggregate.Id}'.";
                }
            }
        }
    }
}
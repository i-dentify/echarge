#region [ COPYRIGHT ]

// <copyright file="DefaultAggregateSpecifications.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-12-30</date>
// <summary></summary>

#endregion

namespace ECharge.Domain.Specifications
{
    #region [ References ]

    using System.Collections.Generic;
    using EventFlow.Aggregates;
    using EventFlow.Provided.Specifications;
    using EventFlow.Specifications;

    #endregion

    public static class DefaultAggregateSpecifications
    {
        private class AggregateIsCreatedSpecification : Specification<IAggregateRoot>
        {
            protected override IEnumerable<string> IsNotSatisfiedBecause(IAggregateRoot obj)
            {
                if (obj.IsNew)
                {
                    yield return $"Aggregate '{obj.Name}' with ID '{obj.GetIdentity()}' is new";
                }
            }
        }

        #region [ Public properties ]

        public static ISpecification<IAggregateRoot> AggregateIsNew { get; } = new AggregateIsNewSpecification();

        public static ISpecification<IAggregateRoot> AggregateIsCreated { get; } =
            new AggregateIsCreatedSpecification();

        #endregion [ Public properties ]
    }
}
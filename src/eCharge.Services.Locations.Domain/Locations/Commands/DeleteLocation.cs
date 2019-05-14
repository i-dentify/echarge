#region [ COPYRIGHT ]

// <copyright file="DeleteLocation.cs" company="i-dentify Software Development">
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

    using ECharge.Services.Locations.Domain.Locations.Aggregates;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class DeleteLocation : Command<Location, LocationId>
    {
        #region [ Constructor ]

        public DeleteLocation(LocationId aggregateId) : base(aggregateId)
        {
        }

        #endregion [ Constructor ]
    }
}
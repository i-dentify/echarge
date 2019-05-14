#region [ COPYRIGHT ]

// <copyright file="CarDeleted.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Events
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.Aggregates;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("CarDeleted", 1)]
    public class CarDeleted : AggregateEvent<Car, CarId>
    {
    }
}
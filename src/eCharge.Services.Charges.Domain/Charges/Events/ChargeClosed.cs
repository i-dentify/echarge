#region [ COPYRIGHT ]

// <copyright file="ChargeClosed.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Events
{
    #region [ References ]

    using ECharge.Services.Charges.Domain.Charges.Aggregates;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using EventFlow.Aggregates;
    using EventFlow.EventStores;

    #endregion

    [EventVersion("ChargeClosed", 1)]
    public class ChargeClosed : AggregateEvent<Charge, ChargeId>
    {
    }
}
#region [ COPYRIGHT ]

// <copyright file="EventFlowOptionsExtensions.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Extensions
{
    #region [ References ]

    using ECharge.Services.Charges.Domain.Charges.ReadModels;
    using ECharge.Services.Charges.Domain.Data.Persistence.Context;
    using EventFlow;
    using EventFlow.EntityFramework.Extensions;

    #endregion

    public static class EventFlowOptionsExtensions
    {
        #region [ Public methods ]

        public static IEventFlowOptions IncludeCharges(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions.UseEntityFrameworkReadModel<Charge, ApplicationContext>();
        }

        #endregion [ Public methods ]
    }
}
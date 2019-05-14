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

namespace ECharge.Web.Core.Extensions
{
    #region [ References ]

    using System;
    using EventFlow;

    #endregion

    public static class EventFlowOptionsExtensions
    {
        #region [ Public methods ]

        public static IEventFlowOptions ConfigureDomain(this IEventFlowOptions eventFlowOptions,
            Action<IEventFlowOptions> callback)
        {
            callback(eventFlowOptions);
            return eventFlowOptions;
        }

        #endregion [ Public methods ]
    }
}
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

namespace ECharge.Services.Locations.Domain.Extensions
{
    #region [ References ]

    using System.Reflection;
    using ECharge.Services.Locations.Domain.Data.Persistence.Context;
    using ECharge.Services.Locations.Domain.Locations.Extensions;
    using EventFlow;
    using EventFlow.EntityFramework.Extensions;
    using EventFlow.Extensions;

    #endregion

    public static class EventFlowOptionsExtensions
    {
        #region [ Private attributes ]

        private static Assembly Assembly { get; } = typeof(EventFlowOptionsExtensions).Assembly;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        public static IEventFlowOptions ConfigureDomain(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddDefaults(Assembly)
                .AddDbContextProvider<ApplicationContext, DatabaseContextProvider>()
                .UseEntityFrameworkEventStore<ApplicationContext>()
                .UseEntityFrameworkSnapshotStore<ApplicationContext>()
                .IncludeLocations();
        }

        #endregion [ Public methods ]
    }
}
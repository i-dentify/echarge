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
// <date>2018-12-30</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Locations.Domain.Locations.Extensions
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Data.Persistence.Context;
    using ECharge.Services.Locations.Domain.Locations.ReadModels;
    using ECharge.Services.Locations.Domain.Locations.ReadModels.Locators;
    using EventFlow;
    using EventFlow.EntityFramework.Extensions;

    #endregion

    public static class EventFlowOptionsExtensions
    {
        #region [ Public methods ]

        public static IEventFlowOptions IncludeLocations(this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .RegisterServices(sr => { sr.RegisterType(typeof(Invitations)); })
                .UseEntityFrameworkReadModel<Location, ApplicationContext>()
                .UseEntityFrameworkReadModel<Invitation, ApplicationContext, Invitations>();
        }

        #endregion [ Public methods ]
    }
}
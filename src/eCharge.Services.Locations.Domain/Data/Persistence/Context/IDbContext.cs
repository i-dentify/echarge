#region [ COPYRIGHT ]

// <copyright file="IDbContext.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Data.Persistence.Context
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Locations.ReadModels;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public interface IDbContext
    {
        #region [ Properties ]

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Location" /> entities.
        /// </summary>
        DbSet<Location> Locations { get; set; }

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Invitation" /> entities.
        /// </summary>
        DbSet<Invitation> LocationInvitations { get; set; }

        #endregion [ Properties ]
    }
}
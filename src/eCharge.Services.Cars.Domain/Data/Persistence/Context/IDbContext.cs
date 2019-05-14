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

namespace ECharge.Services.Cars.Domain.Data.Persistence.Context
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.ReadModels;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public interface IDbContext
    {
        #region [ Properties ]

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Car" /> entities.
        /// </summary>
        DbSet<Car> Cars { get; set; }

        #endregion [ Properties ]
    }
}
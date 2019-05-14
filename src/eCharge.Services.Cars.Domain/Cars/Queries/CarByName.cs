#region [ COPYRIGHT ]

// <copyright file="CarByName.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Queries
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.ViewModels;
    using EventFlow.Queries;

    #endregion

    public class CarByName : IQuery<Car>
    {
        #region [ Constructor ]

        public CarByName(string name)
        {
            this.Name = name;
        }

        #endregion [ Constructor ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets the car name.
        /// </summary>
        public string Name { get; }

        #endregion [ Public properties ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="DeleteCar.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Commands.Handlers
{
    #region [ References ]

    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Authentication;
    using ECharge.Services.Cars.Domain.Cars.Aggregates;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class DeleteCar : CommandHandler<Car, CarId, Commands.DeleteCar>
    {
        #region [ Private attributes ]

        private readonly CurrentUserProvider currentUserProvider;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public DeleteCar(CurrentUserProvider currentUserProvider)
        {
            this.currentUserProvider = currentUserProvider;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public override Task ExecuteAsync(Car aggregate, Commands.DeleteCar command,
            CancellationToken cancellationToken)
        {
            aggregate.Delete(this.currentUserProvider.User.Name);
            return Task.FromResult(0);
        }

        #endregion [ Public methods ]
    }
}
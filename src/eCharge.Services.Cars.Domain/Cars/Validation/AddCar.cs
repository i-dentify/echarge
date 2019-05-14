#region [ COPYRIGHT ]

// <copyright file="AddCar.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Validation
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.Queries;
    using ECharge.Services.Cars.Resources;
    using EventFlow.Queries;
    using FluentValidation;

    #endregion

    public class AddCar : AbstractValidator<ViewModels.AddCar>
    {
        #region [ Constructor ]

        public AddCar(IQueryProcessor queryProcessor)
        {
            this.RuleFor(model => model.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(CarResources.CarNameRequired)
                .MustAsync((name, cancellationToken) =>
                    queryProcessor.ProcessAsync(new CarWithNameDoesNotExist(name), cancellationToken))
                .WithMessage(CarResources.CarAlreadyExists);
            this.RuleFor(model => model.BatteryCapacity)
                .Must(value => value > 0)
                .WithMessage(CarResources.InvalidBatteryCapacity);
        }

        #endregion [ Constructor ]
    }
}
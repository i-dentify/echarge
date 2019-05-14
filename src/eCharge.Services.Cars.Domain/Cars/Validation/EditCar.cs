#region [ COPYRIGHT ]

// <copyright file="EditCar.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Validation
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.Queries;
    using ECharge.Services.Cars.Resources;
    using EventFlow.Queries;
    using FluentValidation;

    #endregion

    public class EditCar : AbstractValidator<ViewModels.EditCar>
    {
        #region [ Constructor ]

        public EditCar(IQueryProcessor queryProcessor)
        {
            this.RuleFor(model => model.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(CarResources.CarIdRequired)
                .MustAsync((model, name, cancellationToken) =>
                    queryProcessor.ProcessAsync(new UserIsCarOwner(model.Id), cancellationToken))
                .WithMessage(CarResources.NotOwnerOfCar);
            this.RuleFor(model => model.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(CarResources.CarNameRequired)
                .MustAsync((model, name, cancellationToken) =>
                    queryProcessor.ProcessAsync(new CarWithNameDoesNotExist(name, model.Id), cancellationToken))
                .WithMessage(CarResources.CarAlreadyExists);
            this.RuleFor(model => model.BatteryCapacity)
                .Must(value => value > 0)
                .WithMessage(CarResources.InvalidBatteryCapacity);
        }

        #endregion [ Constructor ]
    }
}
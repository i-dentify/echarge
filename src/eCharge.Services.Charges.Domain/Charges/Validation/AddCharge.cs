#region [ COPYRIGHT ]

// <copyright file="AddCharge.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Validation
{
    #region [ References ]

    using ECharge.Services.Charges.Resources;
    using FluentValidation;

    #endregion

    public class AddCharge : AbstractValidator<ViewModels.AddCharge>
    {
        #region [ Constructor ]

        public AddCharge()
        {
            this.RuleFor(model => model.Location)
                .NotEmpty()
                .WithMessage(ChargeResources.LocationRequired);
            this.RuleFor(model => model.Car)
                .NotEmpty()
                .WithMessage(ChargeResources.CarRequired);
            this.RuleFor(model => model.LoadStart)
                .Must(value => value >= 0)
                .WithMessage(ChargeResources.InvalidLoadStart);
            this.RuleFor(model => model.LoadEnd)
                .Must((model, value) => value > model.LoadStart)
                .WithMessage(ChargeResources.LoadEndMustBeHigherThanStart);
            this.RuleFor(model => model.PricePerKw)
                .Must(value => value >= 0)
                .WithMessage(ChargeResources.InvalidPricePerKw);
            this.RuleFor(model => model.BatteryCapacity)
                .Must(value => value > 0)
                .WithMessage(ChargeResources.InvalidBatteryCapacity);
        }

        #endregion [ Constructor ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="AddLocation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Validation
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Locations.Queries;
    using ECharge.Services.Locations.Resources;
    using EventFlow.Queries;
    using FluentValidation;

    #endregion

    public class AddLocation : AbstractValidator<Locations.ViewModels.AddLocation>
    {
        #region [ Constructor ]

        public AddLocation(IQueryProcessor queryProcessor)
        {
            this.RuleFor(model => model.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(LocationResources.LocationNameRequired)
                .MustAsync((name, cancellationToken) =>
                    queryProcessor.ProcessAsync(new LocationWithNameDoesNotExist(name), cancellationToken))
                .WithMessage(LocationResources.LocationAlreadyExists);
            this.RuleFor(model => model.Latitude)
                .Must(value => value >= -90 && value <= 90)
                .WithMessage(LocationResources.InvalidLatitude);
            this.RuleFor(model => model.Longitude)
                .Must(value => value >= -180 && value <= 180)
                .WithMessage(LocationResources.InvalidLongitude);
            this.RuleFor(model => model.PricePerKw)
                .Must(value => value >= 0)
                .WithMessage(LocationResources.InvalidPricePerKw);
        }

        #endregion [ Constructor ]
    }
}
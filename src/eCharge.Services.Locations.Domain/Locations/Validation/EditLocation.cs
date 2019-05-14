#region [ COPYRIGHT ]

// <copyright file="EditLocation.cs" company="i-dentify Software Development">
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

    public class EditLocation : AbstractValidator<Locations.ViewModels.EditLocation>
    {
        #region [ Constructor ]

        public EditLocation(IQueryProcessor queryProcessor)
        {
            this.RuleFor(model => model.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(LocationResources.LocationIdRequired)
                .MustAsync((model, name, cancellationToken) =>
                    queryProcessor.ProcessAsync(new UserIsLocationOwner(model.Id), cancellationToken))
                .WithMessage(LocationResources.NotOwnerOfLocation);
            this.RuleFor(model => model.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(LocationResources.LocationNameRequired)
                .MustAsync((model, name, cancellationToken) =>
                    queryProcessor.ProcessAsync(new LocationWithNameDoesNotExist(name, model.Id), cancellationToken))
                .WithMessage(LocationResources.LocationAlreadyExists);
            this.RuleFor(model => model.Latitude)
                .Must(value => value >= -90 && value <= 90)
                .WithMessage(LocationResources.InvalidLatitude);
            this.RuleFor(model => model.Longitude)
                .Must(value => value >= -180 && value <= 180)
                .WithMessage(LocationResources.InvalidLongitude);
        }

        #endregion [ Constructor ]
    }
}
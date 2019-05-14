#region [ COPYRIGHT ]

// <copyright file="DeleteLocation.cs" company="i-dentify Software Development">
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

    public class DeleteLocation : AbstractValidator<Locations.ViewModels.DeleteLocation>
    {
        #region [ Constructor ]

        public DeleteLocation(IQueryProcessor queryProcessor)
        {
            this.RuleFor(model => model.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(LocationResources.LocationIdRequired)
                .MustAsync((id, cancellationToken) =>
                    queryProcessor.ProcessAsync(new UserIsLocationOwner(id), cancellationToken))
                .WithMessage(LocationResources.NotOwnerOfLocation);
        }

        #endregion [ Constructor ]
    }
}
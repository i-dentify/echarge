#region [ COPYRIGHT ]

// <copyright file="RejectInvitation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Validation
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Locations.Queries;
    using ECharge.Services.Locations.Resources;
    using EventFlow.Queries;
    using FluentValidation;

    #endregion

    public class RejectInvitation : AbstractValidator<ViewModels.RejectInvitation>
    {
        #region [ Constructor ]

        public RejectInvitation(IQueryProcessor queryProcessor)
        {
            this.RuleFor(model => model.Code)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(LocationResources.InvitationCodeRequired)
                .MustAsync((code, cancellationToken) =>
                    queryProcessor.ProcessAsync(new OpenInvitationExists(code.ToString()), cancellationToken))
                .WithMessage(LocationResources.InvitationNotFound);
        }

        #endregion [ Constructor ]
    }
}
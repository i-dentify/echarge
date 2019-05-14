#region [ COPYRIGHT ]

// <copyright file="Invitation.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Locations.Mapping.Profiles
{
    #region [ References ]

    using System;
    using AutoMapper;
    using ECharge.Domain.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.Events;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using DeleteInvitationCommand = ECharge.Services.Locations.Domain.Locations.Commands.DeleteInvitation;
    using DeleteInvitationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.DeleteInvitation;
    using InviteUserCommand = ECharge.Services.Locations.Domain.Locations.Commands.InviteUser;
    using InviteUserViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.InviteUser;
    using InvitationReadModel = ECharge.Services.Locations.Domain.Locations.ReadModels.Invitation;
    using InvitationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.Invitation;

    #endregion

    public class Invitation : Profile
    {
        #region [ Constructor ]

        public Invitation()
        {
            this.MapEntitiesToViewModels();
            this.MapViewModelsToCommands();
            this.CreateMap<InvitationAdded, InvitationViewModel>()
                .ForMember(target => target.Id,
                    expression => expression.MapFrom(source =>
                        source.InvitationId.Value.Replace("invitation-", "",
                            StringComparison.InvariantCultureIgnoreCase)))
                .ForMember(target => target.Email, expression => expression.MapFrom(source => source.Email.Value));
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private void MapEntitiesToViewModels()
        {
            this.CreateMap<InvitationReadModel, InvitationViewModel>()
                .ForMember(target => target.Id,
                    expression => expression.MapFrom(source =>
                        source.Id.Replace("invitation-", "", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void MapViewModelsToCommands()
        {
            this.CreateMap<InviteUserViewModel, InviteUserCommand>()
                .ConstructUsing(source =>
                    new InviteUserCommand(LocationId.With(Guid.Parse(source.Id)), new Email(source.Email)));
            this.CreateMap<DeleteInvitationViewModel, DeleteInvitationCommand>()
                .ConstructUsing(source => new DeleteInvitationCommand(LocationId.With(Guid.Parse(source.Id)),
                    InvitationId.With(Guid.Parse(source.Invitation))));
        }

        #endregion [ Private methods ]
    }
}
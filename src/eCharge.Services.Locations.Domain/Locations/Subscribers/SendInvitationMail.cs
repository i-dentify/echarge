#region [ COPYRIGHT ]

// <copyright file="SendInvitationMail.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-03-16</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Locations.Domain.Locations.Subscribers
{
    #region [ References ]

    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Core.Rendering;
    using ECharge.Messaging.Clients;
    using ECharge.Services.Locations.Domain.Locations.Events;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using ECharge.Services.Locations.Resources;
    using EventFlow.Aggregates;
    using EventFlow.Subscribers;
    using Invitation = ECharge.Services.Locations.Domain.Locations.ViewModels.Invitation;
    using Location = ECharge.Services.Locations.Domain.Locations.Aggregates.Location;

    #endregion

    public class SendInvitationMail : ISubscribeSynchronousTo<Location, LocationId, InvitationAdded>
    {
        private readonly IEmailClient emailClient;
        private readonly IMapper mapper;
        private readonly IViewRenderer viewRenderer;

        #region [ Constructor ]

        public SendInvitationMail(IViewRenderer viewRenderer, IEmailClient emailClient, IMapper mapper)
        {
            this.viewRenderer = viewRenderer;
            this.emailClient = emailClient;
            this.mapper = mapper;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task HandleAsync(IDomainEvent<Location, LocationId, InvitationAdded> domainEvent,
            CancellationToken cancellationToken)
        {
            await this.emailClient.SendAsync(new[]
                {
                    domainEvent.AggregateEvent.Email.Value
                }, LocationResources.InvitationEmailSubject,
                await this.viewRenderer.RenderAsync("MailTemplates/InviteUser",
                    this.mapper.Map<Invitation>(domainEvent.AggregateEvent), cancellationToken).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);
        }

        #endregion [ Public methods ]
    }
}
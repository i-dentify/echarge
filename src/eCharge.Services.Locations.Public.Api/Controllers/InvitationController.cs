#region [ COPYRIGHT ]

// <copyright file="InvitationController.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Public.Api.Controllers
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Services.Locations.Domain.Locations.Queries;
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using ECharge.Services.Locations.Domain.Locations.ViewModels;
    using ECharge.Services.Locations.Resources;
    using EventFlow;
    using EventFlow.Aggregates.ExecutionResults;
    using EventFlow.Queries;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using FluentValidation.Results;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Swashbuckle.AspNetCore.Annotations;
    using InviteUserCommand = ECharge.Services.Locations.Domain.Locations.Commands.InviteUser;
    using InviteUserViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.InviteUser;
    using DeleteInvitationCommand = ECharge.Services.Locations.Domain.Locations.Commands.DeleteInvitation;
    using DeleteInvitationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.DeleteInvitation;
    using AcceptInvitationCommand = ECharge.Services.Locations.Domain.Locations.Commands.AcceptInvitation;
    using AcceptInvitationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.AcceptInvitation;
    using Invitation = ECharge.Services.Locations.Domain.Locations.ViewModels.Invitation;
    using RejectInvitationCommand = ECharge.Services.Locations.Domain.Locations.Commands.RejectInvitation;
    using RejectInvitationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.RejectInvitation;

    #endregion

    public class InvitationController : Controller
    {
        #region [ Constructor ]

        public InvitationController(IMapper mapper, ICommandBus commandBus, IQueryProcessor queryProcessor,
            IValidator<InviteUserViewModel> inviteUserValidator,
            IValidator<DeleteInvitationViewModel> deleteInvitationValidator,
            IValidator<AcceptInvitationViewModel> acceptInvitationValidator,
            IValidator<RejectInvitationViewModel> rejectInvitationValidator)
        {
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.inviteUserValidator = inviteUserValidator;
            this.deleteInvitationValidator = deleteInvitationValidator;
            this.acceptInvitationValidator = acceptInvitationValidator;
            this.rejectInvitationValidator = rejectInvitationValidator;
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private async Task<Invitation> GetInvitationByRecipient(string location, string email,
            CancellationToken cancellationToken)
        {
            return await this.queryProcessor.ProcessAsync(new InvitationByRecipient(location, email), cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task<Invitation> GetInvitationByCode(string location, string code,
            CancellationToken cancellationToken)
        {
            return await this.queryProcessor
                .ProcessAsync(new InvitationById(location, code),
                    cancellationToken).ConfigureAwait(false);
        }

        private async Task<Location> GetLocationByInvitationCode(string code, CancellationToken cancellationToken)
        {
            return await this.queryProcessor
                .ProcessAsync(new LocationByInvitationId(code), cancellationToken)
                .ConfigureAwait(false);
        }

        #endregion [ Private methods ]

        #region [ Private attributes ]

        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;
        private readonly IValidator<InviteUserViewModel> inviteUserValidator;
        private readonly IValidator<DeleteInvitationViewModel> deleteInvitationValidator;
        private readonly IValidator<AcceptInvitationViewModel> acceptInvitationValidator;
        private readonly IValidator<RejectInvitation> rejectInvitationValidator;
        private readonly IMapper mapper;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getInvitations")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<Invitation>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/{id}/invitation")]
        public async Task<IActionResult> GetInvitations(string id)
        {
            return this.Ok(this.mapper.Map<List<Invitation>>(await this.queryProcessor
                .ProcessAsync(new InvitationsForLocation(id), CancellationToken.None).ConfigureAwait(false)));
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("addInvitation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Invitation), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/{id}/invitation")]
        public async Task<IActionResult> AddInvitation([CustomizeValidator(Skip = true)] [FromBody]
            InviteUserViewModel model)
        {
            ValidationResult validationResults = await this.inviteUserValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<InviteUserCommand>(model), CancellationToken.None)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.GetInvitationByRecipient(model.Id, model.Email, CancellationToken.None)
                .ConfigureAwait(false));
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation("deleteInvitation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/{id}/invitation/{invitation}")]
        public async Task<IActionResult> DeleteInvitation(
            [CustomizeValidator(Skip = true)] DeleteInvitationViewModel model)
        {
            ValidationResult validationResults = await this.deleteInvitationValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<DeleteInvitationCommand>(model), CancellationToken.None)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpPatch]
        [Authorize]
        [SwaggerOperation("acceptInvitation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Invitation), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/invitation/{code}/accept")]
        public async Task<IActionResult> AcceptInvitation([CustomizeValidator(Skip = true)] [FromBody]
            AcceptInvitationViewModel model)
        {
            ValidationResult validationResults = await this.acceptInvitationValidator
                .ValidateAsync(model ?? new AcceptInvitationViewModel(), CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Location location = await this.GetLocationByInvitationCode(model?.Code.ToString(), CancellationToken.None)
                .ConfigureAwait(false);

            if (location == null)
            {
                this.ModelState.AddModelError("Code", LocationResources.InvitationNotFound);
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(
                    new AcceptInvitationCommand(LocationId.With(Guid.Parse(location.Id)),
                        InvitationId.With(Guid.Parse(model.Code.ToString()))), CancellationToken.None)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.GetInvitationByCode(location.Id, model.Code.ToString(), CancellationToken.None)
                .ConfigureAwait(false));
        }

        [HttpPatch]
        [Authorize]
        [SwaggerOperation("rejectInvitation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Invitation), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/invitation/{code}/reject")]
        public async Task<IActionResult> RejectInvitation([CustomizeValidator(Skip = true)] [FromBody]
            RejectInvitationViewModel model)
        {
            ValidationResult validationResults = await this.rejectInvitationValidator
                .ValidateAsync(model ?? new RejectInvitationViewModel(), CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Location location = await this.GetLocationByInvitationCode(model?.Code.ToString(), CancellationToken.None)
                .ConfigureAwait(false);

            if (location == null)
            {
                this.ModelState.AddModelError("Code", LocationResources.InvitationNotFound);
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(
                    new RejectInvitationCommand(LocationId.With(Guid.Parse(location.Id)),
                        InvitationId.With(Guid.Parse(model.Code.ToString()))), CancellationToken.None)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.GetInvitationByCode(location.Id, model.Code.ToString(), CancellationToken.None)
                .ConfigureAwait(false));
        }

        #endregion [ Public methods ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="LocationController.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Public.Api.Controllers
{
    #region [ References ]

    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Services.Locations.Domain.Locations.Queries;
    using ECharge.Services.Locations.Domain.Locations.ViewModels;
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
    using AddLocationCommand = ECharge.Services.Locations.Domain.Locations.Commands.AddLocation;
    using AddLocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.AddLocation;
    using DeleteLocationCommand = ECharge.Services.Locations.Domain.Locations.Commands.DeleteLocation;
    using DeleteLocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.DeleteLocation;
    using EditLocationCommand = ECharge.Services.Locations.Domain.Locations.Commands.EditLocation;
    using EditLocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.EditLocation;
    using LocationsByIdsQuery = ECharge.Services.Locations.Domain.Locations.Queries.LocationsByIds;
    using LocationsByIdsViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.LocationsByIds;

    #endregion

    public class LocationController : Controller
    {
        #region [ Constructor ]

        public LocationController(IMapper mapper, ICommandBus commandBus, IQueryProcessor queryProcessor,
            IValidator<AddLocationViewModel> addLocationValidator,
            IValidator<EditLocationViewModel> editLocationValidator,
            IValidator<DeleteLocationViewModel> deleteLocationValidator)
        {
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.addLocationValidator = addLocationValidator;
            this.editLocationValidator = editLocationValidator;
            this.deleteLocationValidator = deleteLocationValidator;
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private async Task<Location> GetLocationById(string id, CancellationToken cancellationToken)
        {
            return this.mapper.Map<Location>(await this.queryProcessor
                .ProcessAsync(new LocationById(id), cancellationToken).ConfigureAwait(false));
        }

        #endregion [ Private methods ]

        #region [ Private attributes ]

        private readonly IValidator<AddLocationViewModel> addLocationValidator;
        private readonly IValidator<EditLocationViewModel> editLocationValidator;
        private readonly IValidator<DeleteLocationViewModel> deleteLocationValidator;
        private readonly ICommandBus commandBus;
        private readonly IMapper mapper;
        private readonly IQueryProcessor queryProcessor;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getLocations")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Location>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location")]
        public async Task<IActionResult> Index()
        {
            IReadOnlyCollection<Location> result = await this.queryProcessor
                .ProcessAsync(new LocationsForUser(), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getLocationsById")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Location>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/by-ids")]
        public async Task<IActionResult> LocationsByIds(LocationsByIdsViewModel model)
        {
            
            if (model.Ids == null || !model.Ids.Any())
            {
                return this.Ok(new List<Location>());
            }

            IReadOnlyCollection<Location> result = await this.queryProcessor
                .ProcessAsync(new LocationsByIdsQuery(model.Ids), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("addLocation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Location), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location")]
        public async Task<IActionResult> AddLocation([CustomizeValidator(Skip = true)] [FromBody]
            AddLocationViewModel model)
        {
            ValidationResult validationResults = await this.addLocationValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<AddLocationCommand>(model), CancellationToken.None).ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            Location location = await this.queryProcessor
                .ProcessAsync(new LocationByName(model.Name), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(location);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getLocation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Location), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/{id}")]
        public async Task<IActionResult> GetLocation(string id)
        {
            Location location = await this.GetLocationById(id, CancellationToken.None).ConfigureAwait(false);

            if (location == null)
            {
                return this.NotFound();
            }

            return this.Ok(location);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("editLocation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Location), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/{id}")]
        public async Task<IActionResult> EditLocation([CustomizeValidator(Skip = true)] [FromBody]
            EditLocationViewModel model)
        {
            ValidationResult validationResults = await this.editLocationValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<EditLocationCommand>(model), CancellationToken.None)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.GetLocationById(model.Id, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation("deleteLocation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/location/{id}")]
        public async Task<IActionResult> DeleteLocation([CustomizeValidator(Skip = true)] [FromBody]
            DeleteLocationViewModel model)
        {
            ValidationResult validationResults = await this.deleteLocationValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<DeleteLocationCommand>(model), CancellationToken.None)
                .ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        #endregion [ Public methods ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="CarController.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Public.Api.Controllers
{
    #region [ References ]

    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Services.Cars.Domain.Cars.Queries;
    using ECharge.Services.Cars.Domain.Cars.ViewModels;
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
    using AddCarCommand = ECharge.Services.Cars.Domain.Cars.Commands.AddCar;
    using AddCarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.AddCar;
    using DeleteCarCommand = ECharge.Services.Cars.Domain.Cars.Commands.DeleteCar;
    using DeleteCarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.DeleteCar;
    using EditCarCommand = ECharge.Services.Cars.Domain.Cars.Commands.EditCar;
    using EditCarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.EditCar;
    using CarsByIdsQuery = ECharge.Services.Cars.Domain.Cars.Queries.CarsByIds;
    using CarsByIdsViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.CarsByIds;

    #endregion

    public class CarController : Controller
    {
        #region [ Constructor ]

        public CarController(IMapper mapper, ICommandBus commandBus, IQueryProcessor queryProcessor,
            IValidator<AddCarViewModel> addCarValidator, IValidator<EditCarViewModel> editCarValidator,
            IValidator<DeleteCarViewModel> deleteCarValidator)
        {
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.addCarValidator = addCarValidator;
            this.editCarValidator = editCarValidator;
            this.deleteCarValidator = deleteCarValidator;
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private async Task<Car> GetCarById(string id, CancellationToken cancellationToken)
        {
            return this.mapper.Map<Car>(await this.queryProcessor.ProcessAsync(new CarById(id), cancellationToken)
                .ConfigureAwait(false));
        }

        #endregion [ Private methods ]

        #region [ Private attributes ]

        private readonly IValidator<AddCarViewModel> addCarValidator;
        private readonly IValidator<EditCarViewModel> editCarValidator;
        private readonly IValidator<DeleteCarViewModel> deleteCarValidator;
        private readonly ICommandBus commandBus;
        private readonly IMapper mapper;
        private readonly IQueryProcessor queryProcessor;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getCars")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Car>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/car")]
        public async Task<IActionResult> Index()
        {
            IReadOnlyCollection<Car> result = await this.queryProcessor
                .ProcessAsync(new CarsForUser(), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getCarsById")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Car>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/car/by-ids")]
        public async Task<IActionResult> CarsByIds(CarsByIdsViewModel model)
        {
            if (model.Ids == null || !model.Ids.Any())
            {
                return this.Ok(new List<Car>());
            }

            IReadOnlyCollection<Car> result = await this.queryProcessor
                .ProcessAsync(new CarsByIdsQuery(model.Ids), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("addCar")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Car), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/car")]
        public async Task<IActionResult> AddCar([CustomizeValidator(Skip = true)] [FromBody]
            AddCarViewModel model)
        {
            ValidationResult validationResults = await this.addCarValidator.ValidateAsync(model, CancellationToken.None)
                .ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<AddCarCommand>(model), CancellationToken.None).ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            Car car = await this.queryProcessor.ProcessAsync(new CarByName(model.Name), CancellationToken.None)
                .ConfigureAwait(false);
            return this.Ok(car);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getCar")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Car), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/car/{id}")]
        public async Task<IActionResult> GetCar(string id)
        {
            Car car = await this.GetCarById(id, CancellationToken.None).ConfigureAwait(false);

            if (car == null)
            {
                return this.NotFound();
            }

            return this.Ok(car);
        }

        [HttpPut]
        [Authorize]
        [SwaggerOperation("editCar")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Car), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/car/{id}")]
        public async Task<IActionResult> EditCar([CustomizeValidator(Skip = true)] [FromBody]
            EditCarViewModel model)
        {
            ValidationResult validationResults =
                await this.editCarValidator.ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<EditCarCommand>(model), CancellationToken.None).ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok(await this.GetCarById(model.Id, CancellationToken.None).ConfigureAwait(false));
        }

        [HttpDelete]
        [Authorize]
        [SwaggerOperation("deleteCar")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/car/{id}")]
        public async Task<IActionResult> DeleteCar([CustomizeValidator(Skip = true)] [FromBody]
            DeleteCarViewModel model)
        {
            ValidationResult validationResults = await this.deleteCarValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IExecutionResult result = await this.commandBus
                .PublishAsync(this.mapper.Map<DeleteCarCommand>(model), CancellationToken.None).ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        #endregion [ Public methods ]
    }
}
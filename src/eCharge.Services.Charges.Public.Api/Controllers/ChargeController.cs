#region [ COPYRIGHT ]

// <copyright file="ChargeController.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Public.Api.Controllers
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using ECharge.Services.Charges.Domain.Charges.Commands;
    using ECharge.Services.Charges.Domain.Charges.Queries;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using ECharge.Services.Charges.Domain.Charges.ViewModels;
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
    using AddChargeViewModel = ECharge.Services.Charges.Domain.Charges.ViewModels.AddCharge;
    using AddChargeCommand = ECharge.Services.Charges.Domain.Charges.Commands.AddCharge;

    #endregion

    public class ChargeController : Controller
    {
        #region [ Constructor ]

        public ChargeController(IMapper mapper, ICommandBus commandBus, IQueryProcessor queryProcessor,
            IValidator<AddChargeViewModel> addChargeValidator)
        {
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.addChargeValidator = addChargeValidator;
        }

        #endregion [ Constructor ]

        #region [ Private attributes ]

        private readonly ICommandBus commandBus;
        private readonly IMapper mapper;
        private readonly IQueryProcessor queryProcessor;
        private readonly IValidator<AddChargeViewModel> addChargeValidator;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getCharges")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Charge>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge")]
        public async Task<IActionResult> Index()
        {
            IReadOnlyCollection<Charge> result = await this.queryProcessor
                .ProcessAsync(new ChargesForUser(), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getChargingCostsPerMonth")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<KeyValuePair<string, decimal>>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge/costs-per-month")]
        public async Task<IActionResult> ChargingCostsPerMonth()
        {
            IReadOnlyCollection<Charge> charges = await this.queryProcessor
                .ProcessAsync(new ChargesForUser(), CancellationToken.None).ConfigureAwait(false);

            if (!charges.Any())
            {
                return this.Ok(new List<KeyValuePair<string, decimal>>());
            }

            List<KeyValuePair<string, decimal>> result = charges
                .GroupBy(charge => $"{charge.Date.Year:D4}-{charge.Date.Month:D2}").Select(group =>
                {
                    return new KeyValuePair<string, decimal>(group.Key,
                        Convert.ToDecimal(group.Sum(item =>
                            Convert.ToDouble(item.LoadEnd - item.LoadStart) / 100 * item.BatteryCapacity *
                            Convert.ToDouble(item.PricePerKw))));
                }).ToList();
            DateTime minDate = charges.Min(charge => charge.Date);
            DateTime maxDate = charges.Max(charge => charge.Date);
            List<string> months = Enumerable
                .Range(0, maxDate.Month + maxDate.Year * 12 - (minDate.Month + minDate.Year * 12))
                .Select(n => minDate.AddMonths(n)).TakeWhile(end => end <= maxDate)
                .Select(date => $"{date.Year:D4}-{date.Month:D2}").ToList();
            result.AddRange(months
                .Where(month =>
                    !result.Any(item => item.Key.Equals(month, StringComparison.InvariantCultureIgnoreCase)))
                .Select(month => new KeyValuePair<string, decimal>(month, 0)));
            return this.Ok(result.OrderBy(item => item.Key).ToList());
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getChargingCostsPerLocation")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<KeyValuePair<string, decimal>>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge/costs-per-location")]
        public async Task<IActionResult> ChargingCostsPerLocation()
        {
            IReadOnlyCollection<Charge> charges = await this.queryProcessor
                .ProcessAsync(new ChargesForUser(), CancellationToken.None).ConfigureAwait(false);
            List<KeyValuePair<string, decimal>> result = charges
                .GroupBy(charge => charge.Location).Select(group =>
                {
                    return new KeyValuePair<string, decimal>(group.Key,
                        Convert.ToDecimal(group.Sum(item =>
                            Convert.ToDouble(item.LoadEnd - item.LoadStart) / 100 * item.BatteryCapacity *
                            Convert.ToDouble(item.PricePerKw))));
                }).ToList();
            return this.Ok(result);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getChargingCostsPerLocationAndClearance")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<KeyValuePair<string, ChargingCostsByClearance>>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge/costs-per-location-and-clearance")]
        public async Task<IActionResult> ChargingCostsPerLocationAndClearance()
        {
            IReadOnlyCollection<Charge> charges = await this.queryProcessor
                .ProcessAsync(new ChargesForUser(), CancellationToken.None).ConfigureAwait(false);
            List<KeyValuePair<string, ChargingCostsByClearance>> result = charges.GroupBy(charge => charge.Location)
                .Select(group => new KeyValuePair<string, ChargingCostsByClearance>(group.Key,
                    new ChargingCostsByClearance
                    {
                        Open = Convert.ToDecimal(group.Where(item => !item.Cleared).Sum(item =>
                            Convert.ToDouble(item.LoadEnd - item.LoadStart) / 100 * item.BatteryCapacity *
                            Convert.ToDouble(item.PricePerKw))),
                        Cleared = Convert.ToDecimal(group.Where(item => item.Cleared).Sum(item =>
                            Convert.ToDouble(item.LoadEnd - item.LoadStart) / 100 * item.BatteryCapacity *
                            Convert.ToDouble(item.PricePerKw)))
                    })).ToList();
            return this.Ok(result);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getChargingCostsPerCar")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<KeyValuePair<string, decimal>>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge/costs-per-car")]
        public async Task<IActionResult> ChargingCostsPerCar()
        {
            IReadOnlyCollection<Charge> charges = await this.queryProcessor
                .ProcessAsync(new ChargesForUser(), CancellationToken.None).ConfigureAwait(false);
            List<KeyValuePair<string, decimal>> result = charges
                .GroupBy(charge => charge.Car).Select(group =>
                {
                    return new KeyValuePair<string, decimal>(group.Key,
                        Convert.ToDecimal(group.Sum(item =>
                            Convert.ToDouble(item.LoadEnd - item.LoadStart) / 100 * item.BatteryCapacity *
                            Convert.ToDouble(item.PricePerKw))));
                }).ToList();
            return this.Ok(result);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getChargesByLocationAndUser")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Charge>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge/{location}/by-user/{user}")]
        public async Task<IActionResult> ChargesByLocationAndUser(string location, string user)
        {
            IReadOnlyCollection<Charge> result = await this.queryProcessor
                .ProcessAsync(new ChargesByLocationAndUser(location, user), CancellationToken.None)
                .ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("getChargesByLocationAndCar")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(List<Charge>), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge/{location}/by-car/{car}")]
        public async Task<IActionResult> ChargesByLocationAndCar(string location, string car)
        {
            IReadOnlyCollection<Charge> result = await this.queryProcessor
                .ProcessAsync(new ChargesForUserByLocationAndCar(location, car), CancellationToken.None)
                .ConfigureAwait(false);
            return this.Ok(result.ToList());
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation("addCharge")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Charge), (int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge")]
        public async Task<IActionResult> AddCharge([CustomizeValidator(Skip = true)] [FromBody]
            AddChargeViewModel model)
        {
            ValidationResult validationResults = await this.addChargeValidator
                .ValidateAsync(model, CancellationToken.None).ConfigureAwait(false);
            validationResults.AddToModelState(this.ModelState, null);

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            AddChargeCommand command = this.mapper.Map<AddChargeCommand>(model);
            IExecutionResult result =
                await this.commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

            if (!result.IsSuccess)
            {
                return this.BadRequest();
            }

            Charge charge = await this.queryProcessor
                .ProcessAsync(
                    new ChargeById(command.AggregateId.Value.Replace("charge-", "",
                        StringComparison.InvariantCultureIgnoreCase)), CancellationToken.None).ConfigureAwait(false);
            return this.Ok(charge);
        }

        [HttpPatch]
        [Authorize]
        [SwaggerOperation("closeCharges")]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [MapToApiVersion("1.0")]
        [Route("api/{version:apiVersion}/charge")]
        public async Task<IActionResult> ChargesByLocationAndUser([CustomizeValidator(Skip = true)] [FromBody]
            CloseCharges model)
        {
            await Task.WhenAll(model.Charges.Select(async id =>
                await this.commandBus
                    .PublishAsync(new CloseCharge(ChargeId.With(Guid.Parse(id))), CancellationToken.None)
                    .ConfigureAwait(false))).ConfigureAwait(false);
            return this.Ok();
        }

        #endregion [ Public methods ]
    }
}
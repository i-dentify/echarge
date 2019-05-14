#region [ COPYRIGHT ]

// <copyright file="Charge.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Mapping.Profiles
{
    #region [ References ]

    using System;
    using AutoMapper;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using AddChargeCommand = ECharge.Services.Charges.Domain.Charges.Commands.AddCharge;
    using AddChargeViewModel = ECharge.Services.Charges.Domain.Charges.ViewModels.AddCharge;
    using ChargeReadModel = ECharge.Services.Charges.Domain.Charges.ReadModels.Charge;
    using ChargeViewModel = ECharge.Services.Charges.Domain.Charges.ViewModels.Charge;

    #endregion

    public class Charge : Profile
    {
        #region [ Constructor ]

        public Charge()
        {
            this.MapEntitiesToViewModels();
            this.MapViewModelsToCommands();
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private void MapEntitiesToViewModels()
        {
            this.CreateMap<ChargeReadModel, ChargeViewModel>()
                .ForMember(target => target.Id,
                    expression => expression.MapFrom(source =>
                        source.Id.Replace("charge-", "", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void MapViewModelsToCommands()
        {
            this.CreateMap<AddChargeViewModel, AddChargeCommand>()
                .ConstructUsing(source => new AddChargeCommand(ChargeId.NewComb(), source.Location, source.Car,
                    source.Date, source.LoadStart, source.LoadEnd, source.PricePerKw, source.BatteryCapacity));
        }

        #endregion [ Private methods ]
    }
}
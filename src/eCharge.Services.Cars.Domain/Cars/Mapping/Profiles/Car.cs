#region [ COPYRIGHT ]

// <copyright file="Car.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Cars.Domain.Cars.Mapping.Profiles
{
    #region [ References ]

    using System;
    using AutoMapper;
    using ECharge.Services.Cars.Domain.Cars.ValueObjects;
    using AddCarCommand = ECharge.Services.Cars.Domain.Cars.Commands.AddCar;
    using AddCarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.AddCar;
    using DeleteCarCommand = ECharge.Services.Cars.Domain.Cars.Commands.DeleteCar;
    using DeleteCarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.DeleteCar;
    using EditCarCommand = ECharge.Services.Cars.Domain.Cars.Commands.EditCar;
    using EditCarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.EditCar;
    using CarReadModel = ECharge.Services.Cars.Domain.Cars.ReadModels.Car;
    using CarViewModel = ECharge.Services.Cars.Domain.Cars.ViewModels.Car;

    #endregion

    public class Car : Profile
    {
        #region [ Constructor ]

        public Car()
        {
            this.MapEntitiesToViewModels();
            this.MapViewModelsToCommands();
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private void MapEntitiesToViewModels()
        {
            this.CreateMap<CarReadModel, CarViewModel>()
                .ForMember(target => target.Id,
                    expression => expression.MapFrom(source =>
                        source.Id.Replace("car-", "", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void MapViewModelsToCommands()
        {
            this.CreateMap<AddCarViewModel, AddCarCommand>()
                .ConstructUsing(source => new AddCarCommand(CarId.NewComb(), source.Name, source.BatteryCapacity));
            this.CreateMap<EditCarViewModel, EditCarCommand>()
                .ConstructUsing(source =>
                    new EditCarCommand(CarId.With(Guid.Parse(source.Id)), source.Name, source.BatteryCapacity));
            this.CreateMap<DeleteCarViewModel, DeleteCarCommand>()
                .ConstructUsing(source => new DeleteCarCommand(CarId.With(Guid.Parse(source.Id))));
        }

        #endregion [ Private methods ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="Location.cs" company="i-dentify Software Development">
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
    using ECharge.Services.Locations.Domain.Locations.ValueObjects;
    using AddLocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.AddLocation;
    using AddLocationCommand = ECharge.Services.Locations.Domain.Locations.Commands.AddLocation;
    using DeleteLocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.DeleteLocation;
    using DeleteLocationCommand = ECharge.Services.Locations.Domain.Locations.Commands.DeleteLocation;
    using EditLocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.EditLocation;
    using EditLocationCommand = ECharge.Services.Locations.Domain.Locations.Commands.EditLocation;
    using LocationReadModel = ECharge.Services.Locations.Domain.Locations.ReadModels.Location;
    using LocationViewModel = ECharge.Services.Locations.Domain.Locations.ViewModels.Location;

    #endregion

    public class Location : Profile
    {
        #region [ Constructor ]

        public Location()
        {
            this.MapEntitiesToViewModels();
            this.MapViewModelsToCommands();
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private void MapEntitiesToViewModels()
        {
            this.CreateMap<LocationReadModel, LocationViewModel>()
                .ForMember(target => target.Id,
                    expression => expression.MapFrom(source =>
                        source.Id.Replace("location-", "", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void MapViewModelsToCommands()
        {
            this.CreateMap<AddLocationViewModel, AddLocationCommand>()
                .ConstructUsing(source => new AddLocationCommand(LocationId.NewComb(), source.Name, source.Address,
                    new Latitude(source.Latitude), new Longitude(source.Longitude), source.PricePerKw));
            this.CreateMap<EditLocationViewModel, EditLocationCommand>()
                .ConstructUsing(source => new EditLocationCommand(LocationId.With(Guid.Parse(source.Id)), source.Name,
                    source.Address, new Latitude(source.Latitude), new Longitude(source.Longitude), source.PricePerKw));
            this.CreateMap<DeleteLocationViewModel, DeleteLocationCommand>()
                .ConstructUsing(source => new DeleteLocationCommand(LocationId.With(Guid.Parse(source.Id))));
        }

        #endregion [ Private methods ]
    }
}
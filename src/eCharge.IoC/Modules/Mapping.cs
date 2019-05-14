#region [ COPYRIGHT ]

// <copyright file="Mapping.cs" company="i-dentify Software Development">
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

namespace ECharge.IoC.Modules
{
    #region [ References ]

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using AutoMapper;
    using Module = Autofac.Module;

    #endregion

    public class Mapping : Module
    {
        #region [ Private attributes ]

        private readonly Assembly[] assemblies;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public Mapping(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies?.ToArray();
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.assemblies)
                .AsClosedTypesOf(typeof(ITypeConverter<,>))
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(this.assemblies)
                .AsClosedTypesOf(typeof(IValueResolver<,,>))
                .AsSelf()
                .InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(this.assemblies)
                .Where(type => typeof(Profile).IsAssignableFrom(type))
                .As<Profile>()
                .InstancePerLifetimeScope();

            builder.Register(context =>
            {
                IEnumerable<Profile> profiles = context.Resolve<IEnumerable<Profile>>();
                MapperConfiguration configuration = new MapperConfiguration(config =>
                    {
                        foreach (Profile profile in profiles)
                        {
                            config.AddProfile(profile);
                        }
                    }
                );

                return configuration;
            }).SingleInstance().AutoActivate().AsSelf();

            builder.Register(context =>
            {
                IComponentContext componentContext = context.Resolve<IComponentContext>();
                MapperConfiguration configuration = componentContext.Resolve<MapperConfiguration>();
                return configuration.CreateMapper(type => componentContext.Resolve(type));
            });
        }

        #endregion [ Protected methods ]
    }
}
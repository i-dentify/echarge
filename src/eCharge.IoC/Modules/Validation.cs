#region [ COPYRIGHT ]

// <copyright file="Validation.cs" company="i-dentify Software Development">
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
    using FluentValidation;
    using Module = Autofac.Module;

    #endregion

    public class Validation : Module
    {
        #region [ Private attributes ]

        private readonly Assembly[] assemblies;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public Validation(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies?.ToArray();
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(this.assemblies)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        #endregion [ Protected methods ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="Program.cs" company="i-dentify Software Development">
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

namespace ECharge.Public.Frontend
{
    #region [ References ]

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    #endregion

    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => options.AddServerHeader = false)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
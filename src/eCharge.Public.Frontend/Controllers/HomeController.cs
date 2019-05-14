#region [ COPYRIGHT ]

// <copyright file="HomeController.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-03-19</date>
// <summary></summary>

#endregion

namespace ECharge.Public.Frontend.Controllers
{
    #region [ References ]

    using System.Net;
    using ECharge.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    #endregion

    public class HomeController : Controller
    {
        #region [ Private attributes ]

        private readonly IOptions<ApplicationSettings> applicationSettings;

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public HomeController(IOptions<ApplicationSettings> applicationSettings)
        {
            this.applicationSettings = applicationSettings;
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        [Route("")]
        [Route("home")]
        [Route("home/index")]
        public IActionResult Index()
        {
            return this.View();
        }

        [ProducesResponseType(typeof(ApiSettings), (int) HttpStatusCode.OK)]
        [Route("configuration")]
        [Route("home/configuration")]
        public IActionResult Config()
        {
            return this.Ok(this.applicationSettings.Value.Api);
        }

        #endregion [ Public methods ]
    }
}
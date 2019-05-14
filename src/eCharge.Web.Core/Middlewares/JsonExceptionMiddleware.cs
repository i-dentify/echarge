#region [ COPYRIGHT ]

// <copyright file="JsonExceptionMiddleware.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-12-28</date>
// <summary></summary>

#endregion

namespace ECharge.Web.Core.Middlewares
{
    #region [ References ]

    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using ECharge.Web.Core.Models;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    #endregion

    public sealed class JsonExceptionMiddleware
    {
        #region [ Constructor ]

        public JsonExceptionMiddleware(IHostingEnvironment env)
        {
            this.env = env;
            this.serializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        }

        #endregion [ Constructor ]

        #region [ Public methods ]

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            Exception ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (ex == null)
            {
                return;
            }

            ApiError error = BuildError(ex, this.env);

            using (StreamWriter writer = new StreamWriter(context.Response.Body))
            {
                this.serializer.Serialize(writer, error);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }

        #endregion [ Public methods ]

        #region [ Private methods ]

        private static ApiError BuildError(Exception ex, IHostingEnvironment env)
        {
            ApiError error = new ApiError();

            if (env.IsDevelopment())
            {
                error.Message = ex.Message;
                error.Detail = ex.StackTrace;
            }
            else
            {
                error.Message = DefaultErrorMessage;
                error.Detail = ex.Message;
            }

            return error;
        }

        #endregion [ Private methods ]

        #region [ Private attributes ]

        private const string DefaultErrorMessage = "A server error occurred.";
        private readonly IHostingEnvironment env;
        private readonly JsonSerializer serializer;

        #endregion [ Private attributes ]
    }
}
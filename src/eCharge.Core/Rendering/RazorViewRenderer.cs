#region [ COPYRIGHT ]

// <copyright file="RazorViewRenderer.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-03-16</date>
// <summary></summary>

#endregion

namespace ECharge.Core.Rendering
{
    #region [ References ]

    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;

    #endregion

    public class RazorViewRenderer : IViewRenderer
    {
        #region [ Constructor ]

        public RazorViewRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
        }

        #endregion [ Constructor ]

        #region [ Private methods ]

        private ActionContext GetActionContext()
        {
            DefaultHttpContext httpContext = new DefaultHttpContext
            {
                RequestServices = this.serviceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        #endregion [ Private methods ]

        #region [ Private attributes ]

        private readonly IServiceProvider serviceProvider;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IRazorViewEngine viewEngine;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        public async Task<string> RenderAsync(string name, CancellationToken cancellationToken)
        {
            return await this.RenderAsync<object>(name, null, cancellationToken);
        }

        public async Task<string> RenderAsync<T>(string name, T model, CancellationToken cancellationToken)
        {
            ActionContext actionContext = this.GetActionContext();
            ViewEngineResult viewEngineResult = this.viewEngine.FindView(actionContext, name, false);

            if (!viewEngineResult.Success)
            {
                throw new InvalidOperationException($"Couldn't find view '{name}'");
            }

            IView view = viewEngineResult.View;

            using (StringWriter output = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(actionContext, view,
                    new ViewDataDictionary<T>(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext, this.tempDataProvider), output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);
                return output.ToString();
            }
        }

        #endregion [ Public methods ]
    }
}
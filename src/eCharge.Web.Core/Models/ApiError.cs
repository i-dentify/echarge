#region [ COPYRIGHT ]

// <copyright file="ApiError.cs" company="i-dentify Software Development">
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

namespace ECharge.Web.Core.Models
{
    #region [ References ]

    using System.ComponentModel;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    #endregion

    public sealed class ApiError
    {
        #region [ Private attributes ]

        private const string ModelBindingErrorMessage = "Invalid parameters.";

        #endregion [ Private attributes ]

        #region [ Constructor ]

        public ApiError()
        {
        }

        public ApiError(string message)
        {
            this.Message = message;
        }

        /// <summary>
        ///     Creates a new <see cref="ApiError" /> from the result of a model binding attempt.
        ///     The first model binding error (if any) is placed in the <see cref="Detail" /> property.
        /// </summary>
        /// <param name="modelState"></param>
        public ApiError(ModelStateDictionary modelState)
        {
            this.Message = ModelBindingErrorMessage;
            this.Detail = modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value?.Errors?.FirstOrDefault()
                ?.ErrorMessage;
        }

        public string Message { get; set; }

        #endregion [ Constructor ]

        #region [ Public properties ]

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; }

        #endregion [ Public properties ]
    }
}
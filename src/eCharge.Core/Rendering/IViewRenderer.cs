#region [ COPYRIGHT ]

// <copyright file="IViewRenderer.cs" company="i-dentify Software Development">
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

    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IViewRenderer
    {
        #region [ Methods ]

        Task<string> RenderAsync(string name, CancellationToken cancellationToken);
        Task<string> RenderAsync<T>(string name, T model, CancellationToken cancellationToken);

        #endregion [ Methods ]
    }
}
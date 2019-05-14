#region [ COPYRIGHT ]

// <copyright file="IEmailClient.cs" company="i-dentify Software Development">
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

namespace ECharge.Messaging.Clients
{
    #region [ References ]

    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    public interface IEmailClient
    {
        #region [ Methods ]

        string Send(IEnumerable<string> destination, string subject, string body,
            Dictionary<string, Stream> attachments = null);

        Task<string> SendAsync(IEnumerable<string> destination, string subject, string body,
            CancellationToken cancellationToken, Dictionary<string, Stream> attachments = null);

        #endregion [ Methods ]
    }
}
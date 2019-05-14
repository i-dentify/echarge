#region [ COPYRIGHT ]

// <copyright file="SendGridOptions.cs" company="i-dentify Software Development">
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

namespace ECharge.Messaging.Plugins.Email.SendGrid
{
    public class SendGridOptions
    {
        /// <summary>
        ///     Gets or sets the SendGrid API Key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Gets or sets the sender email address.
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        ///     Gets or sets the sender name.
        /// </summary>
        public string SenderName { get; set; }
    }
}
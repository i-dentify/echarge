#region [ COPYRIGHT ]

// <copyright file="SendGridClient.cs" company="i-dentify Software Development">
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

namespace ECharge.Messaging.Plugins.Email.SendGrid
{
    #region [ References ]

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Messaging.Clients;
    using global::SendGrid.Helpers.Mail;

    #endregion

    public class SendGridClient : IEmailClient
    {
        #region [ Constructor ]

        public SendGridClient(SendGridOptions options)
        {
            this.client = new global::SendGrid.SendGridClient(options.ApiKey);
            this.sender = new EmailAddress(options.SenderAddress, options.SenderName);
        }

        #endregion [ Constructor ]

        #region [ Private attributes ]

        private readonly global::SendGrid.SendGridClient client;
        private readonly EmailAddress sender;

        #endregion [ Private attributes ]

        #region [ Public methods ]

        public string Send(IEnumerable<string> destination, string subject, string body,
            Dictionary<string, Stream> attachments = null)
        {
            SendGridMessage message = new SendGridMessage
            {
                From = this.sender,
                Subject = subject,
                HtmlContent = body
            };
            message.AddTos(destination.Select(address => new EmailAddress(address)).ToList());

            if (attachments == null)
            {
                return Task.Run(() => this.client.SendEmailAsync(message)).GetAwaiter().GetResult().StatusCode
                    .ToString();
            }

            foreach (KeyValuePair<string, Stream> attachment in attachments)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    attachment.Value.CopyTo(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    message.AddAttachment(attachment.Key, Convert.ToBase64String(stream.ToArray()));
                }
            }

            return Task.Run(() => this.client.SendEmailAsync(message)).GetAwaiter().GetResult().StatusCode.ToString();
        }

        public async Task<string> SendAsync(IEnumerable<string> destination, string subject, string body,
            CancellationToken cancellationToken, Dictionary<string, Stream> attachments = null)
        {
            SendGridMessage message = new SendGridMessage
            {
                From = this.sender,
                Subject = subject,
                HtmlContent = body
            };
            message.AddTos(destination.Select(address => new EmailAddress(address)).ToList());

            if (attachments == null)
            {
                return (await this.client.SendEmailAsync(message, cancellationToken).ConfigureAwait(false)).StatusCode
                    .ToString();
            }

            foreach ((string key, Stream value) in attachments)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await value.CopyToAsync(stream, cancellationToken).ConfigureAwait(false);
                    stream.Seek(0, SeekOrigin.Begin);
                    message.AddAttachment(key, Convert.ToBase64String(stream.ToArray()));
                }
            }

            return (await this.client.SendEmailAsync(message, cancellationToken).ConfigureAwait(false)).StatusCode
                .ToString();
        }

        #endregion [ Public methods ]
    }
}
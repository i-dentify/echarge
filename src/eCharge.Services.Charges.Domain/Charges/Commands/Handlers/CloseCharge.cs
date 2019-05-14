#region [ COPYRIGHT ]

// <copyright file="CloseCharge.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Charges.Domain.Charges.Commands.Handlers
{
    #region [ References ]

    using System.Threading;
    using System.Threading.Tasks;
    using ECharge.Services.Charges.Domain.Charges.Aggregates;
    using ECharge.Services.Charges.Domain.Charges.ValueObjects;
    using EventFlow.Commands;

    #endregion

    public class CloseCharge : CommandHandler<Charge, ChargeId, Charges.Commands.CloseCharge>
    {
        #region [ Public methods ]

        public override Task ExecuteAsync(Charge aggregate, Charges.Commands.CloseCharge command,
            CancellationToken cancellationToken)
        {
            aggregate.Close();
            return Task.FromResult(0);
        }

        #endregion [ Public methods ]
    }
}
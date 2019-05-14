#region [ COPYRIGHT ]

// <copyright file="ApplicationContext.cs" company="i-dentify Software Development">
// Copyright (c) 2018 i-dentify Software Development (https://www.i-dentify.de) - All Rights Reserved.
// 
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// 
// </copyright>
// <author>Mario Adam</author>
// <email>mail@i-dentify.de</email>
// <date>2018-12-30</date>
// <summary></summary>

#endregion

namespace ECharge.Services.Cars.Domain.Data.Persistence.Context
{
    #region [ References ]

    using ECharge.Services.Cars.Domain.Cars.ReadModels;
    using EventFlow.EntityFramework.Extensions;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public class ApplicationContext : DbContext, IDbContext
    {
        #region [ Constructor ]

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        #endregion [ Constructor ]

        #region [ Protected methods ]

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddEventFlowEvents()
                .AddEventFlowSnapshots();
            
            #region [ Cars ]

            modelBuilder.Entity<Car>(model =>
            {
                model.ToTable("Cars");

                model.HasKey(entity => entity.Id);
                
                model.HasIndex(entity => entity.Version)
                    .HasName("IX_Car_Version");

                model.Property(entity => entity.Version)
                    .IsRequired()
                    .IsConcurrencyToken()
                    .HasColumnName("Version");

                model.Property(entity => entity.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name");
                
                model.HasIndex(entity => entity.Owner)
                    .HasName("IX_Car_Owner");

                model.Property(entity => entity.Owner)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("Owner");

                model.Property(entity => entity.BatteryCapacity)
                    .IsRequired()
                    .HasColumnName("BatteryCapacity");
            });

            #endregion [ Cars ]
        }

        #endregion [ Protected methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Car" /> entities.
        /// </summary>
        public virtual DbSet<Car> Cars { get; set; }

        #endregion [ Public properties ]
    }
}
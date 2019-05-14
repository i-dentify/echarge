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

namespace ECharge.Services.Charges.Domain.Data.Persistence.Context
{
    #region [ References ]

    using ECharge.Services.Charges.Domain.Charges.ReadModels;
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
            
            #region [ Charges ]

            modelBuilder.Entity<Charge>(model =>
            {
                model.ToTable("Charges");

                model.HasKey(entity => entity.Id);
                
                model.HasIndex(entity => entity.Version)
                    .HasName("IX_Charge_Version");

                model.Property(entity => entity.Version)
                    .IsRequired()
                    .IsConcurrencyToken()
                    .HasColumnName("Version");

                model.HasIndex(entity => entity.Owner)
                    .HasName("IX_Charge_Owner");

                model.Property(entity => entity.Owner)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("Owner");

                model.HasIndex(entity => entity.Location)
                    .HasName("IX_Charge_Location");

                model.Property(entity => entity.Location)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("Location");

                model.HasIndex(entity => entity.Car)
                    .HasName("IX_Charge_Car");

                model.Property(entity => entity.Car)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("Car");
                
                model.HasIndex(entity => entity.Date)
                    .HasName("IX_Charge_Date");

                model.Property(entity => entity.Date)
                    .IsRequired()
                    .HasColumnName("Date");

                model.Property(entity => entity.LoadStart)
                    .IsRequired()
                    .HasColumnName("LoadStart");
                
                model.Property(entity => entity.LoadEnd)
                    .IsRequired()
                    .HasColumnName("LoadEnd");

                model.Property(entity => entity.PricePerKw)
                    .IsRequired()
                    .HasColumnName("PricePerKw");
                
                model.Property(entity => entity.BatteryCapacity)
                    .IsRequired()
                    .HasColumnName("BatteryCapacity");
                
                model.Property(entity => entity.Cleared)
                    .HasColumnName("Cleared");
            });

            #endregion [ Charges ]
        }

        #endregion [ Protected methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Charge" /> entities.
        /// </summary>
        public virtual DbSet<Charge> Charges { get; set; }

        #endregion [ Public properties ]
    }
}
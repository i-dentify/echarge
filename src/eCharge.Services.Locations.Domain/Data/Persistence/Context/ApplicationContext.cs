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

namespace ECharge.Services.Locations.Domain.Data.Persistence.Context
{
    #region [ References ]

    using ECharge.Services.Locations.Domain.Locations.ReadModels;
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
            
            #region [ Locations ]

            modelBuilder.Entity<Location>(model =>
            {
                model.ToTable("Locations");

                model.HasKey(entity => entity.Id);
                
                model.HasIndex(entity => entity.Version)
                    .HasName("IX_Location_Version");

                model.Property(entity => entity.Version)
                    .IsRequired()
                    .IsConcurrencyToken()
                    .HasColumnName("Version");

                model.Property(entity => entity.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Name");
                
                model.Property(entity => entity.Address)
                    .HasMaxLength(255)
                    .HasColumnName("Address");

                model.Property(entity => entity.Latitude)
                    .IsRequired()
                    .HasColumnName("Latitude");

                model.Property(entity => entity.Longitude)
                    .IsRequired()
                    .HasColumnName("Longitude");

                model.Property(entity => entity.PricePerKw)
                    .IsRequired()
                    .HasColumnName("PricePerKw");

                model.HasIndex(entity => entity.Owner)
                    .HasName("IX_Location_Owner");

                model.Property(entity => entity.Owner)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("Owner");
            });

            #endregion [ Locations ]
            
            #region [ Invitations ]

            modelBuilder.Entity<Invitation>(model =>
            {
                model.ToTable("Locations__Invitations");

                model.HasKey(entity => entity.Id);
                
                model.HasIndex(entity => entity.LocationId)
                    .HasName("IX_Location_Invitation_LocationId");

                model.Property(entity => entity.LocationId)
                    .IsRequired()
                    .HasColumnName("LocationId");

                model.Property(entity => entity.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Email");

                model.Property(entity => entity.User)
                    .HasMaxLength(64)
                    .HasColumnName("User");

                model.Property(entity => entity.Accepted)
                    .HasColumnName("Accepted");

                model.HasOne(entity => entity.Location)
                    .WithMany(entity => entity.Invitations)
                    .HasForeignKey(entity => entity.LocationId);
            });

            #endregion [ Invitations ]
        }

        #endregion [ Protected methods ]

        #region [ Public properties ]

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Location" /> entities.
        /// </summary>
        public virtual DbSet<Location> Locations { get; set; }

        /// <summary>
        ///     Gets or sets the entity set for <see cref="Invitation" /> entities.
        /// </summary>
        public virtual DbSet<Invitation> LocationInvitations { get; set; }

        #endregion [ Public properties ]
    }
}
#region [ COPYRIGHT ]

// <copyright file="ApplicationContextModelSnapshot.cs" company="i-dentify Software Development">
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

namespace ECharge.Services.Locations.Domain.Data.Persistence.Migrations
{
    #region [ References ]

    using System;
    using ECharge.Services.Locations.Domain.Data.Persistence.Context;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    #endregion

    [DbContext(typeof(ApplicationContext))]
    internal class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ECharge.Services.Locations.Domain.Locations.ReadModels.Invitation", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("Accepted")
                    .HasColumnName("Accepted");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(255);

                b.Property<string>("LocationId")
                    .IsRequired()
                    .HasColumnName("LocationId");

                b.Property<string>("User")
                    .HasColumnName("User")
                    .HasMaxLength(64);

                b.HasKey("Id");

                b.HasIndex("LocationId")
                    .HasName("IX_Location_Invitation_LocationId");

                b.ToTable("Locations__Invitations");
            });

            modelBuilder.Entity("ECharge.Services.Locations.Domain.Locations.ReadModels.Location", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Address")
                    .HasColumnName("Address")
                    .HasMaxLength(255);

                b.Property<double>("Latitude")
                    .HasColumnName("Latitude");

                b.Property<double>("Longitude")
                    .HasColumnName("Longitude");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(255);

                b.Property<string>("Owner")
                    .IsRequired()
                    .HasColumnName("Owner")
                    .HasMaxLength(64);

                b.Property<decimal>("PricePerKw")
                    .HasColumnName("PricePerKw");

                b.Property<long>("Version")
                    .IsConcurrencyToken()
                    .HasColumnName("Version");

                b.HasKey("Id");

                b.HasIndex("Owner")
                    .HasName("IX_Location_Owner");

                b.HasIndex("Version")
                    .HasName("IX_Location_Version");

                b.ToTable("Locations");
            });

            modelBuilder.Entity("EventFlow.EntityFramework.EventStores.EventEntity", b =>
            {
                b.Property<long>("GlobalSequenceNumber")
                    .ValueGeneratedOnAdd();

                b.Property<string>("AggregateId");

                b.Property<string>("AggregateName");

                b.Property<int>("AggregateSequenceNumber");

                b.Property<Guid>("BatchId");

                b.Property<string>("Data");

                b.Property<string>("Metadata");

                b.HasKey("GlobalSequenceNumber");

                b.HasIndex("AggregateId", "AggregateSequenceNumber")
                    .IsUnique();

                b.ToTable("EventEntity");
            });

            modelBuilder.Entity("EventFlow.EntityFramework.SnapshotStores.SnapshotEntity", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("AggregateId");

                b.Property<string>("AggregateName");

                b.Property<int>("AggregateSequenceNumber");

                b.Property<string>("Data");

                b.Property<string>("Metadata");

                b.HasKey("Id");

                b.HasIndex("AggregateName", "AggregateId", "AggregateSequenceNumber")
                    .IsUnique();

                b.ToTable("SnapshotEntity");
            });

            modelBuilder.Entity("ECharge.Services.Locations.Domain.Locations.ReadModels.Invitation", b =>
            {
                b.HasOne("ECharge.Services.Locations.Domain.Locations.ReadModels.Location", "Location")
                    .WithMany("Invitations")
                    .HasForeignKey("LocationId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}
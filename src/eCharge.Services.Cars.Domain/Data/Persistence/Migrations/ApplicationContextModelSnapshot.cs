﻿#region [ COPYRIGHT ]

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

namespace ECharge.Services.Cars.Domain.Data.Persistence.Migrations
{
    #region [ References ]

    using System;
    using ECharge.Services.Cars.Domain.Data.Persistence.Context;
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

            modelBuilder.Entity("ECharge.Services.Cars.Domain.Cars.ReadModels.Car", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("BatteryCapacity")
                    .HasColumnName("BatteryCapacity");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(255);

                b.Property<string>("Owner")
                    .IsRequired()
                    .HasColumnName("Owner")
                    .HasMaxLength(64);

                b.Property<long>("Version")
                    .IsConcurrencyToken()
                    .HasColumnName("Version");

                b.HasKey("Id");

                b.HasIndex("Owner")
                    .HasName("IX_Car_Owner");

                b.HasIndex("Version")
                    .HasName("IX_Car_Version");

                b.ToTable("Cars");
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
#pragma warning restore 612, 618
        }
    }
}
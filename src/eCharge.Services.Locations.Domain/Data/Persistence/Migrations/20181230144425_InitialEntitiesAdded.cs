#region [ COPYRIGHT ]

// <copyright file="20181230144425_InitialEntitiesAdded.cs" company="i-dentify Software Development">
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
    using Microsoft.EntityFrameworkCore.Migrations;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    #endregion

    public partial class InitialEntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "EventEntity",
                table => new
                {
                    GlobalSequenceNumber = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BatchId = table.Column<Guid>(nullable: false),
                    AggregateName = table.Column<string>(nullable: true),
                    AggregateId = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Metadata = table.Column<string>(nullable: true),
                    AggregateSequenceNumber = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_EventEntity", x => x.GlobalSequenceNumber); });

            migrationBuilder.CreateTable(
                "Locations",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Version = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    PricePerKw = table.Column<decimal>(nullable: false),
                    Owner = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Locations", x => x.Id); });

            migrationBuilder.CreateTable(
                "SnapshotEntity",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AggregateId = table.Column<string>(nullable: true),
                    AggregateName = table.Column<string>(nullable: true),
                    AggregateSequenceNumber = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Metadata = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SnapshotEntity", x => x.Id); });

            migrationBuilder.CreateTable(
                "Locations__Invitations",
                table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LocationId = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    User = table.Column<string>(maxLength: 64, nullable: true),
                    Accepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations__Invitations", x => x.Id);
                    table.ForeignKey(
                        "FK_Locations__Invitations_Locations_LocationId",
                        x => x.LocationId,
                        "Locations",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_EventEntity_AggregateId_AggregateSequenceNumber",
                "EventEntity",
                new[] { "AggregateId", "AggregateSequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Location_Owner",
                "Locations",
                "Owner");

            migrationBuilder.CreateIndex(
                "IX_Location_Version",
                "Locations",
                "Version");

            migrationBuilder.CreateIndex(
                "IX_Location_Invitation_LocationId",
                "Locations__Invitations",
                "LocationId");

            migrationBuilder.CreateIndex(
                "IX_SnapshotEntity_AggregateName_AggregateId_AggregateSequenceN~",
                "SnapshotEntity",
                new[] { "AggregateName", "AggregateId", "AggregateSequenceNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "EventEntity");

            migrationBuilder.DropTable(
                "Locations__Invitations");

            migrationBuilder.DropTable(
                "SnapshotEntity");

            migrationBuilder.DropTable(
                "Locations");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateFundraiserDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Fundraisers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 2, 23, 27, 10, 69, DateTimeKind.Local).AddTicks(7642),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 2, 23, 11, 25, 857, DateTimeKind.Local).AddTicks(4771));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Fundraisers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 3, 2, 23, 11, 25, 857, DateTimeKind.Local).AddTicks(4771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 3, 2, 23, 27, 10, 69, DateTimeKind.Local).AddTicks(7642));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdamAhmedWebProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationToPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePrescribed",
                table: "Prescriptions");

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "Prescriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePrescribed",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

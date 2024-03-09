using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentServiceProvider.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfInstallments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CardExpireDate",
                table: "Transaction",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<long>(
                name: "NumberOfInstallments",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfInstallments",
                table: "Transaction");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CardExpireDate",
                table: "Transaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payable",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}

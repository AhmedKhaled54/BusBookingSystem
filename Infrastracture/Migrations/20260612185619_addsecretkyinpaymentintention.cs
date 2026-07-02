using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class addsecretkyinpaymentintention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymobIntetions_Payments_PaymentId",
                table: "PaymobIntetions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymobIntetions",
                table: "PaymobIntetions");

            migrationBuilder.RenameTable(
                name: "PaymobIntetions",
                newName: "PaymobIntentions");

            migrationBuilder.RenameColumn(
                name: "PaymobIntetionId",
                table: "PaymobIntentions",
                newName: "PaymobIntentionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymobIntetions_PaymentId",
                table: "PaymobIntentions",
                newName: "IX_PaymobIntentions_PaymentId");

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "PaymobIntentions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymobIntentions",
                table: "PaymobIntentions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymobIntentions_Payments_PaymentId",
                table: "PaymobIntentions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymobIntentions_Payments_PaymentId",
                table: "PaymobIntentions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymobIntentions",
                table: "PaymobIntentions");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "PaymobIntentions");

            migrationBuilder.RenameTable(
                name: "PaymobIntentions",
                newName: "PaymobIntetions");

            migrationBuilder.RenameColumn(
                name: "PaymobIntentionId",
                table: "PaymobIntetions",
                newName: "PaymobIntetionId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymobIntentions_PaymentId",
                table: "PaymobIntetions",
                newName: "IX_PaymobIntetions_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymobIntetions",
                table: "PaymobIntetions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymobIntetions_Payments_PaymentId",
                table: "PaymobIntetions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingGidde.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIsOkColumnToCustomTemplateItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_TemplateKind_TemplateKindId",
                schema: "FlowCheck",
                table: "Template");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateKindId",
                schema: "FlowCheck",
                table: "Template",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOk",
                schema: "FlowCheck",
                table: "CustomTemplateItem",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Template_TemplateKind_TemplateKindId",
                schema: "FlowCheck",
                table: "Template",
                column: "TemplateKindId",
                principalSchema: "FlowCheck",
                principalTable: "TemplateKind",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_TemplateKind_TemplateKindId",
                schema: "FlowCheck",
                table: "Template");

            migrationBuilder.DropColumn(
                name: "IsOk",
                schema: "FlowCheck",
                table: "CustomTemplateItem");

            migrationBuilder.AlterColumn<int>(
                name: "TemplateKindId",
                schema: "FlowCheck",
                table: "Template",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_TemplateKind_TemplateKindId",
                schema: "FlowCheck",
                table: "Template",
                column: "TemplateKindId",
                principalSchema: "FlowCheck",
                principalTable: "TemplateKind",
                principalColumn: "Id");
        }
    }
}

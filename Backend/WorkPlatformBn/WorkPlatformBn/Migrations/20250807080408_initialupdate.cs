using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkPlatformBn.Migrations
{
    /// <inheritdoc />
    public partial class initialupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerCategories_WorkerDetails_WorkerDetailsId",
                table: "WorkerCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerCategories_Workers_WorkerId",
                table: "WorkerCategories");

            migrationBuilder.DropTable(
                name: "WorkerDetails");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_WorkerCategories_WorkerId",
                table: "WorkerCategories");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "WorkerCategories");

            migrationBuilder.RenameColumn(
                name: "WorkerDetailsId",
                table: "WorkerCategories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerCategories_WorkerDetailsId",
                table: "WorkerCategories",
                newName: "IX_WorkerCategories_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerCategories_Users_UserId",
                table: "WorkerCategories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerCategories_Users_UserId",
                table: "WorkerCategories");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkerCategories",
                newName: "WorkerDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerCategories_UserId",
                table: "WorkerCategories",
                newName: "IX_WorkerCategories_WorkerDetailsId");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "WorkerCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkerType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    IsCompanyUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerDetails_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerCategories_WorkerId",
                table: "WorkerCategories",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerDetails_WorkerId",
                table: "WorkerDetails",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_UserId",
                table: "Workers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerCategories_WorkerDetails_WorkerDetailsId",
                table: "WorkerCategories",
                column: "WorkerDetailsId",
                principalTable: "WorkerDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerCategories_Workers_WorkerId",
                table: "WorkerCategories",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }
    }
}

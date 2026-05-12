using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KişiselBlog.Migrations
{
    /// <inheritdoc />
    public partial class KategoriTablosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "BlogYazilari",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogYazilari_KategoriId",
                table: "BlogYazilari",
                column: "KategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogYazilari_Kategoriler_KategoriId",
                table: "BlogYazilari",
                column: "KategoriId",
                principalTable: "Kategoriler",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogYazilari_Kategoriler_KategoriId",
                table: "BlogYazilari");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropIndex(
                name: "IX_BlogYazilari_KategoriId",
                table: "BlogYazilari");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "BlogYazilari");
        }
    }
}

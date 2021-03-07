using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class InvertedIndexsearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Docs",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docs", x => x.DocId);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordId);
                });

            migrationBuilder.CreateTable(
                name: "DocsWord",
                columns: table => new
                {
                    DocsContainerDocId = table.Column<int>(type: "int", nullable: false),
                    WordsOfDocWordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocsWord", x => new { x.DocsContainerDocId, x.WordsOfDocWordId });
                    table.ForeignKey(
                        name: "FK_DocsWord_Docs_DocsContainerDocId",
                        column: x => x.DocsContainerDocId,
                        principalTable: "Docs",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocsWord_Words_WordsOfDocWordId",
                        column: x => x.WordsOfDocWordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocsWord_WordsOfDocWordId",
                table: "DocsWord",
                column: "WordsOfDocWordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocsWord");

            migrationBuilder.DropTable(
                name: "Docs");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MakaleWebProje.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Makale",
                columns: table => new
                {
                    MakaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakaleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleIsShow = table.Column<bool>(type: "bit", nullable: false),
                    MakaleImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    MakaleCarousel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makale", x => x.MakaleId);
                    table.ForeignKey(
                        name: "FK_Makale_Kategori_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "Role",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MakaleData",
                columns: table => new
                {
                    MakaleDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MakaleId = table.Column<int>(type: "int", nullable: false),
                    MakaleLike = table.Column<int>(type: "int", nullable: false),
                    MakaleDislike = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleData", x => x.MakaleDataId);
                    table.ForeignKey(
                        name: "FK_MakaleData_Makale_MakaleId",
                        column: x => x.MakaleId,
                        principalTable: "Makale",
                        principalColumn: "MakaleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakaleData_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Kategori",
                columns: new[] { "KategoriId", "KategoriName" },
                values: new object[,]
                {
                    { 1, "Bilim Kurgu" },
                    { 2, "Yazılım" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "UserRoleId", "UserRoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Makale",
                columns: new[] { "MakaleId", "KategoriId", "MakaleCarousel", "MakaleDate", "MakaleImagesUrl", "MakaleIsShow", "MakaleName", "MakaleSummary" },
                values: new object[,]
                {
                    { 1, 1, true, "31.12.2013", "/images/1.jpg", true, "Denem1", "Makale1Özet" },
                    { 2, 2, true, "26.08.2008", "/images/2.jpg", true, "Denem2", "Makale2Özet" },
                    { 3, 2, true, "14.03.2021", "/images/3.jpg", true, "Denem3", "Makale3Özet" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Name", "Password", "SurName", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { 1, "ozdemirbaranugur@example.com", "Baran Uğur", "baranugurozdemir", "Özdemir", "ozdemir", 2 },
                    { 2, "tuncyusuf@example.com", "Yusuf", "yusuftunc", "Tunç", "tunc", 2 },
                    { 3, "soydincomer@example.com", "Ömer", "omersoydınc", "Soydinç", "soydınc", 2 },
                    { 4, "zeynep.kaya@example.com", "Zeynep", "Password101", "Kaya", "zeynep", 1 }
                });

            migrationBuilder.InsertData(
                table: "MakaleData",
                columns: new[] { "MakaleDataId", "MakaleDislike", "MakaleId", "MakaleLike", "UserId", "UsersUserId" },
                values: new object[] { 1, 1, 1, 1, 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Makale_KategoriId",
                table: "Makale",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleData_MakaleId",
                table: "MakaleData",
                column: "MakaleId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleData_UsersUserId",
                table: "MakaleData",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MakaleData");

            migrationBuilder.DropTable(
                name: "Makale");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Kategori");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}

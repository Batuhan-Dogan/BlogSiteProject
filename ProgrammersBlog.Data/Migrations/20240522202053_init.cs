using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProgrammersBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Picture = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    LinkedInProfile = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    GitHubProfile = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Thumbnail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ViewsCount = table.Column<int>(type: "integer", nullable: false),
                    CommentCount = table.Column<int>(type: "integer", nullable: false),
                    SeoAuthor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SeoDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    SeoTags = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ArticleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "0935139f-6fdd-484d-9731-456c38dfc17e", "admin", "ADMIN" },
                    { 2, "91765083-e8b7-46bb-a978-172fd396ba75", "editor", "EDITOR" },
                    { 3, "ef0f66f1-bee0-48b2-ab63-24ac6c4c4872", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "GitHubProfile", "LinkedInProfile", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "bfcff5e2-749c-4108-95dd-9c320b4f3e9a", "batuhandogan@petalmail.com", true, null, null, false, null, "BATUHANDOGAN@PETALMAİL.COM", "BATUHAN", "AQAAAAIAAYagAAAAEAmT5h6lhqPL4alw1ph1QpOKGGPftAD28H/Bv4ORr1rlInnK3C3DWFkiXfmX5bE8EQ==", "+905462636750", true, "AdminProfile.png", "f58e9040-ea49-46b9-ba11-ba303f3ec643", false, "batuhan" },
                    { 2, 0, "958215ab-d276-4146-8c4c-d071f7f51c7c", "editor@petalmail.com", true, null, null, false, null, "EDİTOR@PETALMAİL.COM", "EDİTOR", "AQAAAAIAAYagAAAAEMfOmLgXS1wCvdIadrLBPUhkhUZ7nQQMqcMWcNGeg+2Xt/zBvzf98Sa9HVlgFHIDvw==", "+905442656951", true, "EditorProfile.png", "a4b311a8-7d42-4e8a-8081-d5ff5d52211a", false, "editor" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedTime", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(9270), "Python programlama dili ile ilgili güncel bilgiler .", true, false, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(9273), "Python", "Python kategorisi" },
                    { 2, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(9293), "C# programlama dili ile ilgili güncel bilgiler .", true, false, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(9296), "C#", "C# kategorisi" },
                    { 3, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(9303), "Nesne yönelimli prodramlama ile ilgili güncel bilgiler .", true, false, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(9305), "Nesne Tabanlı Programlama", "Nesne yönelimli programlama kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedTime", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[,]
                {
                    { 1, 3, 15, "Yazılımda, bir nesne, nesne yönelimli programlama (OOP) paradigmalarından biri olan ve gerçek dünyadaki nesnelerin modellemesi için kullanılan temel bir bileşendir. Bir nesne, verileri (değişkenler veya özellikler) ve bu verilere uygulanan işlevselliği (metodlar) içeren birimdir. Örneğin, bir \"Araba\" nesnesi, rengi, hızı ve modeli gibi verileri içerebilir ve aynı zamanda \"Gaz Ver\" ve \"Fren Yap\" gibi işlevselliği sağlayan metodları içerebilir. Bu şekilde, yazılım nesneleri, gerçek dünyadaki nesnelerin özelliklerini ve davranışlarını yansıtarak, programların daha modüler, anlaşılır ve sürdürülebilir olmasına yardımcı olur.", "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(5490), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(5494), "Yazılımda nesne nedir?", "Batuhan Doğan", "Yazılımda nesne nedir?", "Nesne", "https://codingnomads.co/wp-content/uploads/2020/12/OOP-graphic-blog-oop-concepts-in-java-what-is-object-oriented-programming.png", "Nesne nedir?", 1, 288 },
                    { 2, 3, 4, "Nesne Yönelimli Programlama (OOP), yazılım geliştirmenin bir programlama paradigması veya yaklaşımıdır ve yazılım tasarımını ve kod organizasyonunu şekillendirmek için kullanılır. OOP, programların daha anlaşılır, modüler ve sürdürülebilir olmasına yardımcı olmak için kullanılan bir dizi kavram ve prensibe dayanır. İşte OOP'nin temel açıklamalarından bazıları:\r\n\r\nNesneler: OOP, gerçek dünyadaki nesneleri programlamada kullanır. Bu nesneler, verileri ve bu verileri işleyen metodları içerir. Örneğin, bir \"Araba\" nesnesi, rengi, hızı ve modeli gibi özellikleri içerebilir ve gaz verme veya fren yapma gibi işlevleri gerçekleştirebilir.\r\n\r\nSınıflar: Nesnelerin tasarım şablonlarıdır. Sınıflar, belirli bir nesne türünün özelliklerini ve davranışlarını tanımlar. Örneğin, \"Araba\" sınıfı, araba nesnelerinin nasıl oluşturulacağını ve hangi özelliklere sahip olacağını belirtir.\r\n\r\nSoyutlama: Soyutlama, karmaşıklığı yönetmek ve gereksiz ayrıntıları gizlemek için kullanılır. Nesneler, gerçek dünyadaki nesnelerin önemli özelliklerini ve davranışlarını temsil ederken gereksiz detayları gizler.\r\n\r\nKapsülleme: Kapsülleme, verileri ve işlevleri bir araya getirerek bir \"kapsül\" oluşturur. Bu, verilere dışarıdan sadece belirli metodlar aracılığıyla erişilmesini ve bu verilerin güvenliğini sağlar", "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(5504), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 681, DateTimeKind.Utc).AddTicks(5506), "OOP nedir?", "Batuhan Doğan", "Yazılımda nesne nedir?", " OOP", "https://codingnomads.co/wp-content/uploads/2020/12/OOP-graphic-blog-oop-concepts-in-java-what-is-object-oriented-programming.png", "Nesne Tabanlı programlama (OOP) nedir?", 1, 218 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "ArticleId", "CreatedByName", "CreatedTime", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Note", "Text" },
                values: new object[] { 1, 1, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 682, DateTimeKind.Utc).AddTicks(9369), true, false, "InitialCreate", new DateTime(2024, 5, 22, 20, 20, 51, 682, DateTimeKind.Utc).AddTicks(9372), "Nesne yönelimli programlama yorumu", "Çok teşşekür ederim çok faydası dokundu" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

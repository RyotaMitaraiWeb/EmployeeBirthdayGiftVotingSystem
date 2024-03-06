using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeBirthdayGiftVotingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                name: "Gift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageFileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gift", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                name: "BirthdayVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthdayVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BirthdayVotes_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BirthdayVotes_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserGiftVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoterId = table.Column<Guid>(type: "uuid", nullable: false),
                    GiftId = table.Column<int>(type: "integer", nullable: true),
                    BirthdayVoteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGiftVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGiftVotes_AspNetUsers_VoterId",
                        column: x => x.VoterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGiftVotes_BirthdayVotes_BirthdayVoteId",
                        column: x => x.BirthdayVoteId,
                        principalTable: "BirthdayVotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGiftVotes_Gift_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gift",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1976a0d1-d843-4c6a-a746-1d909178d1de"), 0, new DateTime(1984, 7, 6, 21, 0, 0, 0, DateTimeKind.Utc), "9630aa77-ee68-49cc-8931-32460dd13112", null, false, "Lee", "Everett", false, null, null, "LEE", "AQAAAAIAAYagAAAAEBF96hPbuYkcCyA2+KJhiAp4AS5kWWejwOovYq3zLP0NvlUy3Ws2lc5p5juiAEsEng==", null, false, "83d9f9b9-aa23-43b3-a755-78650e128929", false, "lee" },
                    { new Guid("29506ae4-eccc-47d8-94ed-ec6ffc8023c5"), 0, new DateTime(1990, 4, 4, 21, 0, 0, 0, DateTimeKind.Utc), "0bb01ec1-7f2c-422a-96c1-20baa2aec144", null, false, "Henry", "Wilson", false, null, null, "ALAKAZAM", "AQAAAAIAAYagAAAAEMGUr7efWGiD1Gl/mFJaPBzkIr39U3ayM4TI3c/oUQozBFAstfFQQ4WlmqR/w5Q64A==", null, false, "229f7ec6-a5bb-4630-8339-dde015920848", false, "Alakazam" },
                    { new Guid("4e592c87-0e1f-4b64-97f2-31aa0444705d"), 0, new DateTime(2002, 6, 3, 21, 0, 0, 0, DateTimeKind.Utc), "9290469e-e686-4fcc-ab8a-2eea902364be", null, false, "Ryota", "Mitarai", false, null, null, "RYOTA1", "AQAAAAIAAYagAAAAEDuz/8k/BWXqL1PohMSfay2kt9w6YiMOKXPRSELKK7PTRuVuXUFODtLOj0q+Hog4cg==", null, false, "375e952e-67a1-4ea1-a5a2-4ed5f79da8c4", false, "ryota1" },
                    { new Guid("6dc922b1-3987-4a34-83ec-c8b27a718fbb"), 0, new DateTime(1999, 1, 1, 22, 0, 0, 0, DateTimeKind.Utc), "c7874886-8d46-4c12-99e0-9c6b2400a4cf", null, false, "Jane", "Doe", false, null, null, "THEREALJANE", "AQAAAAIAAYagAAAAELmREJIMDBuHIf4RfiAAEt98MiZnCoIBY83nkDLeJ4Zm0SJCDSUsOzFcBMPZgZ2KjQ==", null, false, "12490346-a2e1-4a60-9d0f-e3dd4b2a0fb5", false, "therealjane" },
                    { new Guid("8018e901-3aa6-4345-8675-fadbb6852c7b"), 0, new DateTime(1998, 12, 31, 22, 0, 0, 0, DateTimeKind.Utc), "97f47df1-5e22-4884-a989-64dbc2bcda0d", null, false, "John", "Doe", false, null, null, "THEREALJOHN", "AQAAAAIAAYagAAAAECtQ+LcXcMVOdjZu5eJP/Vd9tspqrWsq8LjpA6LGZn63WZqUfw8CSDkxg7BcAmCCHw==", null, false, "fe3ce740-6429-43e7-9e24-c00907285858", false, "therealjohn" },
                    { new Guid("a6795017-baf4-477f-b289-fbf01e755dd8"), 0, new DateTime(1981, 9, 26, 21, 0, 0, 0, DateTimeKind.Utc), "3a92a167-c28a-46ed-9ce5-760fd312e7b1", null, false, "Joel", "Miller", false, null, null, "TEXAS", "AQAAAAIAAYagAAAAEA9z0beUNTc4CCUhsbADLIbfPrmPSmz5EUF6dRFuw3pXA7R87XXqrYipSgEyl+un3A==", null, false, "74b72bf3-6f9f-46bb-bddc-b227bdbeb1e4", false, "texas" }
                });

            migrationBuilder.InsertData(
                table: "Gift",
                columns: new[] { "Id", "Description", "ImageFileName", "Name" },
                values: new object[,]
                {
                    { 1, "A pair of joycons for the Nintendo Switch. The joycons will be bought brand new, so hopefully they won't drift.", "joycons.jpg", "Joycons" },
                    { 2, "A high-quality pizza cutter wheel, made of Swedish steel.", "pizzacutter.jpg", "Pizza cutter wheel" },
                    { 3, "A mug with text that reveals how everyone really feels.", "mug.jpg", "\"I hate Mondays\" mug" },
                    { 4, "What people would describe as a \"Rubik Pyramid\".", "pyraminx.jpg", "Pyraminx" },
                    { 5, "A license for the new and totally existant and non-fake Java implementation, which can be executed inside the browser.", "fake-javascript-license.png", "Fake Java license" },
                    { 6, "A wand that grants any wish (when those wishes will be granted remains to be seen).", "magic-wand.png", "Magic Wand" }
                });

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
                name: "IX_BirthdayVotes_CreatorId",
                table: "BirthdayVotes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BirthdayVotes_EmployeeId",
                table: "BirthdayVotes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGiftVotes_BirthdayVoteId",
                table: "UserGiftVotes",
                column: "BirthdayVoteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGiftVotes_GiftId",
                table: "UserGiftVotes",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGiftVotes_VoterId",
                table: "UserGiftVotes",
                column: "VoterId");
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
                name: "UserGiftVotes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BirthdayVotes");

            migrationBuilder.DropTable(
                name: "Gift");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

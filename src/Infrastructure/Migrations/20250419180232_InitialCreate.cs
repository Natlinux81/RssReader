using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "feeds");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RssFeeds",
                schema: "feeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    ChannelTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RssFeedItems",
                schema: "feeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    RssFeedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssFeedItems_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalSchema: "feeds",
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RssFeedId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalSchema: "feeds",
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RssFeedUser",
                columns: table => new
                {
                    RssFeedsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeedUser", x => new { x.RssFeedsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RssFeedUser_RssFeeds_RssFeedsId",
                        column: x => x.RssFeedsId,
                        principalSchema: "feeds",
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RssFeedUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SuperAdmin" },
                    { 2, "Admin" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                schema: "feeds",
                table: "RssFeeds",
                columns: new[] { "Id", "ChannelTitle", "Url" },
                values: new object[,]
                {
                    { 1, "Test Feed 1 Technologie Nachrichten und neue Entwicklungen", "https://www.spiegel.de/politik/index.rss" },
                    { 2, "Test Feed 2 Wissenschaft - WELT", "https://www.welt.de/feeds/section/wissenschaft.rss" }
                });

            migrationBuilder.InsertData(
                schema: "feeds",
                table: "RssFeedItems",
                columns: new[] { "Id", "Description", "ImageUrl", "Link", "PublishDate", "RssFeedId", "Title" },
                values: new object[,]
                {
                    { 1, "Bundesarbeitsminister Heil will den Einsatz von Künstlicher Intelligenz (KI) in der Arbeitswelt vorantreiben. Arbeit werde sich verändern, aber nicht ausgehen, so Heil. In seinem Ministerium gebe es dazu bereits eine Denkfabrik.", "https://images.tagesschau.de/image/8616ee35-2c95-46f8-b8b5-0f5da2c4b359/AAABiL5ZIxM/AAABkZLhkrw/16x9-1280/roboter-ki-100.jpg", "https://www.tagesschau.de/wirtschaft/digitales/spd-heil-ki-arbeitswelt-100.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 1, "Test Feed Item 1 Künstliche Intelligenz; Arbeitsminister Heil will KI \"auf die Straße bringen" },
                    { 2, "Test Feed Item 2 Bei der Messe IAA Mobility in München setzen die Autobauer besonders ihre E-Autos in Szene. Die Botschaft: Die Zukunft ist elektrisch. Doch wann kommt der angekündigte Durchbruch der E-Mobilität? Von Sebastian Hanisch.", "https://images.tagesschau.de/image/ddd57310-c336-49f4-9351-067576add465/AAABimEJjZ0/AAABkZLhkrw/16x9-1280/iaa-200.jpg", "https://testlink2.com/", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 1, "Test Feed Item 2 IAA Mobility: Wann schaffen E-Autos den Durchbruch in Deutschland?" },
                    { 3, "Test Feed Item 3 Ob Staubsauger, Herd oder Lampen - Haushaltselektronik wird immer schlauer und macht das Zuhause zum \"Smart Home\". Die intelligente Vernetzung breitet sich aus und ist auch bei auf heute beginnenden IFA ein Thema. Von Ole Hilgert.", "https://images.tagesschau.de/image/132ad0ce-c2a4-4b80-893f-d5236a39891f/AAABikx6AhE/AAABkZLhkrw/16x9-1280/ifa-2023-100.jpg", "https://www.tagesschau.de/wirtschaft/digitales/ifa-smart-home-102.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 1, "Test Feed Item 3 Elektronikmesse IFA: Der Weg zum intelligenten Zuhause" },
                    { 4, "Test Feed Item 4 Ausgedehnte Riffe vor den europäischen Küsten – das war einmal. Aber nicht Korallen, sondern einheimische Austern waren die Basis. Diese existieren nur noch vereinzelt in Nordsee und Atlantik, aber das soll sich durch die Wiederansiedlung ändern", "https://img.welt.de/img/wissenschaft/mobile253832924/1181622987-ci23x11-w780/France-Normandy-Manche-department-Cotentin-Saint-Vaast-la-Hougue-lle-de-Tat.jpg", "https://www.welt.de/wissenschaft/article253832928/Europaeische-Austern-Muscheln-aus-der-Zucht-sollen-neue-Riffe-bilden.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 1, "Test Feed Item 4 Zuchtmuscheln sollen wieder Riffe bilden" },
                    { 5, "Test Feed Item 5 Die Form ist entscheidend: Menschen nehmen Informationen unterschiedlich wahr – und zwar abhängig davon, auf welche Weise ihnen Zahlen präsentiert werden. Italienische Forscher haben untersucht, wie sich das zum Beispiel auf das Thema Migration auswirkt.", "https://img.welt.de/img/wissenschaft/mobile253829762/5701629567-ci23x11-w780/Beer-Fest-from-within-tent.jpg", "https://www.welt.de/wissenschaft/article253829764/Gefuehltes-Verhaeltnis-Warum-zwei-von-50-mehr-klingt-als-vier-Prozent.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 1, "Test Feed Item 5 Zwei von 50 Menschen – sind nur vier Prozent" },
                    { 6, "Test Feed Item 6 Eine Grenze, mitten durch Deutschland: Nicht nur die Nation war über Jahrzehnte geteilt, sondern auch die Tierwelt. Der fast 1400 Kilometer lange Todesstreifen ist heute ein „Grünes Band“. Trotzdem bleiben die Tiere im Osten und die im Westen unter sich – aus bemerkenswerten Gründen.", "https://img.welt.de/img/wissenschaft/mobile253834946/0941626977-ci23x11-w780/DWO-WS-Teaser-Ost-West-Tiere-mku-cw-jpg.jpg", "https://www.welt.de/wissenschaft/plus253801310/West-und-ostdeutsche-Natur-Warum-durch-die-Tierwelt-noch-immer-eine-Grenze-verlaeuft.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 1, "Test Feed Item 6 Warum sich Deutschland für Mäuse, Igel, Krähen und andere Tiere in Ost und West teilt" },
                    { 7, "Bundesarbeitsminister Heil will den Einsatz von Künstlicher Intelligenz (KI) in der Arbeitswelt vorantreiben. Arbeit werde sich verändern, aber nicht ausgehen, so Heil. In seinem Ministerium gebe es dazu bereits eine Denkfabrik.", "https://images.tagesschau.de/image/8616ee35-2c95-46f8-b8b5-0f5da2c4b359/AAABiL5ZIxM/AAABkZLhkrw/16x9-1280/roboter-ki-100.jpg", "https://www.tagesschau.de/wirtschaft/digitales/spd-heil-ki-arbeitswelt-100.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Test Feed Item 7 Künstliche Intelligenz; Arbeitsminister Heil will KI \"auf die Straße bringen" },
                    { 8, "Test Feed Item 2 Bei der Messe IAA Mobility in München setzen die Autobauer besonders ihre E-Autos in Szene. Die Botschaft: Die Zukunft ist elektrisch. Doch wann kommt der angekündigte Durchbruch der E-Mobilität? Von Sebastian Hanisch.", "https://images.tagesschau.de/image/ddd57310-c336-49f4-9351-067576add465/AAABimEJjZ0/AAABkZLhkrw/16x9-1280/iaa-200.jpg", "https://testlink2.com/", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Test Feed Item 8 IAA Mobility: Wann schaffen E-Autos den Durchbruch in Deutschland?" },
                    { 9, "Test Feed Item 3 Ob Staubsauger, Herd oder Lampen - Haushaltselektronik wird immer schlauer und macht das Zuhause zum \"Smart Home\". Die intelligente Vernetzung breitet sich aus und ist auch bei auf heute beginnenden IFA ein Thema. Von Ole Hilgert.", "https://images.tagesschau.de/image/132ad0ce-c2a4-4b80-893f-d5236a39891f/AAABikx6AhE/AAABkZLhkrw/16x9-1280/ifa-2023-100.jpg", "https://www.tagesschau.de/wirtschaft/digitales/ifa-smart-home-102.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Test Feed Item 9 Elektronikmesse IFA: Der Weg zum intelligenten Zuhause" },
                    { 10, "Test Feed Item 4 Ausgedehnte Riffe vor den europäischen Küsten – das war einmal. Aber nicht Korallen, sondern einheimische Austern waren die Basis. Diese existieren nur noch vereinzelt in Nordsee und Atlantik, aber das soll sich durch die Wiederansiedlung ändern", "https://img.welt.de/img/wissenschaft/mobile253832924/1181622987-ci23x11-w780/France-Normandy-Manche-department-Cotentin-Saint-Vaast-la-Hougue-lle-de-Tat.jpg", "https://www.welt.de/wissenschaft/article253832928/Europaeische-Austern-Muscheln-aus-der-Zucht-sollen-neue-Riffe-bilden.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Test Feed Item 10 Zuchtmuscheln sollen wieder Riffe bilden" },
                    { 11, "Test Feed Item 5 Die Form ist entscheidend: Menschen nehmen Informationen unterschiedlich wahr – und zwar abhängig davon, auf welche Weise ihnen Zahlen präsentiert werden. Italienische Forscher haben untersucht, wie sich das zum Beispiel auf das Thema Migration auswirkt.", "https://img.welt.de/img/wissenschaft/mobile253829762/5701629567-ci23x11-w780/Beer-Fest-from-within-tent.jpg", "https://www.welt.de/wissenschaft/article253829764/Gefuehltes-Verhaeltnis-Warum-zwei-von-50-mehr-klingt-als-vier-Prozent.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Test Feed Item 11 Zwei von 50 Menschen – sind nur vier Prozent" },
                    { 12, "Test Feed Item 6 Eine Grenze, mitten durch Deutschland: Nicht nur die Nation war über Jahrzehnte geteilt, sondern auch die Tierwelt. Der fast 1400 Kilometer lange Todesstreifen ist heute ein „Grünes Band“. Trotzdem bleiben die Tiere im Osten und die im Westen unter sich – aus bemerkenswerten Gründen.", "https://img.welt.de/img/wissenschaft/mobile253834946/0941626977-ci23x11-w780/DWO-WS-Teaser-Ost-West-Tiere-mku-cw-jpg.jpg", "https://www.welt.de/wissenschaft/plus253801310/West-und-ostdeutsche-Natur-Warum-durch-die-Tierwelt-noch-immer-eine-Grenze-verlaeuft.html", new DateTime(2024, 4, 21, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Test Feed Item 12 Warum sich Deutschland für Mäuse, Igel, Krähen und andere Tiere in Ost und West teilt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedItems_RssFeedId",
                schema: "feeds",
                table: "RssFeedItems",
                column: "RssFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedUser_UsersId",
                table: "RssFeedUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RssFeedId",
                schema: "auth",
                table: "Users",
                column: "RssFeedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RssFeedItems",
                schema: "feeds");

            migrationBuilder.DropTable(
                name: "RssFeedUser");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "RssFeeds",
                schema: "feeds");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Assignment7.Persistence.Migrations
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
                    Id = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                name: "Books",
                columns: table => new
                {
                    bookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ISBN = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    author = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    publisher = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    purchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    totalBook = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    language = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.bookId);
                });

            migrationBuilder.CreateTable(
                name: "workflow",
                columns: table => new
                {
                    workflowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflowname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow", x => x.workflowid);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
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
                    UserId = table.Column<string>(type: "text", nullable: false),
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
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
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
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    position = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    privilage = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    libraryCardNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    notes = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Users_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "borrow",
                columns: table => new
                {
                    borrowid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    bookid = table.Column<int>(type: "integer", nullable: false),
                    dateofborrow = table.Column<DateOnly>(type: "date", nullable: true),
                    deadlinereturn = table.Column<DateOnly>(type: "date", nullable: true),
                    dateofreturn = table.Column<DateOnly>(type: "date", nullable: true),
                    penalty = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrow", x => x.borrowid);
                    table.ForeignKey(
                        name: "FK_borrow_AspNetUsers_userid",
                        column: x => x.userid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_borrow_Books_bookid",
                        column: x => x.bookid,
                        principalTable: "Books",
                        principalColumn: "bookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflowsequences",
                columns: table => new
                {
                    stepid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflowid = table.Column<int>(type: "integer", nullable: true),
                    steporder = table.Column<int>(type: "integer", nullable: true),
                    stepname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    requiredrole = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflowsequences", x => x.stepid);
                    table.ForeignKey(
                        name: "FK_workflowsequences_AspNetUsers_requiredrole",
                        column: x => x.requiredrole,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_workflowsequences_workflow_workflowid",
                        column: x => x.workflowid,
                        principalTable: "workflow",
                        principalColumn: "workflowid");
                });

            migrationBuilder.CreateTable(
                name: "nextsteprules",
                columns: table => new
                {
                    ruleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    currentstepid = table.Column<int>(type: "integer", nullable: true),
                    nextstepid = table.Column<int>(type: "integer", nullable: true),
                    conditiontype = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    conditionvalue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nextsteprules", x => x.ruleid);
                    table.ForeignKey(
                        name: "FK_nextsteprules_workflowsequences_currentstepid",
                        column: x => x.currentstepid,
                        principalTable: "workflowsequences",
                        principalColumn: "stepid");
                    table.ForeignKey(
                        name: "FK_nextsteprules_workflowsequences_nextstepid",
                        column: x => x.nextstepid,
                        principalTable: "workflowsequences",
                        principalColumn: "stepid");
                });

            migrationBuilder.CreateTable(
                name: "process",
                columns: table => new
                {
                    processid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workflowid = table.Column<int>(type: "integer", nullable: true),
                    requesterid = table.Column<string>(type: "character varying", nullable: true),
                    requesttype = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    currentstepid = table.Column<int>(type: "integer", nullable: true),
                    requestdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_process", x => x.processid);
                    table.ForeignKey(
                        name: "FK_process_AspNetUsers_requesterid",
                        column: x => x.requesterid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_process_workflow_workflowid",
                        column: x => x.workflowid,
                        principalTable: "workflow",
                        principalColumn: "workflowid");
                    table.ForeignKey(
                        name: "FK_process_workflowsequences_currentstepid",
                        column: x => x.currentstepid,
                        principalTable: "workflowsequences",
                        principalColumn: "stepid");
                });

            migrationBuilder.CreateTable(
                name: "bookrequest",
                columns: table => new
                {
                    requestid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    requestname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    startdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    enddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    isbn = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    author = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    publisher = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    processid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookrequest", x => x.requestid);
                    table.ForeignKey(
                        name: "FK_bookrequest_process_processid",
                        column: x => x.processid,
                        principalTable: "process",
                        principalColumn: "processid");
                });

            migrationBuilder.CreateTable(
                name: "workflowactions",
                columns: table => new
                {
                    actionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    requestid = table.Column<int>(type: "integer", nullable: true),
                    stepid = table.Column<int>(type: "integer", nullable: true),
                    actorid = table.Column<string>(type: "character varying", nullable: true),
                    action = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    actiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflowactions", x => x.actionid);
                    table.ForeignKey(
                        name: "FK_workflowactions_AspNetUsers_actorid",
                        column: x => x.actorid,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_workflowactions_process_requestid",
                        column: x => x.requestid,
                        principalTable: "process",
                        principalColumn: "processid");
                    table.ForeignKey(
                        name: "FK_workflowactions_workflowsequences_stepid",
                        column: x => x.stepid,
                        principalTable: "workflowsequences",
                        principalColumn: "stepid");
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
                name: "IX_bookrequest_processid",
                table: "bookrequest",
                column: "processid");

            migrationBuilder.CreateIndex(
                name: "IX_borrow_bookid",
                table: "borrow",
                column: "bookid");

            migrationBuilder.CreateIndex(
                name: "IX_borrow_userid",
                table: "borrow",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_nextsteprules_currentstepid",
                table: "nextsteprules",
                column: "currentstepid");

            migrationBuilder.CreateIndex(
                name: "IX_nextsteprules_nextstepid",
                table: "nextsteprules",
                column: "nextstepid");

            migrationBuilder.CreateIndex(
                name: "IX_process_currentstepid",
                table: "process",
                column: "currentstepid");

            migrationBuilder.CreateIndex(
                name: "IX_process_requesterid",
                table: "process",
                column: "requesterid");

            migrationBuilder.CreateIndex(
                name: "IX_process_workflowid",
                table: "process",
                column: "workflowid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AppUserId",
                table: "Users",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_workflowactions_actorid",
                table: "workflowactions",
                column: "actorid");

            migrationBuilder.CreateIndex(
                name: "IX_workflowactions_requestid",
                table: "workflowactions",
                column: "requestid");

            migrationBuilder.CreateIndex(
                name: "IX_workflowactions_stepid",
                table: "workflowactions",
                column: "stepid");

            migrationBuilder.CreateIndex(
                name: "IX_workflowsequences_requiredrole",
                table: "workflowsequences",
                column: "requiredrole");

            migrationBuilder.CreateIndex(
                name: "IX_workflowsequences_workflowid",
                table: "workflowsequences",
                column: "workflowid");
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
                name: "bookrequest");

            migrationBuilder.DropTable(
                name: "borrow");

            migrationBuilder.DropTable(
                name: "nextsteprules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "workflowactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "process");

            migrationBuilder.DropTable(
                name: "workflowsequences");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "workflow");
        }
    }
}

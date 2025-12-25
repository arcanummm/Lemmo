using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lemmo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lemmo");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "default_user_lesson_settings",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultPricePerHour = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    DefaultDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_default_user_lesson_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_default_user_lesson_settings_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lemmo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personal_event_templates",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Location = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal_event_templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personal_event_templates_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lemmo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lemmo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personal_events",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Location = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsFromTemplate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personal_events_personal_event_templates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "lemmo",
                        principalTable: "personal_event_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_personal_events_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "lemmo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recurrence_patterns",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Interval = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    DaysOfWeek = table.Column<string>(type: "jsonb", nullable: false),
                    DayOfMonth = table.Column<int>(type: "integer", nullable: true),
                    MonthlyType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    OccurrenceCount = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recurrence_patterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recurrence_patterns_personal_event_templates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "lemmo",
                        principalTable: "personal_event_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rooms_students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "lemmo",
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rooms_users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "lemmo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_contacts",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_student_contacts_students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "lemmo",
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personal_event_exceptions",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExceptionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NewDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NewDuration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedEventId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal_event_exceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personal_event_exceptions_personal_event_templates_Template~",
                        column: x => x.TemplateId,
                        principalSchema: "lemmo",
                        principalTable: "personal_event_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personal_event_exceptions_personal_events_CreatedEventId",
                        column: x => x.CreatedEventId,
                        principalSchema: "lemmo",
                        principalTable: "personal_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "default_room_settings",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultPricePerHour = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    DefaultDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_default_room_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_default_room_settings_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "lemmo",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "homeworks",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Deadline = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_homeworks_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "lemmo",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notes_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "lemmo",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room_schedule_templates",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_schedule_templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_room_schedule_templates_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "lemmo",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedule_template_rules",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Price = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_template_rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedule_template_rules_room_schedule_templates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "lemmo",
                        principalTable: "room_schedule_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lessons",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    TemplateRuleId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Price = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsModified = table.Column<bool>(type: "boolean", nullable: false),
                    OriginalPlannedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModificationReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessons_room_schedule_templates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "lemmo",
                        principalTable: "room_schedule_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_lessons_rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "lemmo",
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lessons_schedule_template_rules_TemplateRuleId",
                        column: x => x.TemplateRuleId,
                        principalSchema: "lemmo",
                        principalTable: "schedule_template_rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "schedule_exceptions",
                schema: "lemmo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExceptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NewDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NewDuration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    NewPrice = table.Column<double>(type: "numeric(10,2)", nullable: true),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedLessonId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_exceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedule_exceptions_lessons_CreatedLessonId",
                        column: x => x.CreatedLessonId,
                        principalSchema: "lemmo",
                        principalTable: "lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_schedule_exceptions_room_schedule_templates_TemplateId",
                        column: x => x.TemplateId,
                        principalSchema: "lemmo",
                        principalTable: "room_schedule_templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_default_room_settings_RoomId",
                schema: "lemmo",
                table: "default_room_settings",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_default_user_lesson_settings_UserId",
                schema: "lemmo",
                table: "default_user_lesson_settings",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_homeworks_Deadline",
                schema: "lemmo",
                table: "homeworks",
                column: "Deadline");

            migrationBuilder.CreateIndex(
                name: "IX_homeworks_RoomId",
                schema: "lemmo",
                table: "homeworks",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_OriginalPlannedTime",
                schema: "lemmo",
                table: "lessons",
                column: "OriginalPlannedTime");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_RoomId",
                schema: "lemmo",
                table: "lessons",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_RoomId_StartTime",
                schema: "lemmo",
                table: "lessons",
                columns: new[] { "RoomId", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_lessons_StartTime",
                schema: "lemmo",
                table: "lessons",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_Status",
                schema: "lemmo",
                table: "lessons",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_TemplateId",
                schema: "lemmo",
                table: "lessons",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_TemplateRuleId",
                schema: "lemmo",
                table: "lessons",
                column: "TemplateRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_notes_RoomId",
                schema: "lemmo",
                table: "notes",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_exceptions_CreatedEventId",
                schema: "lemmo",
                table: "personal_event_exceptions",
                column: "CreatedEventId");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_exceptions_ExceptionDate",
                schema: "lemmo",
                table: "personal_event_exceptions",
                column: "ExceptionDate");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_exceptions_TemplateId",
                schema: "lemmo",
                table: "personal_event_exceptions",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_exceptions_TemplateId_ExceptionDate",
                schema: "lemmo",
                table: "personal_event_exceptions",
                columns: new[] { "TemplateId", "ExceptionDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_exceptions_Type",
                schema: "lemmo",
                table: "personal_event_exceptions",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_templates_UserId",
                schema: "lemmo",
                table: "personal_event_templates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_templates_ValidFrom",
                schema: "lemmo",
                table: "personal_event_templates",
                column: "ValidFrom");

            migrationBuilder.CreateIndex(
                name: "IX_personal_event_templates_ValidUntil",
                schema: "lemmo",
                table: "personal_event_templates",
                column: "ValidUntil");

            migrationBuilder.CreateIndex(
                name: "IX_personal_events_EndTime",
                schema: "lemmo",
                table: "personal_events",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_personal_events_StartTime",
                schema: "lemmo",
                table: "personal_events",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_personal_events_TemplateId",
                schema: "lemmo",
                table: "personal_events",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_personal_events_UserId",
                schema: "lemmo",
                table: "personal_events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_personal_events_UserId_StartTime",
                schema: "lemmo",
                table: "personal_events",
                columns: new[] { "UserId", "StartTime" });

            migrationBuilder.CreateIndex(
                name: "IX_recurrence_patterns_TemplateId",
                schema: "lemmo",
                table: "recurrence_patterns",
                column: "TemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recurrence_patterns_Type",
                schema: "lemmo",
                table: "recurrence_patterns",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_room_schedule_templates_RoomId",
                schema: "lemmo",
                table: "room_schedule_templates",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_room_schedule_templates_ValidFrom",
                schema: "lemmo",
                table: "room_schedule_templates",
                column: "ValidFrom");

            migrationBuilder.CreateIndex(
                name: "IX_room_schedule_templates_ValidUntil",
                schema: "lemmo",
                table: "room_schedule_templates",
                column: "ValidUntil");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_CreatedAt",
                schema: "lemmo",
                table: "rooms",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_OwnerId",
                schema: "lemmo",
                table: "rooms",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_StudentId",
                schema: "lemmo",
                table: "rooms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exceptions_CreatedLessonId",
                schema: "lemmo",
                table: "schedule_exceptions",
                column: "CreatedLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exceptions_ExceptionDate",
                schema: "lemmo",
                table: "schedule_exceptions",
                column: "ExceptionDate");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exceptions_TemplateId",
                schema: "lemmo",
                table: "schedule_exceptions",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exceptions_TemplateId_ExceptionDate",
                schema: "lemmo",
                table: "schedule_exceptions",
                columns: new[] { "TemplateId", "ExceptionDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exceptions_Type",
                schema: "lemmo",
                table: "schedule_exceptions",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_template_rules_DayOfWeek",
                schema: "lemmo",
                table: "schedule_template_rules",
                column: "DayOfWeek");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_template_rules_IsActive",
                schema: "lemmo",
                table: "schedule_template_rules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_template_rules_TemplateId",
                schema: "lemmo",
                table: "schedule_template_rules",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_template_rules_TemplateId_DayOfWeek_StartTime",
                schema: "lemmo",
                table: "schedule_template_rules",
                columns: new[] { "TemplateId", "DayOfWeek", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_contacts_StudentId",
                schema: "lemmo",
                table: "student_contacts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_contacts_Type",
                schema: "lemmo",
                table: "student_contacts",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_students_Name",
                schema: "lemmo",
                table: "students",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_students_UserId",
                schema: "lemmo",
                table: "students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CreatedAt",
                schema: "lemmo",
                table: "users",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                schema: "lemmo",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "default_room_settings",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "default_user_lesson_settings",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "homeworks",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "notes",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "personal_event_exceptions",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "recurrence_patterns",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "schedule_exceptions",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "student_contacts",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "personal_events",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "lessons",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "personal_event_templates",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "schedule_template_rules",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "room_schedule_templates",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "students",
                schema: "lemmo");

            migrationBuilder.DropTable(
                name: "users",
                schema: "lemmo");
        }
    }
}

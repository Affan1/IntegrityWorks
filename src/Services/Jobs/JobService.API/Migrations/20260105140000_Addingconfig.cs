using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobService.API.Migrations
{
    /// <inheritdoc />
    public partial class Addingconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAttachments_jobs_JobId",
                table: "JobAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_JobCategories_jobs_JobId",
                table: "JobCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_jobs_JobId",
                table: "JobSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobViews",
                table: "JobViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_JobCategories_JobId",
                table: "JobCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAttachments",
                table: "JobAttachments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "ProficiencyLevel",
                table: "JobSkills");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JobCategories");

            migrationBuilder.RenameTable(
                name: "JobViews",
                newName: "job_views");

            migrationBuilder.RenameTable(
                name: "JobSkills",
                newName: "job_skills");

            migrationBuilder.RenameTable(
                name: "JobCategories",
                newName: "job_categories");

            migrationBuilder.RenameTable(
                name: "JobAttachments",
                newName: "job_attachments");

            migrationBuilder.RenameIndex(
                name: "IX_JobAttachments_JobId",
                table: "job_attachments",
                newName: "IX_job_attachments_JobId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ViewedAt",
                table: "job_views",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignedAt",
                table: "job_skills",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignedAt",
                table: "job_categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "job_attachments",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_views",
                table: "job_views",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_skills",
                table: "job_skills",
                columns: new[] { "JobId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_categories",
                table: "job_categories",
                columns: new[] { "JobId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_attachments",
                table: "job_attachments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categories_categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobStatusHistories_JobId",
                table: "JobStatusHistories",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_job_views_JobId",
                table: "job_views",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_job_views_ViewedAt",
                table: "job_views",
                column: "ViewedAt");

            migrationBuilder.CreateIndex(
                name: "IX_job_skills_SkillId",
                table: "job_skills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_job_categories_CategoryId",
                table: "job_categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_Name",
                table: "categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentId",
                table: "categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_Slug",
                table: "categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_skills_Slug",
                table: "skills",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_job_attachments_jobs_JobId",
                table: "job_attachments",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_categories_categories_CategoryId",
                table: "job_categories",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_categories_jobs_JobId",
                table: "job_categories",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_skills_jobs_JobId",
                table: "job_skills",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_skills_skills_SkillId",
                table: "job_skills",
                column: "SkillId",
                principalTable: "skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_views_jobs_JobId",
                table: "job_views",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobStatusHistories_jobs_JobId",
                table: "JobStatusHistories",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_job_attachments_jobs_JobId",
                table: "job_attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_job_categories_categories_CategoryId",
                table: "job_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_job_categories_jobs_JobId",
                table: "job_categories");

            migrationBuilder.DropForeignKey(
                name: "FK_job_skills_jobs_JobId",
                table: "job_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_job_skills_skills_SkillId",
                table: "job_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_job_views_jobs_JobId",
                table: "job_views");

            migrationBuilder.DropForeignKey(
                name: "FK_JobStatusHistories_jobs_JobId",
                table: "JobStatusHistories");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropIndex(
                name: "IX_JobStatusHistories_JobId",
                table: "JobStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_views",
                table: "job_views");

            migrationBuilder.DropIndex(
                name: "IX_job_views_JobId",
                table: "job_views");

            migrationBuilder.DropIndex(
                name: "IX_job_views_ViewedAt",
                table: "job_views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_skills",
                table: "job_skills");

            migrationBuilder.DropIndex(
                name: "IX_job_skills_SkillId",
                table: "job_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_categories",
                table: "job_categories");

            migrationBuilder.DropIndex(
                name: "IX_job_categories_CategoryId",
                table: "job_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_attachments",
                table: "job_attachments");

            migrationBuilder.RenameTable(
                name: "job_views",
                newName: "JobViews");

            migrationBuilder.RenameTable(
                name: "job_skills",
                newName: "JobSkills");

            migrationBuilder.RenameTable(
                name: "job_categories",
                newName: "JobCategories");

            migrationBuilder.RenameTable(
                name: "job_attachments",
                newName: "JobAttachments");

            migrationBuilder.RenameIndex(
                name: "IX_job_attachments_JobId",
                table: "JobAttachments",
                newName: "IX_JobAttachments_JobId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ViewedAt",
                table: "JobViews",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignedAt",
                table: "JobSkills",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "JobSkills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ProficiencyLevel",
                table: "JobSkills",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AssignedAt",
                table: "JobCategories",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "JobCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "JobAttachments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobViews",
                table: "JobViews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkills",
                table: "JobSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAttachments",
                table: "JobAttachments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_JobId",
                table: "JobCategories",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAttachments_jobs_JobId",
                table: "JobAttachments",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobCategories_jobs_JobId",
                table: "JobCategories",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_jobs_JobId",
                table: "JobSkills",
                column: "JobId",
                principalTable: "jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

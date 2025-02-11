using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IconGeneratorAI.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate23456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_application_role_claims_roles_role_id",
                table: "application_role_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_application_user_claims_users_user_id",
                table: "application_user_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_icon_generations_ai_models_ai_model_id",
                table: "icon_generations");

            migrationBuilder.DropForeignKey(
                name: "fk_icon_generations_users_user_id",
                table: "icon_generations");

            migrationBuilder.DropIndex(
                name: "ix_ai_models_sizes",
                table: "ai_models");

            migrationBuilder.DropColumn(
                name: "size",
                table: "icon_generations");

            migrationBuilder.DropColumn(
                name: "sizes",
                table: "ai_models");

            migrationBuilder.CreateTable(
                name: "ai_model_parameters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ai_model_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    display_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    type = table.Column<short>(type: "SMALLINT", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false),
                    default_value = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    possible_values = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by_user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ai_model_parameters", x => x.id);
                    table.ForeignKey(
                        name: "fk_ai_model_parameters_ai_models_ai_model_id",
                        column: x => x.ai_model_id,
                        principalTable: "ai_models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "icon_generation_parameters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    icon_generation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ai_model_parameter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_by_user_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_icon_generation_parameters", x => x.id);
                    table.ForeignKey(
                        name: "fk_icon_generation_parameters_ai_model_parameters_ai_model_par",
                        column: x => x.ai_model_parameter_id,
                        principalTable: "ai_model_parameters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_icon_generation_parameters_icon_generations_icon_generation",
                        column: x => x.icon_generation_id,
                        principalTable: "icon_generations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ai_model_parameters_ai_model_id",
                table: "ai_model_parameters",
                column: "ai_model_id");

            migrationBuilder.CreateIndex(
                name: "ix_ai_model_parameters_possible_values",
                table: "ai_model_parameters",
                column: "possible_values")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "ix_icon_generation_parameters_ai_model_parameter_id",
                table: "icon_generation_parameters",
                column: "ai_model_parameter_id");

            migrationBuilder.CreateIndex(
                name: "ix_icon_generation_parameters_icon_generation_id",
                table: "icon_generation_parameters",
                column: "icon_generation_id");

            migrationBuilder.AddForeignKey(
                name: "fk_application_role_claims_asp_net_roles_role_id",
                table: "application_role_claims",
                column: "role_id",
                principalTable: "application_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_application_user_claims_asp_net_users_user_id",
                table: "application_user_claims",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_icon_generations_ai_models_ai_model_id",
                table: "icon_generations",
                column: "ai_model_id",
                principalTable: "ai_models",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_icon_generations_users_user_id",
                table: "icon_generations",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_application_role_claims_asp_net_roles_role_id",
                table: "application_role_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_application_user_claims_asp_net_users_user_id",
                table: "application_user_claims");

            migrationBuilder.DropForeignKey(
                name: "fk_icon_generations_ai_models_ai_model_id",
                table: "icon_generations");

            migrationBuilder.DropForeignKey(
                name: "fk_icon_generations_users_user_id",
                table: "icon_generations");

            migrationBuilder.DropTable(
                name: "icon_generation_parameters");

            migrationBuilder.DropTable(
                name: "ai_model_parameters");

            migrationBuilder.AddColumn<string>(
                name: "size",
                table: "icon_generations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sizes",
                table: "ai_models",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_ai_models_sizes",
                table: "ai_models",
                column: "sizes")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.AddForeignKey(
                name: "fk_application_role_claims_roles_role_id",
                table: "application_role_claims",
                column: "role_id",
                principalTable: "application_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_application_user_claims_users_user_id",
                table: "application_user_claims",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_icon_generations_ai_models_ai_model_id",
                table: "icon_generations",
                column: "ai_model_id",
                principalTable: "ai_models",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_icon_generations_users_user_id",
                table: "icon_generations",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

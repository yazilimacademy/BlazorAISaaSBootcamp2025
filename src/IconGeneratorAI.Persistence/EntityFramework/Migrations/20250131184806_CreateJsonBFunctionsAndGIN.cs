using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IconGeneratorAI.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class CreateJsonBFunctionsAndGIN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_ai_models_sizes",
                table: "ai_models",
                column: "sizes")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION jsonb_array_contains(jsonb, text) RETURNS boolean AS $$
                    SELECT $1 @> to_jsonb(array[$2])::jsonb
                $$ LANGUAGE sql IMMUTABLE;

                CREATE OR REPLACE FUNCTION jsonb_array_contains_any(jsonb, text[]) RETURNS boolean AS $$
                    SELECT EXISTS (
                        SELECT 1 FROM jsonb_array_elements_text($1) elem
                        WHERE elem = ANY($2)
                    )
                $$ LANGUAGE sql IMMUTABLE;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_ai_models_sizes",
                table: "ai_models");

            migrationBuilder.Sql(@"
                DROP FUNCTION IF EXISTS jsonb_array_contains(jsonb, text);
                DROP FUNCTION IF EXISTS jsonb_array_contains_any(jsonb, text[]);
            ");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyCarBook.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_carfeaturess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeature_Cars_CarID",
                table: "CarFeature");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeature_Features_FeatureID",
                table: "CarFeature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFeature",
                table: "CarFeature");

            migrationBuilder.RenameTable(
                name: "CarFeature",
                newName: "CarFeatures");

            migrationBuilder.RenameIndex(
                name: "IX_CarFeature_FeatureID",
                table: "CarFeatures",
                newName: "IX_CarFeatures_FeatureID");

            migrationBuilder.RenameIndex(
                name: "IX_CarFeature_CarID",
                table: "CarFeatures",
                newName: "IX_CarFeatures_CarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFeatures",
                table: "CarFeatures",
                column: "CarFeatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Cars_CarID",
                table: "CarFeatures",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Features_FeatureID",
                table: "CarFeatures",
                column: "FeatureID",
                principalTable: "Features",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Cars_CarID",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Features_FeatureID",
                table: "CarFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarFeatures",
                table: "CarFeatures");

            migrationBuilder.RenameTable(
                name: "CarFeatures",
                newName: "CarFeature");

            migrationBuilder.RenameIndex(
                name: "IX_CarFeatures_FeatureID",
                table: "CarFeature",
                newName: "IX_CarFeature_FeatureID");

            migrationBuilder.RenameIndex(
                name: "IX_CarFeatures_CarID",
                table: "CarFeature",
                newName: "IX_CarFeature_CarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarFeature",
                table: "CarFeature",
                column: "CarFeatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeature_Cars_CarID",
                table: "CarFeature",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeature_Features_FeatureID",
                table: "CarFeature",
                column: "FeatureID",
                principalTable: "Features",
                principalColumn: "FeatureID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

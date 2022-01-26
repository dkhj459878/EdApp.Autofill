using Microsoft.EntityFrameworkCore.Migrations;

namespace EdApp.AutoFill.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModelType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributesForSimocalc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    CalculationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributesForSimocalc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributesForSimocalc_CalculationType_CalculationTypeId",
                        column: x => x.CalculationTypeId,
                        principalTable: "CalculationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ModelTypeId = table.Column<int>(nullable: false),
                    CalculationTypeId = table.Column<int>(nullable: false),
                    MandatoryParameter = table.Column<bool>(nullable: false),
                    MandatoryValue = table.Column<bool>(nullable: false),
                    VariableName = table.Column<string>(nullable: true),
                    DescriptionEn = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    ParentEntity = table.Column<string>(nullable: true),
                    ExampleFlatRotorSingleCadge = table.Column<string>(nullable: true),
                    ExampleFlatDoubleCadge = table.Column<string>(nullable: true),
                    ExampleRoundDoubleCadge = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    DesignWireFlatRequest = table.Column<string>(nullable: true),
                    DesignWireFlatResponse = table.Column<string>(nullable: true),
                    DesignWireRoundRequest = table.Column<string>(nullable: true),
                    DesignWireRoundResponse = table.Column<string>(nullable: true),
                    TorqueRequest = table.Column<string>(nullable: true),
                    TorqueResponse = table.Column<string>(nullable: true),
                    ParametersForAllCalculationModules = table.Column<string>(nullable: true),
                    RelevantForHash = table.Column<bool>(nullable: false),
                    UIName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameter_CalculationType_CalculationTypeId",
                        column: x => x.CalculationTypeId,
                        principalTable: "CalculationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parameter_ModelType_ModelTypeId",
                        column: x => x.ModelTypeId,
                        principalTable: "ModelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Jet:Identity", "1, 1"),
                    CalculationTypeId = table.Column<int>(nullable: false),
                    AttributesForSimocalcId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_AttributesForSimocalc_CalculationTypeId",
                        column: x => x.CalculationTypeId,
                        principalTable: "AttributesForSimocalc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attribute_CalculationType_CalculationTypeId",
                        column: x => x.CalculationTypeId,
                        principalTable: "CalculationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_CalculationTypeId",
                table: "Attribute",
                column: "CalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributesForSimocalc_CalculationTypeId",
                table: "AttributesForSimocalc",
                column: "CalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_CalculationTypeId",
                table: "Parameter",
                column: "CalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ModelTypeId",
                table: "Parameter",
                column: "ModelTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "AttributesForSimocalc");

            migrationBuilder.DropTable(
                name: "ModelType");

            migrationBuilder.DropTable(
                name: "CalculationType");
        }
    }
}

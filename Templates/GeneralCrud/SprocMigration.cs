using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace Bpf.Api.Migrations
{
    public partial class adduspRetrieve{!FIELDS}ByFilter : Migration
    {
        private string areaName = "{!AREA}";
        private string sprocName = "uspRetrieve{!FIELDS}ByFilter";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROCEDURE IF EXISTS [{areaName}].[{sprocName}]");
            var sqlFile = Path.Combine(Environment.CurrentDirectory,
                $@"Procedures/{areaName}/{sprocName}.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROCEDURE IF EXISTS [{areaName}].[{sprocName}]");
        }
    }
}

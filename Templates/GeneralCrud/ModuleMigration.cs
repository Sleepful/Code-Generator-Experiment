using Microsoft.EntityFrameworkCore.Migrations;

namespace Bpf.Api.Migrations
{
    public partial class addModulePermissions{!FIELDS} : Migration
    {
        private string policyName = "{!POLICY}";
        private string englishId = "1";
        private string moduleNameEnglish = "{!FIELDS_EN}";
        private string spanishId = "2";
        private string moduleNameSpanish = "{!FIELDS_ES}";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                                INSERT INTO [Access].[utbModules](IsActive) VALUES(1)
                                DECLARE @moduleId int
                                SELECT @moduleId = MAX(ModuleId) FROM [Access].[utbModules]
                                INSERT INTO [Access].[utbTranslationsByModule](ModuleId,LanguageId,ModuleName)
                                VALUES(@moduleId,{englishId},'{moduleNameEnglish}')
                                INSERT INTO [Access].[utbTranslationsByModule](ModuleId,LanguageId,ModuleName)
                                VALUES(@moduleId,{spanishId},'{moduleNameSpanish}')

                                IF NOT EXISTS (SELECT * FROM [Access].[utbPolicies] WHERE PolicyName = '{policyName}')
                                BEGIN
                                    INSERT INTO  [Access].[utbPolicies](PolicyName,IsActive) VALUES('{policyName}',1)
                                END
                                DECLARE @policyId int
                                SELECT @policyId = PolicyId FROM [Access].[utbPolicies] where PolicyName = '{policyName}'
                                
                                INSERT INTO [Access].[utbPoliciesByModule](PolicyId,ModuleId,IsAllowed,HasListAccessLevel,HasReadAccessLevel,HasDeleteAccessLevel,HasAddEditAccessLevel)
                                VALUES(@policyId,@moduleId,1,1,1,1,1)
                                
                                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"	DECLARE @moduleId int
                            SELECT @moduleId = ModuleId  FROM [Access].[utbTranslationsByModule] WHERE ModuleName = '{moduleNameEnglish}'
                            DELETE FROM [Access].[utbTranslationsByModule]  WHERE ModuleId = @moduleId
                            DELETE FROM [Access].[utbPoliciesByModule] WHERE ModuleId = @moduleId
                            DELETE FROM [Access].[utbModules] Where ModuleId = @moduleId
                            ");
        }
    }
}

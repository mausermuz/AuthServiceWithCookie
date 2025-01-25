using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

#nullable disable

namespace AuthServiceBulgakov.DataAccess.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class SQLScript_Migration_AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var test = Path.GetFullPath(@"Scripts\UsersCreate.sql");
            migrationBuilder.Sql(File.ReadAllText(test));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

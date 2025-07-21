using Dapper;
using PlaywrightTest.Configs;
using System.Data.SqlClient;

namespace PlaywrightTest.Utilities
{
    public static class DbHelper
    {

        public static async Task<string> GetAssetName(string assetName)
        {
            using var conn = new SqlConnection(Constants.ConnectionString);
            await conn.OpenAsync();

            return await conn.QueryFirstOrDefaultAsync<string>(
                "SELECT AssetName FROM [AssetTreeData] WHERE [AssetName] = @name",
                new { name = assetName });
        }
    }
}

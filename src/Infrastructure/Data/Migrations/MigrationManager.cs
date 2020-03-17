using Microsoft.EntityFrameworkCore;

namespace Vnit.Infrastructure.Data.Migrations
{
    public static class MigrationManager
    {
        public static void UpdateDatabaseToLatestVersion(DbContext dbContext)
        {
            DatabaseManager.IsDatabaseUpdating = true;
            dbContext.Database.Migrate();
            DatabaseManager.IsDatabaseUpdating = false;
        }
    }
}
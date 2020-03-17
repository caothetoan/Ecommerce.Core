using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Vnit.Infrastructure.Data
{
    public class DatabaseManager
    {

        public static string GetProviderName(string providerAbstractName)
        {
            switch (providerAbstractName.ToLower())
            {

                case "sqlserver":
                    return "System.Data.SqlClient";
                case "sqlce":
                    return "System.Data.SqlServerCe.4.0";
                case "mysql":
                    return "MySql.Data.MySqlClient";
            }
            return string.Empty;
        }


        public static bool IsMigrationRunning { get; set; } = true;

        public static bool IsDatabaseUpdating { get; set; } = false;
    }
}

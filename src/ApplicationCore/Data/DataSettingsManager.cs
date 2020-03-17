
namespace Vnit.ApplicationCore.Data
{
    public class DataSettingsManager
    {
        #region Fields

        private static bool? _databaseIsInstalled;

        #endregion
        #region Properties

        /// <summary>
        /// Gets a value indicating whether database is already installed
        /// </summary>
        public static bool DatabaseIsInstalled
        {
            get
            {
                if (!_databaseIsInstalled.HasValue)
                    //_databaseIsInstalled = !string.IsNullOrEmpty(LoadSettings(reloadSettings: true)?.DataConnectionString);
                    _databaseIsInstalled = true;
                return _databaseIsInstalled.Value;
            }
        }

        #endregion
    }
}

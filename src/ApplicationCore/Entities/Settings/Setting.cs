using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Settings
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    public partial class Setting : BaseEntity, ILocalizedEntity
    {
        public Setting()
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="value">Value</param>
        /// <param name="groupName"></param>
        /// <param name="storeId">Store identifier</param>
        public Setting(string name, string value, string groupName, int storeId = 0)
        {
            this.Name = name;
            this.Value = value;
            this.GroupName = groupName;
            this.StoreId = storeId;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the store for which this setting is valid. 0 is set when the setting is for all stores
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>Result</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

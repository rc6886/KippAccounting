using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KippAccounting.DataAccess
{
    public class ConfigurationSettings : IConfigurationSettings
    {
        private readonly string _connectionString;

        public ConfigurationSettings(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString { get { return _connectionString; } }
    }
}

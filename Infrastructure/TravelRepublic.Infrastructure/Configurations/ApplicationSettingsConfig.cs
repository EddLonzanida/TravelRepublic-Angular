using System.Collections.Generic;

namespace TravelRepublic.Infrastructure.Configurations
{
    //Properties here should match the config file entries.
    public class ApplicationSettingsConfig
    {
        public int IntellisenseCount { get; set; }

        public int PageSize { get; set; }

        public List<string> WhiteList { get; set; }
    }
}

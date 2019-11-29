using Eml.ConfigParser;
using Microsoft.Extensions.Configuration;

namespace TravelRepublic.Infrastructure.Configurations
{
    public class TravelRepublicConnectionStringParser : ConfigParserBase<string, TravelRepublicConnectionStringParser>
    {
        /// <summary>
        /// DI signature: <![CDATA[IConfigParserBase<string, TravelRepublicConnectionStringParser> travelRepublicConnectionStringParser]]>.
        /// </summary>
        public TravelRepublicConnectionStringParser(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

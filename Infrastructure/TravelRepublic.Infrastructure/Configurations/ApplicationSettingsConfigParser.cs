using Eml.ConfigParser;
using Eml.ConfigParser.Parsers;
using Microsoft.Extensions.Configuration;

namespace TravelRepublic.Infrastructure.Configurations
{
    public class ApplicationSettingsConfigParser : ConfigParserBase<ApplicationSettingsConfig, ApplicationSettingsConfigParser>
    {
		/// <summary>
		/// DI signature: <![CDATA[IConfigParserBase<ApplicationSettingsConfig, ApplicationSettingsConfigParser> applicationSettingsConfigParser]]>.
		/// </summary>
		public ApplicationSettingsConfigParser(IConfiguration configuration) 
			: base(configuration, new ComplexTypeConfigParser<ApplicationSettingsConfig>())
        {
        }
    }
}

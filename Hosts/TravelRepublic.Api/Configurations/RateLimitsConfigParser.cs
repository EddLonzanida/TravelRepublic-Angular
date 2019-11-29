using AspNetCoreRateLimit;
using Eml.ConfigParser;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TravelRepublic.Api.Configurations
{
    public class RateLimitsConfigParser : ConfigParserBase<List<RateLimitRule>, RateLimitsConfigParser>
    {
        /// <summary>
        /// DI signature: <![CDATA[IConfigParserBase<List<RateLimitRule>, RateLimitsConfigParser>  RateLimitsConfigParser]]>.
        /// </summary>
        public RateLimitsConfigParser(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

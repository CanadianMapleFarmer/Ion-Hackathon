using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace MultiAgentBot.plugins
{
    public class MarketPlugin
    {
        private readonly ILogger<MarketPlugin> _logger;
        private readonly List<string> _markets;

        public MarketPlugin()
        {
            //_logger = loggerFactory.CreateLogger<MarketPlugin>();
            _markets = new List<string>
            {
                "Market 1",
                "Market 2",
                "Market 3",
                "Market 4",
                "Market 5",
                "Market 6",
                "Market 7",
                "Market 8",
                "Market 9",
                "Market 10"
            };
        }

        [KernelFunction("GetMarkets")]
        [Description("Gets all of the market information")]
        [return: Description("A list of markets")]
        public List<string> GetMarkets()
        {
            return _markets;
        }

    }

    public class RequirementPlugin
    {
        private readonly ILogger<RequirementPlugin> _logger;

        public RequirementPlugin()
        {
            //_logger = loggerFactory.CreateLogger<MarketPlugin>();
            
        }

        [KernelFunction("GetMarketRequirements")]
        [Description("Gets the requirements for a specific market")]
        [return: Description("The requirements for the market")]
        public string GetMarketRequirements(string market)
        {
            return "Requirements for " + market;
        }
    }
}

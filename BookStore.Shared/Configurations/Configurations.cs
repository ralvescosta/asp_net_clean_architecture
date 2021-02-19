using BookStore.Shared.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BookStore.Shared.Configurations
{
    public class Configurations : IConfigurations
    {
        private readonly IConfigurationSection appConfigs;
        public Configurations(IConfiguration configurations)
        {
            appConfigs = configurations.GetSection("AppConfiguration");
        }

        public string JwtScrete => appConfigs.GetValue<string>("JwtSecrete");
    }
}

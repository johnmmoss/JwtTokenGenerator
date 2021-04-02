using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace JwtTokenGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            var tokenSettings = configuration.GetSection(nameof(TokenSettings))
                .Get<TokenSettings>();

            var tokenGenerator = new TokenGenerator(tokenSettings);

            Console.WriteLine(tokenGenerator.Generate());
            Console.WriteLine();
        }
    }
}

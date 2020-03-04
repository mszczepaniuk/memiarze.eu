using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationTests
{
    public static class TestHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddUserSecrets("ef0b3d9d-e242-4900-a161-080434f576e8")
                .Build();
            return config;
        }
    }
}

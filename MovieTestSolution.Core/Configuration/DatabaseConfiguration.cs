using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Core.Configuration
{
    public static class DatabaseConfiguration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MovieTestSolution.WebApplication"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("Default");
            }
        }
    }
}

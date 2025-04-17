using Microsoft.Extensions.Configuration;

namespace ClassifiedsApp.Infrastructure.Persistence.Context;

public static class Configuration
{
	static public string ConnectionString
	{
		get
		{
			ConfigurationManager configurationManager = new();
			try
			{
				configurationManager.SetBasePath(Directory.GetCurrentDirectory());

				configurationManager.AddJsonFile("appsettings.json");
			}
			catch
			{
				configurationManager.AddJsonFile("appsettings.Production.json");
			}

			return configurationManager.GetConnectionString("Default")!;
		}
	}
}


// OLD Version.
/*

static public string ConnectionString
	{
		get
		{
			ConfigurationManager configurationManager = new();
			try
			{
				//configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../../API/ClassifiedsApp.API"));
				configurationManager
				.SetBasePath("D:\\Visual Programming Codes\\C Sharp Codes\\Final Projects\\Classifieds App\\Server\\ClassifiedsApp\\API\\ClassifiedsApp.API");

				"[17:39:46 ERR] Dir: D:\\Visual Programming Codes\\C Sharp Codes\\Final Projects\\Classifieds App\\Server\\ClassifiedsApp\\API\\ClassifiedsApp.API |"

				configurationManager.AddJsonFile("appsettings.json");
				Log.Error($"Dir: {Directory.GetCurrentDirectory()} |");

			}
			catch
			{
				configurationManager.AddJsonFile("appsettings.Production.json");
			}

			return configurationManager.GetConnectionString("Default")!;
		}
	}

*/
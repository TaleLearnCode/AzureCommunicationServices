using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(TaleLearnCode.CommunicationServices.Functions.Startup))]
namespace TaleLearnCode.CommunicationServices.Functions
{

	/// <summary>
	/// Overrides the internal <see cref="FunctionsStartup"/> in order to perform dependency injection.
	/// </summary>
	public class Startup : FunctionsStartup
	{

		/// <summary>
		/// Configures the Azure Function to include injection of necessary services.
		/// </summary>
		/// <param name="builder">The builder to use for configuring the Azure Function.</param>
		public override void Configure(IFunctionsHostBuilder builder)
		{

			AzureStorageSettings azureStorageSettings = new AzureStorageSettings()
			{
				AccountKey = Environment.GetEnvironmentVariable("AzureStorageAccountKey"),
				AccountName = Environment.GetEnvironmentVariable("AzureStorageAccountName"),
				Url = Environment.GetEnvironmentVariable("AzureStorageUrl")
			};

			builder.Services.AddSingleton<ISMSService>((s) =>
			{
				return new SMSService(Environment.GetEnvironmentVariable("ACSConnectionString"), azureStorageSettings, Environment.GetEnvironmentVariable("MessageArchiveTable"), Environment.GetEnvironmentVariable("ACSPhoneNumber"));
			});

		}

	}
}
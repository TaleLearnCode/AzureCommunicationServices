using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.Functions
{

	public static class SMSDeliveryConfirmation
	{

		[FunctionName("SMSDeliveryConfirmation")]
		public static void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
		{
			log.LogInformation(eventGridEvent.Data.ToString());

			MessageDeliveryReport messageDeliveryReport = JsonConvert.DeserializeObject<MessageDeliveryReport>(eventGridEvent.Data.ToString());

			// TODO: SMSService/AzureStorageSettings should injected
			AzureStorageSettings azureStorageSettings = new AzureStorageSettings()
			{
				AccountKey = Environment.GetEnvironmentVariable("AzureStorageAccountKey"),
				AccountName = Environment.GetEnvironmentVariable("AzureStorageAccountName"),
				Url = Environment.GetEnvironmentVariable("AzureStorageUrl")
			};

			SMSService smsService = new SMSService(azureStorageSettings);
			smsService.AddDeliveryConfirmation(messageDeliveryReport.To, messageDeliveryReport.MessageId, messageDeliveryReport.DeliveryStatus, messageDeliveryReport.DeliveryStatusDetail, messageDeliveryReport.ReceivedTimestamp);

		}

	}

}
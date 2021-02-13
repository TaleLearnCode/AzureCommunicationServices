// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaleLearnCode.CommunicationServices;

namespace SMSFunctions
{
	public static class SMSDeliveryConfirmation
	{
		[FunctionName("SMSDeliveryConfirmation")]
		public static void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
		{
			log.LogInformation(eventGridEvent.Data.ToString());

			MessageDeliveryReport messageDeliveryReport = JsonConvert.DeserializeObject<MessageDeliveryReport>(eventGridEvent.Data.ToString());

			// TODO: SMSService/AzureStorageSettings should injected
			SMSService smsService = new SMSService(Settings.AzureStorageSettings);
			smsService.AddDeliveryConfirmation(messageDeliveryReport.To, messageDeliveryReport.MessageId, messageDeliveryReport.DeliveryStatus, messageDeliveryReport.DeliveryStatusDetail, messageDeliveryReport.ReceivedTimestamp);

		}
	}
}

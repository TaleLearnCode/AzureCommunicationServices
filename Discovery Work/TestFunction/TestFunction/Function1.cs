// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Azure.Communication;
using Azure.Communication.Sms;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TestFunction
{
	public static class Function1
	{
		[FunctionName("Function1")]
		public static void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
		{
			log.LogInformation(eventGridEvent.Data.ToString());

			SMSReceived smsReceived = JsonConvert.DeserializeObject<SMSReceived>(eventGridEvent.Data.ToString());

			string returnMessage = string.Empty;
			switch (smsReceived.message)
			{
				case "Use Case 1":
					returnMessage = "Results for Use Case 1";
					break;
				case "Use Case 2":
					returnMessage = "Results for Use Case 2";
					break;
				default:
					returnMessage = "I didn't understand";
					break;
			}

			SmsClient smsClient = new SmsClient(Settings.ACSConnectionString);
			smsClient.Send(
				from: new PhoneNumber(Settings.ACSPhoneNumber),
				to: new PhoneNumber(Settings.ConsumerPhoneNumber),
				message: returnMessage,
				new SendSmsOptions { EnableDeliveryReport = true } // optional
			);
		}
	}
}

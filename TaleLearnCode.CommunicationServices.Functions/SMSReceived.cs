// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.Functions
{

	/// <summary>
	/// Handles incoming SMS messages.
	/// </summary>
	public class SMSReceived
	{

		private readonly ISMSService _smsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="SMSReceived"/> class.
		/// </summary>
		/// <param name="smsService">Reference to the <see cref="ISMSService"/> instance to use for interacting with the SMS services.</param>
		public SMSReceived(ISMSService smsService)
		{
			_smsService = smsService;
		}

		/// <summary>
		/// Handles incoming SMS messages.
		/// </summary>
		/// <param name="eventGridEvent">Properties of the event published to the Event Grid topic.</param>
		/// <param name="log">Represents the logger.</param>
		[FunctionName("SMSReceived")]
		public void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
		{
			//log.LogInformation(eventGridEvent.Data.ToString());
			IncomingSMSMessage incomingMessage = JsonConvert.DeserializeObject<IncomingSMSMessage>(eventGridEvent.Data.ToString());
			log.LogWarning(_smsService.ProcessIncomingMessage(incomingMessage));
		}

	}

}

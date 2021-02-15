using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.Functions
{

	/// <summary>
	/// Handles the receipt of a SMS delivery report by the Azure Communication Service.
	/// </summary>
	public class SMSDeliveryConfirmation
	{

		private readonly ISMSService _smsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="SMSDeliveryConfirmation"/> class.
		/// </summary>
		/// <param name="smsService">Reference to the <see cref="ISMSService"/> instance to use for interacting with the SMS services.</param>
		public SMSDeliveryConfirmation(ISMSService smsService)
		{
			_smsService = smsService;
		}

		/// <summary>
		/// Handles the receipt of a SMS delivery report by the Azure Communication Service.
		/// </summary>
		/// <param name="eventGridEvent">Properties of the event published to the Event Grid topic.</param>
		/// <param name="log">Represents the logger.</param>
		/// <remarks>This function will update the saved message details with delivery confirmation information.</remarks>
		[FunctionName("SMSDeliveryConfirmation")]
		public void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
		{
			log.LogInformation(eventGridEvent.Data.ToString());
			MessageDeliveryReport messageDeliveryReport = JsonConvert.DeserializeObject<MessageDeliveryReport>(eventGridEvent.Data.ToString());
			_smsService.AddDeliveryConfirmation(messageDeliveryReport.To, messageDeliveryReport.MessageId, messageDeliveryReport.DeliveryStatus, messageDeliveryReport.DeliveryStatusDetail, messageDeliveryReport.ReceivedTimestamp);
		}

	}

}
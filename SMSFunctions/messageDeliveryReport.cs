using Newtonsoft.Json;

namespace SMSFunctions
{

	public class MessageDeliveryReport
	{

		[JsonProperty("messageId")]
		public string MessageId { get; set; }

		[JsonProperty("from")]
		public string From { get; set; }

		[JsonProperty("to")]
		public string To { get; set; }

		[JsonProperty("receivedTimestamp")]
		public string ReceivedTimestamp { get; set; }

		[JsonProperty("deliveryStatus")]
		public string DeliveryStatus { get; set; }

		[JsonProperty("deliveryStatusDetails")]
		public string DeliveryStatusDetail { get; set; }

	}

}
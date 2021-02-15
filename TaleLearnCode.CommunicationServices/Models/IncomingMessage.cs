using Newtonsoft.Json;

namespace TaleLearnCode.CommunicationServices.Models
{

	public class IncomingMessage
	{

		[JsonProperty("messageId")]
		public string MessageId { get; set; }

		[JsonProperty("from")]
		public string From { get; set; }

		[JsonProperty("to")]
		public string To { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("receivedTimestamp")]
		public string ReceivedTimestamp { get; set; }

	}

}
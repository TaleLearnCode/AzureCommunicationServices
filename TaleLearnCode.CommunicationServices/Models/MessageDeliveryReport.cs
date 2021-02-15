using Newtonsoft.Json;

namespace TaleLearnCode.CommunicationServices.Models
{

	/// <summary>
	/// Represents a SMS message delivery report.
	/// </summary>
	public class MessageDeliveryReport
	{

		/// <summary>
		/// Gets or sets the message identifier.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the identifier for the message.
		/// </value>
		[JsonProperty("messageId")]
		public string MessageId { get; set; }

		/// <summary>
		/// Gets or sets the phone number where the message originated.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the origin phone number.
		/// </value>
		[JsonProperty("from")]
		public string From { get; set; }

		/// <summary>
		/// Gets or sets the phone number where the message was sent.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the destination phone number.
		/// </value>
		[JsonProperty("to")]
		public string To { get; set; }

		/// <summary>
		/// Gets or sets the received timestamp.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the date and time the message was received.
		/// </value>
		[JsonProperty("receivedTimestamp")]
		public string ReceivedTimestamp { get; set; }

		/// <summary>
		/// Gets or sets the delivery status.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the delivery status of the message.
		/// </value>
		[JsonProperty("deliveryStatus")]
		public string DeliveryStatus { get; set; }

		/// <summary>
		/// Gets or sets the delivery status detail.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the details of the delivery status.
		/// </value>
		[JsonProperty("deliveryStatusDetails")]
		public string DeliveryStatusDetail { get; set; }

	}

}

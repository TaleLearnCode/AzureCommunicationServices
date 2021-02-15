using Newtonsoft.Json;

namespace TaleLearnCode.CommunicationServices.Models
{

	/// <summary>
	/// Represents an incoming SMS Message.
	/// </summary>
	public class IncomingSMSMessage

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
		/// Gets or sets the phone number sending the message.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the originating phone number.
		/// </value>
		[JsonProperty("from")]
		public string From { get; set; }

		/// <summary>
		/// Gets or sets the phone number receiving the message.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the receiving phone number.
		/// </value>
		[JsonProperty("to")]
		public string To { get; set; }

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the message.
		/// </value>
		[JsonProperty("message")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the received timestamp.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the date and time the message was received.
		/// </value>
		[JsonProperty("receivedTimestamp")]
		public string ReceivedTimestamp { get; set; }

	}

}
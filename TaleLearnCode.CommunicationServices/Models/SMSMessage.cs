namespace TaleLearnCode.CommunicationServices.Models
{
	public class SMSMessage
	{

		/// <summary>
		/// Gets or sets the phone number sending the SMS message.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the phone number that sent the SMS message.
		/// </value>
		public string FromPhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the phone number the SMS message is being sent to.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the recipient of the SMS message.
		/// </value>
		public string ToPhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the SMS message text.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the text of the SMS message.
		/// </value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the message identifier.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing SMS message's identifier.
		/// </value>
		public string MessageId { get; set; }

		/// <summary>
		/// Gets or sets the delivery status of the message.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the SMS message delivery status.
		/// </value>
		/// <remarks>This value will be null until a delivery confirmation report is received.</remarks>
		public string DeliveryStatus { get; set; }

		/// <summary>
		/// Gets or sets the delivery status detail.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the detail of the delivery status.
		/// </value>
		/// <remarks>This value will be null until a delivery confirmation report is received.</remarks>
		public string DeliveryStatusDetail { get; set; }

		/// <summary>
		/// Gets or sets the received timestamp.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the timestamp the SMS message was received.
		/// </value>
		public string ReceivedTimestamp { get; set; }

	}

}
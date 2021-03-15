namespace TaleLearnCode.CommunicationServices.Models
{

	public class IncomingSMSEvent : IncomingSMSMessage
	{

		/// <summary>
		/// Gets or sets the type of the incoming SMS event being processed.
		/// </summary>
		/// <value>
		/// A <see cref="IncomingSMSEventType"/> representing the type of message being processed.
		/// </value>
		public IncomingSMSEventType IncomingSMSEventType { get; set; }

		/// <summary>
		/// Gets or sets the response message identifier.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the identifier of the automatic response message.
		/// </value>
		public string ResponseMessageId { get; set; }

		/// <summary>
		/// Gets or sets the response message text.
		/// </summary>
		/// <value>
		/// A <c>string</c> representing the text of the automatic response message.
		/// </value>
		public string ResponseMessageText { get; set; }

	}

}
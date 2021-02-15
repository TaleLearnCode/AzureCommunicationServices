using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices
{

	/// <summary>
	/// Interface for types providing SMS (short message service) services.
	/// </summary>
	public interface ISMSService
	{

		/// <summary>
		/// Adds delivery confirmation information to a sent message log.
		/// </summary>
		/// <param name="toPhoneNumber">The recipient's phone number.</param>
		/// <param name="messageId">Identifier of the sent message.</param>
		/// <param name="deliveryStatus">The delivery status of the message.</param>
		/// <param name="deliveryStatusDetail">Details of the delivery status.</param>
		/// <param name="receivedTimestamp">Timestamp when the message was received.</param>
		void AddDeliveryConfirmation(string toPhoneNumber, string messageId, string deliveryStatus, string deliveryStatusDetail, string receivedTimestamp);

		/// <summary>
		/// Retrieves the log of a sent message.
		/// </summary>
		/// <param name="toPhoneNumber">The recipient's phone number.</param>
		/// <param name="messageId">Identifier of the sent message.</param>
		/// <returns>A <see cref="SMSMessage"/> representing the log of the sent message.</returns>
		SMSMessage RetrieveSentMessage(string toPhoneNumber, string messageId);

		/// <summary>
		/// Sends the specified SMS message.
		/// </summary>
		/// <param name="toPhoneNumber">The recipient's phone number.</param>
		/// <param name="message">Text of the message to be sent.</param>
		/// <param name="enableDeliveryReport">If set to <c>true</c> the delivery report will be enabled.</param>
		/// <returns>A <c>string</c> representing the message identifier.</returns>
		string SendSMS(string toPhoneNumber, string message, bool enableDeliveryReport = true);

		/// <summary>
		/// Processes an incoming SMS message.
		/// </summary>
		/// <param name="incomingMessage">The incoming message.</param>
		void ProcessIncomingMessage(IncomingSMSMessage incomingMessage);

	}

}
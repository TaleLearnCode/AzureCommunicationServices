using TaleLearnCode.CommunicationServices.AzureStorage;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.Extensions
{

	/// <summary>
	/// Provides extensions to the <see cref="IncomingSMSMessage"/> type.
	/// </summary>
	public static class IncomingMessageExtensions
	{

		/// <summary>
		/// Converts the <see cref="IncomingSMSMessage"/> to a <see cref="SMSMessage"/>.
		/// </summary>
		/// <param name="incomingMessage">The incoming message to be converted.</param>
		/// <returns>A <see cref="SMSMessage"/> representing the <paramref name="incomingMessage"/>.</returns>
		public static SMSMessage ToSMSMessage(this IncomingSMSMessage incomingMessage)
		{
			return new SMSMessage()
			{
				MessageId = incomingMessage.MessageId,
				FromPhoneNumber = incomingMessage.From,
				ToPhoneNumber = incomingMessage.To,
				Message = incomingMessage.Message,
				ReceivedTimestamp = incomingMessage.ReceivedTimestamp
			};
		}

		/// <summary>
		/// Converts the <see cref="IncomingSMSMessage"/> to a <see cref="SMSMessageTableEntity"/>.
		/// </summary>
		/// <param name="incomingMessage">The incoming message to be converted.</param>
		/// <returns>A <see cref="SMSMessageTableEntity"/> representing the <paramref name="incomingMessage"/>.</returns>
		public static SMSMessageTableEntity ToSMSMessageTableEntity(this IncomingSMSMessage incomingMessage)
		{
			return new SMSMessageTableEntity()
			{
				PartitionKey = incomingMessage.From,
				RowKey = incomingMessage.MessageId,
				FromPhoneNumber = incomingMessage.From,
				ToPhoneNumber = incomingMessage.To,
				Message = incomingMessage.Message,
				MessageId = incomingMessage.MessageId
			};

		}

	}
}
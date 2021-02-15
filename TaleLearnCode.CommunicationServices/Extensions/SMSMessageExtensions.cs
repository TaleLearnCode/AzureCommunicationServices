using TaleLearnCode.CommunicationServices.AzureStorage;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.Extensions
{

	/// <summary>
	/// Provides extension to the <see cref="SMSMessage"/> type.
	/// </summary>
	public static class SMSMessageExtensions
	{

		/// <summary>
		/// Converts the <see cref="SMSMessage"/> to a <see cref="SMSMessageTableEntity"/>.
		/// </summary>
		/// <param name="smsMessage">The SMS message to be converted.</param>
		/// <returns>A <see cref="SMSMessageTableEntity"/> representing the <paramref name="smsMessage"/>.</returns>
		public static SMSMessageTableEntity ToSMSMessageTableEntity(this SMSMessage smsMessage)
		{
			return new SMSMessageTableEntity()
			{
				PartitionKey = smsMessage.ToPhoneNumber,
				RowKey = smsMessage.MessageId,
				FromPhoneNumber = smsMessage.FromPhoneNumber,
				ToPhoneNumber = smsMessage.ToPhoneNumber,
				Message = smsMessage.Message,
				MessageId = smsMessage.MessageId,
				DeliveryStatus = smsMessage.DeliveryStatus,
				DeliveryStatusDetail = smsMessage.DeliveryStatusDetail,
				ReceivedTimestamp = smsMessage.ReceivedTimestamp
			};
		}

	}

}
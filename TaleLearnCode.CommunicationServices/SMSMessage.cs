using Azure;
using Azure.Data.Tables;
using System;
using System.Linq;

namespace TaleLearnCode.CommunicationServices
{
	public class SMSMessage : ITableEntity
	{

		public string PartitionKey { get; set; }
		public string RowKey { get; set; }
		public DateTimeOffset? Timestamp { get; set; }
		public ETag ETag { get; set; }

		public string FromPhoneNumber { get; set; }

		public string ToPhoneNumber { get; set; }

		public string Message { get; set; }

		public string MessageId { get; set; }

		public string DeliveryStatus { get; set; }

		public string DeliveryStatusDetail { get; set; }

		public string ReceivedTimestamp { get; set; }

		public SMSMessage() : base() { }

		public SMSMessage(string fromPhoneNumber, string toPhoneNumber, string message, string messageId)
		{
			PartitionKey = toPhoneNumber;
			RowKey = messageId;
			FromPhoneNumber = fromPhoneNumber;
			ToPhoneNumber = toPhoneNumber;
			Message = message;
			MessageId = messageId;
		}

		public static void Save(SMSMessage smsMessage, AzureStorageSettings azureStorageSettings)
		{
			AzureStorageHelper.GetTableClient(azureStorageSettings, "SentMessages").UpsertEntity(smsMessage);
		}

		public static SMSMessage Retrieve(string toPhoneNumber, string messageId, AzureStorageSettings azureStorageSettings)
		{

			SMSMessage results = AzureStorageHelper.GetTableClient(azureStorageSettings, "SentMessages")
				.Query<SMSMessage>(t => t.PartitionKey == toPhoneNumber && t.RowKey == messageId)
				.SingleOrDefault();
			return results;
		}
	}
}
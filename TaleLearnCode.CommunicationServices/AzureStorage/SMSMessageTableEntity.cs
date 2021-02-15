using Azure;
using Azure.Data.Tables;
using System;
using System.Linq;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.AzureStorage
{
	public class SMSMessageTableEntity : SMSMessage, ITableEntity
	{

		/// <summary>
		/// The partition key is a unique identifier for the partition within a
		/// given table and forms the first part of an entity's primary key.
		/// </summary>
		/// <value>
		/// A string containing the partition key for the entity.
		/// </value>
		public string PartitionKey { get; set; }

		/// <summary>
		/// The row key is a unique identifier for an entity within a given partition.
		/// Together the <see cref="P:Azure.Data.Tables.ITableEntity.PartitionKey" />
		/// and RowKey uniquely identify every entity within a table.
		/// </summary>
		/// <value>
		/// A string containing the row key for the entity.
		/// </value>
		public string RowKey { get; set; }

		/// <summary>
		/// The Timestamp property is a DateTime value that is maintained on the server
		/// side to record the time an entity was last modified.
		/// The Table service uses the Timestamp property internally to provide optimistic
		/// concurrency. The value of Timestamp is a monotonically increasing value,
		/// meaning that each time the entity is modified, the value of Timestamp increases
		/// for that entity. This property should not be set on insert or update operations (the value will be ignored).
		/// </summary>
		/// <value>
		/// A <see cref="T:System.DateTimeOffset" /> containing the timestamp of the entity.
		/// </value>
		public DateTimeOffset? Timestamp { get; set; }

		/// <summary>
		/// Gets or sets the entity's ETag. Set this value to '*' in order to force an
		/// overwrite to an entity as part of an update operation.
		/// </summary>
		/// <value>
		/// A string containing the ETag value for the entity.
		/// </value>
		public ETag ETag { get; set; }

		public SMSMessageTableEntity() : base() { }

		public SMSMessageTableEntity(string fromPhoneNumber, string toPhoneNumber, string message, string messageId)
		{
			PartitionKey = toPhoneNumber;
			RowKey = messageId;
			FromPhoneNumber = fromPhoneNumber;
			ToPhoneNumber = toPhoneNumber;
			Message = message;
			MessageId = messageId;
		}

		public static void Save(SMSMessageTableEntity smsMessageTableEntity, AzureStorageSettings azureStorageSettings, string messageArchiveTable)
		{
			Helper.GetTableClient(azureStorageSettings, messageArchiveTable).UpsertEntity(smsMessageTableEntity);
		}

		public static SMSMessage Retrieve(string toPhoneNumber, string messageId, AzureStorageSettings azureStorageSettings, string messageArchiveTable)
		{
			SMSMessageTableEntity results = Helper.GetTableClient(azureStorageSettings, messageArchiveTable)
				.Query<SMSMessageTableEntity>(t => t.PartitionKey == toPhoneNumber && t.RowKey == messageId)
				.SingleOrDefault();
			if (results != null)
				return results.ToSMSMesage();
			else
				return null;
		}

		private SMSMessage ToSMSMesage()
		{
			return new SMSMessage()
			{
				MessageId = MessageId,
				FromPhoneNumber = FromPhoneNumber,
				ToPhoneNumber = ToPhoneNumber,
				Message = Message,
				DeliveryStatus = DeliveryStatus,
				DeliveryStatusDetail = DeliveryStatusDetail,
				ReceivedTimestamp = ReceivedTimestamp
			};
		}

	}

}
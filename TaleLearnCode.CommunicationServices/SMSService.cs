using Azure;
using Azure.Communication;
using Azure.Communication.Sms;
using System;
using TaleLearnCode.CommunicationServices.AzureStorage;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices
{

	/// <summary>
	/// Provides services for sending and acting upon SMS messages.
	/// </summary>
	/// <seealso cref="ISMSService" />
	public class SMSService : ISMSService
	{

		private readonly string _fromPhoneNumber;
		private readonly SmsClient _smsClient;
		private readonly AzureStorageSettings _azureStorageSettings;

		/// <summary>
		/// Initializes a new instance of the <see cref="SMSService"/> class.
		/// </summary>
		/// <param name="serviceConnectionString">The configuration for connecting to the SMS service.</param>
		/// <param name="azureStorageSettings"><see cref="AzureStorageSettings"/> representing the setting necessary for connecting to the associated Azure Storage account.</param>
		/// <param name="fromPhoneNumber">The phone number to use for sending SMS messages.</param>
		public SMSService(string serviceConnectionString, AzureStorageSettings azureStorageSettings, string fromPhoneNumber)
		{
			_fromPhoneNumber = fromPhoneNumber;
			_smsClient = new SmsClient(serviceConnectionString);
			_azureStorageSettings = azureStorageSettings;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SMSService"/> class.
		/// </summary>
		/// <param name="azureStorageSettings">The azure storage settings.</param>
		public SMSService(AzureStorageSettings azureStorageSettings)
		{
			_azureStorageSettings = azureStorageSettings;
		}

		/// <summary>
		/// Sends the specified SMS message.
		/// </summary>
		/// <param name="toPhoneNumber">The recipient's phone number.</param>
		/// <param name="message">Text of the message to be sent.</param>
		/// <param name="enableDeliveryReport">If set to <c>true</c> the delivery report will be enabled.</param>
		/// <returns>A <c>string</c> representing the message identifier.</returns>
		/// <exception cref="Exception">Thrown if the SMS client or originating phone number was not initialized correctly.</exception>
		public string SendSMS(string toPhoneNumber, string message, bool enableDeliveryReport = true)
		{

			if (_smsClient is null) throw new Exception("The SMS Client was not initialized correctly.");
			if (string.IsNullOrWhiteSpace(_fromPhoneNumber)) throw new Exception("Need to initialize the from phone number first.");

			Response<SendSmsResponse> response = _smsClient.Send(
				from: new PhoneNumber(_fromPhoneNumber),
				to: new PhoneNumber(toPhoneNumber),
				message: message,
				new SendSmsOptions { EnableDeliveryReport = enableDeliveryReport });

			SMSMessageTableEntity.Save(new SMSMessageTableEntity(_fromPhoneNumber, toPhoneNumber, message, response.Value.MessageId), _azureStorageSettings);

			return response.Value.MessageId;

		}

		/// <summary>
		/// Adds delivery confirmation information to a sent message log.
		/// </summary>
		/// <param name="toPhoneNumber">The recipient's phone number.</param>
		/// <param name="messageId">Identifier of the sent message.</param>
		/// <param name="deliveryStatus">The delivery status of the message.</param>
		/// <param name="deliveryStatusDetail">Details of the delivery status.</param>
		/// <param name="receivedTimestamp">Timestamp when the message was received.</param>
		public void AddDeliveryConfirmation(string toPhoneNumber, string messageId, string deliveryStatus, string deliveryStatusDetail, string receivedTimestamp)
		{
			SMSMessage smsMessage = SMSMessageTableEntity.Retrieve(toPhoneNumber, messageId, _azureStorageSettings);
			smsMessage.DeliveryStatus = deliveryStatus;
			smsMessage.DeliveryStatusDetail = deliveryStatusDetail;
			smsMessage.ReceivedTimestamp = receivedTimestamp;
			SMSMessageTableEntity.Save(smsMessage, _azureStorageSettings);
		}

		/// <summary>
		/// Retrieves the log of a sent message.
		/// </summary>
		/// <param name="toPhoneNumber">The recipient's phone number.</param>
		/// <param name="messageId">Identifier of the sent message.</param>
		/// <returns>
		/// A <see cref="SMSMessage" /> representing the log of the sent message.
		/// </returns>
		public SMSMessage RetrieveSentMessage(string toPhoneNumber, string messageId)
		{
			return SMSMessageTableEntity.Retrieve(toPhoneNumber, messageId, _azureStorageSettings);
		}


	}

}
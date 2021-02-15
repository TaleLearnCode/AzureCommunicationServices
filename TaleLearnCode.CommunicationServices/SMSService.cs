using Azure;
using Azure.Communication;
using Azure.Communication.Sms;
using System;
using TaleLearnCode.CommunicationServices.AzureStorage;
using TaleLearnCode.CommunicationServices.Extensions;
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
		private readonly string _messageArchiveTable;

		/// <summary>
		/// Initializes a new instance of the <see cref="SMSService"/> class.
		/// </summary>
		/// <param name="serviceConnectionString">The configuration for connecting to the SMS service.</param>
		/// <param name="azureStorageSettings"><see cref="AzureStorageSettings"/> representing the setting necessary for connecting to the associated Azure Storage account.</param>
		/// <param name="messageArchiveTable">Name of the Azure Storage table where to store the message archive.</param>
		/// <param name="fromPhoneNumber">The phone number to use for sending SMS messages.</param>
		public SMSService(string serviceConnectionString, AzureStorageSettings azureStorageSettings, string messageArchiveTable, string fromPhoneNumber)
		{
			_fromPhoneNumber = fromPhoneNumber;
			_smsClient = new SmsClient(serviceConnectionString);
			_azureStorageSettings = azureStorageSettings;
			_messageArchiveTable = messageArchiveTable;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SMSService"/> class.
		/// </summary>
		/// <param name="azureStorageSettings">The azure storage settings.</param>
		/// <param name="messageArchiveTable">Name of the Azure Storage table where to store the message archive.</param>
		public SMSService(AzureStorageSettings azureStorageSettings, string messageArchiveTable)
		{
			_azureStorageSettings = azureStorageSettings;
			_messageArchiveTable = messageArchiveTable;
		}

		/// <summary>
		/// Sends the specified SMS message.
		/// </summary>
		/// <param name="toPhoneNumber">The destination phone number.</param>
		/// <param name="message">Text of the message to be sent.</param>
		/// <param name="enableDeliveryReport">If set to <c>true</c> the delivery report will be enabled.</param>
		/// <returns>A <c>string</c> representing the sent message identifier.</returns>
		/// <exception cref="Exception">Thrown if the originating phone number is not initialized.</exception>
		public string SendSMS(string toPhoneNumber, string message, bool enableDeliveryReport = true)
		{
			if (string.IsNullOrWhiteSpace(_fromPhoneNumber)) throw new Exception("Need to initialize the from phone number first.");
			return SendSMS(_fromPhoneNumber, toPhoneNumber, message, enableDeliveryReport);
		}

		/// <summary>
		/// Sends the specified SMS message.
		/// </summary>
		/// <param name="fromPhoneNumber">The origin phone number</param>
		/// <param name="toPhoneNumber">The destination phone number.</param>
		/// <param name="message">Text of the message to be sent.</param>
		/// <param name="enableDeliveryReport">If set to <c>true</c> the delivery report will be enabled.</param>
		/// <returns>A <c>string</c> representing the sent message identifier.</returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown if <paramref name="fromPhoneNumber"/>, <paramref name="toPhoneNumber"/>,
		/// or <paramref name="message"/> are not specified.
		/// </exception>
		/// <exception cref="Exception">Thrown if the SMS client was not initialized correctly.</exception>
		public string SendSMS(string fromPhoneNumber, string toPhoneNumber, string message, bool enableDeliveryReport = true)
		{

			if (string.IsNullOrWhiteSpace(fromPhoneNumber)) throw new ArgumentNullException(nameof(fromPhoneNumber));
			if (string.IsNullOrWhiteSpace(toPhoneNumber)) throw new ArgumentNullException(nameof(toPhoneNumber));
			if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));
			if (_smsClient is null) throw new Exception("The SMS Client was not initialized correctly.");

			Response<SendSmsResponse> response = _smsClient.Send(
				from: new PhoneNumber(fromPhoneNumber),
				to: new PhoneNumber(toPhoneNumber),
				message: message,
				new SendSmsOptions { EnableDeliveryReport = enableDeliveryReport });

			SMSMessageTableEntity.Save(
				new SMSMessage()
				{
					FromPhoneNumber = _fromPhoneNumber,
					ToPhoneNumber = toPhoneNumber,
					Message = message,
					MessageId = response.Value.MessageId
				}.ToSMSMessageTableEntity(),
				_azureStorageSettings,
				_messageArchiveTable);

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
			SMSMessage smsMessage = SMSMessageTableEntity.Retrieve(toPhoneNumber, messageId, _azureStorageSettings, _messageArchiveTable);
			smsMessage.DeliveryStatus = deliveryStatus;
			smsMessage.DeliveryStatusDetail = deliveryStatusDetail;
			smsMessage.ReceivedTimestamp = receivedTimestamp;
			SMSMessageTableEntity.Save(smsMessage.ToSMSMessageTableEntity(), _azureStorageSettings, _messageArchiveTable);
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
			return SMSMessageTableEntity.Retrieve(toPhoneNumber, messageId, _azureStorageSettings, _messageArchiveTable);
		}

		/// <summary>
		/// Processes an incoming SMS message.
		/// </summary>
		/// <param name="incomingMessage">The incoming message.</param>
		public void ProcessIncomingMessage(IncomingSMSMessage incomingMessage)
		{
			string message = incomingMessage.Message.ToLower();
			string returnMessage;
			if (message.Contains("price")
				|| message.Contains("pricing")
				|| message.Contains("cost")
				|| message.Contains("rate")
				|| message.Contains("how much"))
			{
				// Add a follow up task within CRM
				returnMessage = "Thank you for inquiring about Atria Stony Brook. Rental rates can be found at: https://www.atriaseniorliving.com/retirement-communities/atria-stony-brook-louisville-ky/#iframePricing";
			}
			else if (message.Contains("schedule") || message.Contains("tour"))
			{
				// Add a schedule virtual tour task within CRM
				returnMessage = "Someone will be contacting you very soon to schedule a virtual tour.";
			}
			else
			{
				// Add a follow up task within CRM
				returnMessage = "Thank you for inquiring about Atria Stony Brook.  Someone will be getting back to you really soon.";
			}

			SendSMS(incomingMessage.To, incomingMessage.From, returnMessage, true);

			SMSMessageTableEntity.Save(incomingMessage.ToSMSMessageTableEntity(), _azureStorageSettings, _messageArchiveTable);

		}

	}

}
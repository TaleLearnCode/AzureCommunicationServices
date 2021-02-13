using Azure;
using Azure.Communication;
using Azure.Communication.Sms;
using System;

namespace TaleLearnCode.CommunicationServices
{
	public class SMSService
	{

		private readonly string _fromPhoneNumber;
		private readonly SmsClient _smsClient;
		private readonly AzureStorageSettings _azureStorageSettings;

		public SMSService(string serviceConnectionString, AzureStorageSettings azureStorageSettings, string fromPhoneNumber)
		{

			if (string.IsNullOrWhiteSpace(serviceConnectionString)) throw new ArgumentNullException(nameof(serviceConnectionString));

			_fromPhoneNumber = fromPhoneNumber;
			_smsClient = new SmsClient(serviceConnectionString);
			_azureStorageSettings = azureStorageSettings;
		}

		public SMSService(AzureStorageSettings azureStorageSettings)
		{
			_azureStorageSettings = azureStorageSettings;
		}

		public string SendSMS(string toPhoneNumber, string message, bool enableDeliveryReport = true)
		{

			if (_smsClient is null) throw new Exception("Need to initialize the SMS CLient first.");
			if (string.IsNullOrWhiteSpace(_fromPhoneNumber)) throw new Exception("Need to initialize the from phone number first.");

			Response<SendSmsResponse> response = _smsClient.Send(
				from: new PhoneNumber(_fromPhoneNumber),
				to: new PhoneNumber(toPhoneNumber),
				message: message,
				new SendSmsOptions { EnableDeliveryReport = enableDeliveryReport });

			SMSMessage.Save(new SMSMessage(_fromPhoneNumber, toPhoneNumber, message, response.Value.MessageId), _azureStorageSettings);

			return response.Value.MessageId;

		}

		public void AddDeliveryConfirmation(string toPhoneNumber, string messageId, string deliveryStatus, string deliveryStatusDetail, string receivedTimestamp)
		{
			SMSMessage smsMessage = SMSMessage.Retrieve(toPhoneNumber, messageId, _azureStorageSettings);
			smsMessage.DeliveryStatus = deliveryStatus;
			smsMessage.DeliveryStatusDetail = deliveryStatusDetail;
			smsMessage.ReceivedTimestamp = receivedTimestamp;
			SMSMessage.Save(smsMessage, _azureStorageSettings);
		}

		public SMSMessage RetrieveSentMessage(string toPhoneNumber, string messageId)
		{
			return SMSMessage.Retrieve(toPhoneNumber, messageId, _azureStorageSettings);
		}


	}

}
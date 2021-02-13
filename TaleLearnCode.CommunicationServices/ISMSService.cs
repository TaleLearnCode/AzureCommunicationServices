namespace TaleLearnCode.CommunicationServices
{
	public interface ISMSService
	{
		void AddDeliveryConfirmation(string toPhoneNumber, string messageId, string deliveryStatus, string deliveryStatusDetail, string receivedTimestamp);
		SMSMessage RetrieveSentMessage(string toPhoneNumber, string messageId);
		string SendSMS(string toPhoneNumber, string message, bool enableDeliveryReport = true);
	}
}
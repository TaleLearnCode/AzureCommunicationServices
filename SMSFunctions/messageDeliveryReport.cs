namespace SMSFunctions
{

	public class messageDeliveryReport
	{
		public string messageId { get; set; }
		public string from { get; set; }
		public string to { get; set; }
		public string receivedTimestamp { get; set; }
		public string deliveryStatus { get; set; }
		public string deliveryStatusDetails { get; set; }
	}

}
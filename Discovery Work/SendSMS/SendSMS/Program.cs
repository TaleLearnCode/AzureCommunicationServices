using Azure.Communication;
using Azure.Communication.Sms;
using System;

namespace SendSMS
{
	class Program
	{
		static void Main(string[] args)
		{
			SmsClient smsClient = new SmsClient(Settings.ACSConnectionString);
			smsClient.Send(
				from: new PhoneNumber(Settings.ACSPhoneNumber),
				to: new PhoneNumber(Settings.ConsumerPhoneNumber),
				//message: "Hello World via SMS",
				message: "Azure Functions for the win!!!",
				new SendSmsOptions { EnableDeliveryReport = true } // optional
			);

			Console.WriteLine("Message sent");

		}
	}
}

using System;
using TaleLearnCode.CommunicationServices;

namespace ProofOfConcept
{
	class Program
	{
		static void Main()
		{
			SendSMS();
		}



		private static void SendSMS()
		{
			Console.Clear();
			Console.WriteLine("Send SMS Demo");
			Console.WriteLine("-------------");
			Console.WriteLine();
			Console.WriteLine("Phone Number (be sure to include +1):");
			string phoneNumber = Console.ReadLine();
			Console.WriteLine();
			Console.WriteLine("Message:");
			string message = Console.ReadLine();

			SMSService smsService = new SMSService(Settings.ACSConnectionString, Settings.AzureStorageSettings, Settings.ACSPhoneNumber);
			string messageId = smsService.SendSMS(phoneNumber, message, true);
			Console.WriteLine();
			Console.WriteLine("Message sent");
			Console.WriteLine($"Message ID: {messageId}");

			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();

		}


	}
}
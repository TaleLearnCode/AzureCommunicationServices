using System;
using TaleLearnCode.CommunicationServices;

namespace ProofOfConcept
{

	public class Program
	{

		static void Main()
		{
			do
			{
				SendSMS();
			} while (true);
		}


		private static void SendSMS()
		{

			Console.Clear();
			PrintHeader();

			Console.WriteLine("Phone Number (be sure to include +1):");
			string phoneNumber = Console.ReadLine();
			Console.WriteLine();
			Console.WriteLine("Message:");
			string message = Console.ReadLine();

			SMSService smsService = new SMSService(Settings.ACSConnectionString, Settings.AzureStorageSettings, Settings.MessageArchiveTable, Settings.ACSPhoneNumber);
			string messageId = smsService.SendSMS(phoneNumber, message, true);

			Console.WriteLine();
			Console.WriteLine("Message sent");
			Console.WriteLine($"Message ID: {messageId}");

			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();

		}

		private static void PrintHeader()
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(@"  _________                  .___   _________   _____    _________ ________                         ");
			Console.WriteLine(@" /   _____/ ____   ____    __| _/  /   _____/  /     \  /   _____/ \______ \   ____   _____   ____  ");
			Console.WriteLine(@" \_____  \_/ __ \ /    \  / __ |   \_____  \  /  \ /  \ \_____  \   |    |  \_/ __ \ /     \ /  _ \ ");
			Console.WriteLine(@" /        \  ___/|   |  \/ /_/ |   /        \/    Y    \/        \  |    `   \  ___/|  Y Y  (  <_> )");
			Console.WriteLine(@"/_______  /\___  >___|  /\____ |  /_______  /\____|__  /_______  / /_______  /\___  >__|_|  /\____/ ");
			Console.WriteLine(@"        \/     \/     \/      \/          \/         \/        \/          \/     \/      \/        ");
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine();
			Console.WriteLine();
		}


	}
}
﻿using TaleLearnCode.CommunicationServices.Models;

namespace ProofOfConcept
{
	public static class Settings
	{

		public static string ACSConnectionString = "endpoint=https://tlcprototype.communication.azure.com/;accesskey=12CAMbPQ1RRAzdaNTM882Z8jS43sc4PjyTOvd5DkkiKx2S6Zal+orsRkOKND+H10DeqwNB2wZS3wVoNQUUKeqQ==";
		public static string ACSPhoneNumber = "+18332321898";
		public static AzureStorageSettings AzureStorageSettings = new AzureStorageSettings()
		{
			AccountKey = "jpIVEL8Tu5ioQYrMTqGUkIq6xJBks/A1uFarbbL9fRkpQm0ah9+wW1whACgsZFlYffhSzByBqavbu6OaawhGsQ==",
			AccountName = "stazurecommservices",
			Url = "https://stazurecommservices.table.core.windows.net"
		};
		public static string MessageArchiveTable = "Messages";
	}
}
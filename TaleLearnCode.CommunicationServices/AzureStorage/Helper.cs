﻿using Azure.Data.Tables;
using System;
using TaleLearnCode.CommunicationServices.Models;

namespace TaleLearnCode.CommunicationServices.AzureStorage
{

	/// <summary>
	/// Provides helper methods for working with Azure Storage.
	/// </summary>
	public static class Helper
	{

		/// <summary>
		/// Gets the <see cref="TableClient"/> to use for connecting to an Azure Storage Table container.
		/// </summary>
		/// <param name="azureStorageSettings">The <see cref="AzureStorageSettings"/> containing the settings for connecting to the Azure Storage account.</param>
		/// <param name="tableName">The name of the table which the returned client instance will interact.</param>
		/// <returns>A <see cref="TableClient"/> to use for interacting with an Azure Storage Table container.</returns>
		public static TableClient GetTableClient(AzureStorageSettings azureStorageSettings, string tableName)
		{
			return new TableClient(
				new Uri(azureStorageSettings.Url),
				tableName,
				new TableSharedKeyCredential(azureStorageSettings.AccountName, azureStorageSettings.AccountKey));
		}

	}

}

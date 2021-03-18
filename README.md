# Azure Communication Services

The Settings.cs class will need to look simliar to this:

~~~
	public static class Settings
	{

		public static string ACSConnectionString = "<ACS Connection String>";
		public static string ACSPhoneNumber = "<ACS Phone Number>";
		public static AzureStorageSettings AzureStorageSettings = new AzureStorageSettings()
		{
			AccountKey = "<Storage Account Key>",
			AccountName = "<Storage Account Name>",
			Url = "https://<Storage Account Name>.table.core.windows.net"
		};
		public static string MessageArchiveTable = "Messages";
	}
  ~~~

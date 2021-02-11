using System;
using System.Collections.Generic;
using System.Text;

namespace TestFunction
{

	public class SMSReceived
	{
		public string messageId { get; set; }
		public string from { get; set; }
		public string message { get; set; }
		public string receivedTimestamp { get; set; }
	}

}
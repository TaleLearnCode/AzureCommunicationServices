namespace TaleLearnCode.CommunicationServices.Models
{

	/// <summary>
	/// Represents the different incoming SMS event types.
	/// </summary>
	public enum IncomingSMSEventType
	{
		/// <summary>
		/// Unknown SMS event type (used when none of the other conditions are met)
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// The incoming message matched one requesting pricing information
		/// </summary>
		PriceInquiry = 1,
		/// <summary>
		/// The incoming message matched one requesting to schedule a tour of the community
		/// </summary>
		ScheduleTour = 2
	}

}
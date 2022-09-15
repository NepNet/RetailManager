using System;

namespace RetailManager.Data
{
	public class RegisterEntry
	{
		//TODO MAKE THEM PRIVATE SETTERS OR INIT
		public int Id { get; set; }
		public User RecordedBy { get; set; }
		public DateTime Time { get; set; }
		public double Value { get; set; }
		public string Reason { get; set; }
	}
}
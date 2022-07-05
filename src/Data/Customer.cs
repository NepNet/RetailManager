using Gtk;

namespace RetailManager.Data
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string CompanyNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string County { get; set; } = string.Empty;
		
	}
}
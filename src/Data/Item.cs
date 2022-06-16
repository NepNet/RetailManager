namespace RetailManager.Data
{
	public class Item
	{
		public int Id { get; }
		public string Name { get; }
		public decimal UnitPrice { get; }
		public byte VatGroup { get; }
		public decimal Vat { get; }
	}
}
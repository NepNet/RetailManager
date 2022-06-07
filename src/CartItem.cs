namespace RetailManager
{
	public class CartItem
	{
		public string Name { get; set; }
		public decimal Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Discount { get; set; }
		public decimal DiscountedPrice => decimal.Round(UnitPrice - UnitPrice * Discount, 2);
		public decimal VAT { get; set; } = 0.19m;
		public decimal VATValue => decimal.Round(DiscountedPrice * VAT, 2);

		public decimal TotalPrice => decimal.Round(DiscountedPrice * Quantity, 2);
		public decimal TotalVAT => decimal.Round(VATValue * Quantity, 2);
		public decimal TotalBase => (DiscountedPrice - VATValue) * Quantity;
	}
}
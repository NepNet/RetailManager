namespace RetailManager
{
	public class CartItem
	{
		public string Name { get; set; }
		public int Quantity { get; set; }
		public float UnitPrice { get; set; }
		public float Discount { get; set; }
		public float DiscountedPrice => UnitPrice - UnitPrice * Discount;
		public float VAT { get; set; } = 0.19f;
		public float VATValue => DiscountedPrice * VAT;
		
		public float TotalPrice => DiscountedPrice * Quantity;
		public float TotalVAT => VATValue * Quantity;
		public float TotalBase => (DiscountedPrice - VATValue) * Quantity;
	}
}
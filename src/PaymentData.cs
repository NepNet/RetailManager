using RetailManager.Data;

namespace RetailManager
{
	public struct PaymentData
	{
		public PaymentMethod Method { get;}
		public ReceiptType ReceiptType { get;}
		public Cart Cart { get; }
		public ClientInfo Client { get; private set; }
		public int ReceiptId { get; set; }

		public PaymentData(PaymentMethod method, ReceiptType type, Cart cart)
		{
			Method = method;
			ReceiptType = type;
			Cart = cart;
			Client = null;
			ReceiptId = 0;
		}
		
		public void SetClient(ClientInfo info)
		{
			Client = info;
		}
	}
}
using System.Threading.Tasks;

namespace RetailManager
{
	public static class PaymentHandler
	{
		public static async Task<int> Process(PaymentMethod method, ReceiptType receiptType)
		{
			await Task.Delay(3000);
			return 1;
		}
	}
}
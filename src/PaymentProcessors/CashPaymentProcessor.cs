using System;
using System.Threading.Tasks;

namespace RetailManager.PaymentProcessors
{
	public class CashPaymentProcessor : IPaymentProcessor
	{
		private int receiptCount;
		public async Task<int> Process(PaymentData data)
		{
			await Task.Delay(500);
			
			data.ReceiptId = ++receiptCount;
			Console.WriteLine(data.ReceiptId);
			
			return 0;
		}
	}
}
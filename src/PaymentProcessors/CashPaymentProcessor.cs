using System.Threading.Tasks;

namespace RetailManager.PaymentProcessors
{
	public class CashPaymentProcessor : IPaymentProcessor
	{
		public async Task<int> Process(PaymentData data)
		{
			return data.ReceiptType switch
			{
				ReceiptType.Basic_receipt => 0,
				ReceiptType.Receipt_with_company_code => 2,
				ReceiptType.Invoice => 5,
				_ => 222
			};
		}
	}
}
using System.Threading.Tasks;

namespace RetailManager.PaymentProcessors
{
	public class PaymentOrderPaymentHandler : IPaymentProcessor
	{
		public async Task<int> Process(PaymentData data)
		{
			return data.ReceiptType == ReceiptType.Invoice ? 0 : 22;
		}
	}
}
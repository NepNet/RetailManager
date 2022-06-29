using System.Threading.Tasks;

namespace RetailManager.ReceiptProcessors
{
	public class InvoiceProcessor : IReceiptProcessor
	{
		public async Task<int> Preprocess(PaymentData data)
		{
			return 5;
		}
	}
}
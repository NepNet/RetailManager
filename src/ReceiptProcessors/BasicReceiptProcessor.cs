using System.Threading.Tasks;

namespace RetailManager.ReceiptProcessors
{
	public class BasicReceiptProcessor : IReceiptProcessor
	{
		public async Task<int> Preprocess(PaymentData data)
		{
			return 0;
		}
	}
}
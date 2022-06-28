using System.Threading.Tasks;

namespace RetailManager.ReceiptProcessors
{
	public class BasicReceiptProcessor : IReceiptProcessor
	{
		public async Task<int> Preprocess()
		{
			return 0;
		}
	}
}
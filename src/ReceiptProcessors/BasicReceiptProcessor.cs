using System.Threading.Tasks;

namespace RetailManager.ReceiptProcessors
{
	public class BasicReceiptProcessor : IReceiptProcessor
	{
		public Task Preprocess()
		{
			return Task.CompletedTask;
		}
	}
}
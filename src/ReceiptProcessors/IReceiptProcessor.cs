using System.Threading.Tasks;

namespace RetailManager.ReceiptProcessors
{
	public interface IReceiptProcessor
	{
		public Task<int> Preprocess();
	}
}
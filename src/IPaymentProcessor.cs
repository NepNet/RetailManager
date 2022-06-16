using System.Threading.Tasks;

namespace RetailManager
{
	public interface IPaymentProcessor
	{
		public Task<int> Process();
	}
}
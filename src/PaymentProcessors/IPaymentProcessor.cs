using System.Threading.Tasks;

namespace RetailManager.PaymentProcessors
{
	public interface IPaymentProcessor
	{
		public Task<int> Process(PaymentData data);
	}
}
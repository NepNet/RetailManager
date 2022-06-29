using System.Threading.Tasks;

namespace RetailManager.PaymentProcessors
{
	public class CardPaymentProcessor : IPaymentProcessor
	{
		public async Task<int> Process(PaymentData data)
		{
			await Task.Delay(5000);
			return 0;
		}
	}
}
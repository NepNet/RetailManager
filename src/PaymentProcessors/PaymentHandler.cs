using System.Collections.Generic;
using System.Threading.Tasks;
using RetailManager.ReceiptProcessors;

namespace RetailManager.PaymentProcessors
{
	public static class PaymentHandler
	{
		private static Dictionary<PaymentMethod, IPaymentProcessor> methods = new Dictionary<PaymentMethod, IPaymentProcessor>();
		public static void Register<T>(PaymentMethod method) where T : IPaymentProcessor, new()
		{
			methods.Add(method, new T());
		}
		
		public static async Task<int> Process(PaymentData data)
		{
			await Task.Delay(200);
			await ReceiptHandler.Preprocess(data.ReceiptType);
			return await methods[data.Method].Process(data);
		}
	}
}
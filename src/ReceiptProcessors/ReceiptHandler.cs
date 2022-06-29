using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailManager.ReceiptProcessors
{
	public static class ReceiptHandler
	{
		private static Dictionary<ReceiptType, IReceiptProcessor> methods = new Dictionary<ReceiptType, IReceiptProcessor>();
		public static void Register<T>(ReceiptType type) where T : IReceiptProcessor, new()
		{
			methods.Add(type, new T());
		}

		public static async Task<int> Preprocess(PaymentData data)
		{
			return await methods[data.ReceiptType].Preprocess(data);
		}
	}
}
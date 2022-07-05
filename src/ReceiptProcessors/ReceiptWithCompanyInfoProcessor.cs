using System;
using System.Threading.Tasks;
using Gtk;
using RetailManager.GUI;

namespace RetailManager.ReceiptProcessors
{
	public class ReceiptWithCompanyInfoProcessor : IReceiptProcessor
	{
		public async Task<int> Preprocess(PaymentData data)
		{
			var tcs = new TaskCompletionSource<int>();
			
			var window = new ClientSelectionDialog();

			window.ShowAll();
			window.KeepAbove = true;
			
			try
			{
				var customer = await window.WaitClientSelectionAsync();
				tcs.SetResult(0);
			}
			catch (TaskCanceledException e)
			{
				tcs.SetResult(1);
			}
			finally
			{
				window.Dispose();
			}
			
			return await tcs.Task;
		}
	}
}
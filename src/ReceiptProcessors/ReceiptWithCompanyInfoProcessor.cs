using System;
using System.Threading.Tasks;
using Gtk;
using RetailManager.GUI;

namespace RetailManager.ReceiptProcessors
{
	public class ReceiptWithCompanyInfoProcessor : IReceiptProcessor
	{
		public async Task<int> Preprocess()
		{
			var tcs = new TaskCompletionSource<int>();
			
			var window = new ClientSelectionDialog();

			window.Response += (o, args) =>
			{
				if (args.ResponseId == ResponseType.Accept)
				{
					tcs.SetResult(0);
				}
				else
				{
					tcs.SetResult(1);
				}
				
				window.Dispose();
			};

			window.ShowAll();

			window.KeepAbove = true;
			
			return await tcs.Task;
		}
	}
}
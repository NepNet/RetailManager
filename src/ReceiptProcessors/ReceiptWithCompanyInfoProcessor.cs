using System.Threading.Tasks;
using Gtk;

namespace RetailManager.ReceiptProcessors
{
	public class ReceiptWithCompanyInfoProcessor : IReceiptProcessor
	{
		public Task Preprocess()
		{
			var window = new Window("Company code");
			
			var entry = new Entry("CODE");
			
			window.Add(entry);
			
			window.ShowAll();
			
			return Task.CompletedTask;
		}
	}
}
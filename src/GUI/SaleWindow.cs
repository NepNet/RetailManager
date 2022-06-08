using GLib;
using Gtk;

namespace RetailManager.GUI
{
	[TypeName(nameof(SaleWindow))]
	[Template(GUIConstants.RES + nameof(SaleWindow))]
	public class SaleWindow : Window
	{
		public SaleWindow() : base("sale")
		{
			
		}
	}
}
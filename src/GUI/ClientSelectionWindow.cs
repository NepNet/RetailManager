using GLib;
using Gtk;

namespace RetailManager.GUI
{
	[TypeName(nameof(ClientSelectionWindow))]
	[Template(GUIConstants.RES + nameof(ClientSelectionWindow))]
	public class ClientSelectionWindow : Window
	{
		public ClientSelectionWindow() : base(WindowType.Toplevel)
		{
		}
	}
}
using System;
using GLib;
using Gtk;

namespace RetailManager.GUI
{
	[TypeName(nameof(MainWindow))]
	[Template(GUIConstants.RES + nameof(MainWindow))]
	public class MainWindow : Window
	{
		[Child] private Notebook _main;
		public MainWindow(string title) : base(title)
		{
			
		}
	}
}

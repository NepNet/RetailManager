using System;
using System.Threading;
using GLib;
using RetailManager.GUI;
using Application = Gtk.Application;

namespace RetailManager
{
	static class Program
	{
		private const string APPID = "com.nepnet.retailmanager";
		private static Application _app;
		private static bool _initialized;
		private static GLibSynchronizationContext _synchronizationContext;

		static void Main(string[] args)
		{
			_app = new Application(APPID, ApplicationFlags.None);
			
			_synchronizationContext = new GLibSynchronizationContext();
			SynchronizationContext.SetSynchronizationContext(_synchronizationContext);
			
			_app.Startup += AppOnStartup;
			_app.Activated += OnAppStarted;
			
			Environment.Exit(((GLib.Application) _app).Run());
		}

		private static void AppOnStartup(object? sender, EventArgs e)
		{
			
		}

		private static void OnAppStarted(object? sender, EventArgs e)
		{
			if (!_initialized)
			{
				var window = new ItemSaleWindow();
				//var window = new ClientSelectionDialog();
				_app.AddWindow(window);
			
				window.Show();
				_initialized = true;
			}
		}
	}
}

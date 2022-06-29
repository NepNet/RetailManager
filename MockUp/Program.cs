using System;
using System.Threading;
using GLib;
using Application = Gtk.Application;

namespace mockup
{

	static class Program
	{
		private const string APPID = "com.nepnet.retail.mockup";
		private static Application _app;

		private static GLibSynchronizationContext _synchronizationContext;

		static void Main(string[] args)
		{
			_app = new Application(APPID, ApplicationFlags.None);

			_synchronizationContext = new GLibSynchronizationContext();
			SynchronizationContext.SetSynchronizationContext(_synchronizationContext);

			_app.Activated += OnAppStarted;

			Environment.Exit(((GLib.Application) _app).Run());
		}

		private static void OnAppStarted(object? sender, EventArgs e)
		{
			var window = new CashRegisterWindow();

			_app.AddWindow(window);

			window.Show();
		}
	}
}
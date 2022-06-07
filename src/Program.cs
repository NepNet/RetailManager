using System;
using System.Reflection;
using GLib;
using RetailManager.GUI;
using Application = Gtk.Application;

namespace RetailManager
{
	static class Program
	{
		private const string APPID = "com.nepnet.retailmanager";
		private static Application _app;

		static void Main(string[] args)
		{
			_app = new Application(APPID, ApplicationFlags.None);
			_app.Activated += OnAppStarted;

			Environment.Exit(((GLib.Application) _app).Run());
		}

		private static void OnAppStarted(object? sender, EventArgs e)
		{
			var window = new ItemSaleWindow();
			_app.AddWindow(window);
			
			window.Show();
		}
	}
}

using System;
using System.Threading;
using GLib;
using RetailManager.Data;
using RetailManager.GUI;
using RetailManager.PaymentProcessors;
using RetailManager.ReceiptProcessors;
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
			PaymentHandler.Register<PaymentOrderPaymentHandler>(PaymentMethod.Payment_Order);
			PaymentHandler.Register<CashPaymentProcessor>(PaymentMethod.Cash);
			PaymentHandler.Register<CardPaymentProcessor>(PaymentMethod.Card);
			
			ReceiptHandler.Register<BasicReceiptProcessor>(ReceiptType.Basic_receipt);
			ReceiptHandler.Register<ReceiptWithCompanyInfoProcessor>(ReceiptType.Receipt_with_company_code);
			ReceiptHandler.Register<InvoiceProcessor>(ReceiptType.Invoice);
		}

		private static void OnAppStarted(object? sender, EventArgs e)
		{
			if (!_initialized)
			{
				var window = new MainWindow("a");
					
				_app.AddWindow(window);
			
				window.Show();
				
				/*
				var data = new SqLiteDataAccess();
				if (data.LoginUser("test", "what", out User user))
				{
					var window = new ItemSaleWindow(user);
					
					_app.AddWindow(window);
			
					window.Show();
					
					new ClientInfoWindow(new Customer(){Name = "tester"}).Show();
				}*/
				
				_initialized = true;
			}
		}
	}
}

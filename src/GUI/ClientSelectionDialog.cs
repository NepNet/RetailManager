using System;
using GLib;
using Gtk;
using RetailManager.Data;
using Action = System.Action;

namespace RetailManager.GUI
{
	[TypeName(nameof(ClientSelectionDialog))]
	[Template(GUIConstants.RES + nameof(ClientSelectionDialog))]
	public class ClientSelectionDialog : Dialog
	{
		[Child] private Button _confirmButton;
		[Child] private Button _cancelButton;
		[Child] private Button _addButton;

		[Child] private Entry _clientSearchEntry;
		[Child] private TreeView _clientsTreeView;

		private ListStore _clientsList;
		
		public ClientSelectionDialog()
		{
			_confirmButton.Clicked += OnConfirm;
			_cancelButton.Clicked += OnCancel;
			
			_addButton.Clicked += AddButtonOnClicked;
			
			_clientsList = new ListStore(typeof(Customer));
			_clientsTreeView.Model = _clientsList;
			
			//Fill with dummy data
			_clientsList.AppendValues(
				new Customer()
				{
					Name = "Elmet",
					CompanyNumber = "22"
				});
			_clientsList.AppendValues(
				new Customer()
				{
					Name = "Elmat",
					CompanyNumber = "22"
				});
			_clientsList.AppendValues(
				new Customer()
				{
					Name = "Demo tester",
					CompanyNumber = "1587"
				});
			
			var nameCell = new CellRendererText();
			var nameColumn = _clientsTreeView.AppendColumn("Name", nameCell, NameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 200;
			nameColumn.MaxWidth = 700;
		}

		private async void AddButtonOnClicked(object? sender, EventArgs e)
		{
			Hide();
			
			try
			{
				var customer = await ClientInfoWindow.CreateRegistrationWindow();
				Console.WriteLine(customer.Name);
				Show();
			}
			catch (Exception exception)
			{
				
			}
			Show();
		}

		private void NameCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (Customer)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Name));
		}

		private void OnCancel(object sender, EventArgs e)
		{
			Respond(ResponseType.Cancel);
			Dispose();
		}

		private void OnConfirm(object sender, EventArgs e)
		{
			_clientsTreeView.Selection.GetSelected(out var iter);
			var client = _clientsList.GetValue(iter, 0) as Customer;
			if (client is null)
			{
				return;
			}
			ClientSelected?.Invoke(client);
			
			Respond(ResponseType.Accept);
			Dispose();
		}

		public event Action<Customer> ClientSelected;
	}
}
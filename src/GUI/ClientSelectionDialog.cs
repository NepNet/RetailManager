using System;
using System.Threading.Tasks;
using GLib;
using Gtk;
using RetailManager.Data;

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
			_clientSearchEntry.Changed += (sender, args) =>
			{
				_clientsList.Clear();
				if (_clientSearchEntry.Text.Length > 2)
				{
					var data = new SqLiteDataAccess();
					var customers = data.FindCustomers(_clientSearchEntry.Text);
					foreach (var customer in customers)
					{
						_clientsList.AppendValues(customer);
					}
				}
			};
			
			var nameCell = new CellRendererText();
			var nameColumn = _clientsTreeView.AppendColumn("Name", nameCell, NameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 200;
			nameColumn.MaxWidth = 700;
			
			var companyCodeCell = new CellRendererText();
			var companyCodeColumn = _clientsTreeView.AppendColumn("Company code", companyCodeCell, NameCellFunc);
			companyCodeColumn.MinWidth = 100;
		}

		public async Task<Customer> WaitClientSelectionAsync()
		{
			var tcs = new TaskCompletionSource<Customer>();

			var completed = false;
			ClientSelected += customer =>
			{
				tcs.SetResult(customer);
				completed = true;
				Dispose();
			};
			DestroyEvent += (o, args) =>
			{
				if (!completed)
				{
					tcs.SetCanceled();
				}
			};
			
			return await tcs.Task;
		}

		private async void AddButtonOnClicked(object? sender, EventArgs e)
		{
			Hide();
			
			try
			{
				var customer = await ClientInfoWindow.CreateRegistrationWindow();
				ClientSelected?.Invoke(customer);
			
				Respond(ResponseType.Accept);
				Dispose();
			}
			catch (TaskCanceledException canceledException)
			{
				Show();
			}
		}

		private void NameCellFunc(TreeViewColumn column, CellRenderer cell, ITreeModel model, TreeIter iter)
		{
			var item = (Customer)model.GetValue(iter, 0);
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
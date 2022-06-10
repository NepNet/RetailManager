using System;
using GLib;
using Gtk;
using Action = System.Action;

namespace RetailManager.GUI
{
	[TypeName(nameof(ClientSelectionDialog))]
	[Template(GUIConstants.RES + nameof(ClientSelectionDialog))]
	public class ClientSelectionDialog : Dialog
	{
		[Child] private Button _confirmButton;
		[Child] private Button _cancelButton;

		[Child] private Entry _clientSearchEntry;
		[Child] private TreeView _clientsTreeView;

		private ListStore _clientsList;
		
		public ClientSelectionDialog()
		{
			_confirmButton.Clicked += OnConfirm;
			_confirmButton.Activated += OnConfirm;
			
			_cancelButton.Clicked += OnCancel;
			_cancelButton.Activated += OnCancel;
			
			_clientsList = new ListStore(typeof(ClientInfo));
			_clientsTreeView.Model = _clientsList;
			
			//Fill with dummy data
			_clientsList.AppendValues(
				new ClientInfo()
				{
					Name = "Elmet",
					CompanyNumber = "22"
				});
			_clientsList.AppendValues(
				new ClientInfo()
				{
					Name = "Elmat",
					CompanyNumber = "22"
				});
			_clientsList.AppendValues(
				new ClientInfo()
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

		private void NameCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (ClientInfo)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Name));
		}

		private void OnCancel(object sender, EventArgs e)
		{
			Dispose();
		}

		private void OnConfirm(object sender, EventArgs e)
		{
			_clientsTreeView.Selection.GetSelected(out var iter);
			var client = _clientsList.GetValue(iter, 0) as ClientInfo;
			ClientSelected?.Invoke(client);
			Dispose();
		}

		public event Action<ClientInfo> ClientSelected;
	}
}
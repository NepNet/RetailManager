using System;
using GLib;
using Gtk;
using RetailManager.Data;

namespace RetailManager.GUI
{
	[TypeName(nameof(ClientInfoWindow))]
	[Template(GUIConstants.RES + nameof(ClientInfoWindow))]
	public class ClientInfoWindow : Window
	{
		[Child] private Entry _nameEntry;
		[Child] private Entry _companyCodeEntry;
		[Child] private Entry _addressEntry;
		[Child] private Entry _cityEntry;
		[Child] private Entry _countyEntry;
		[Child] private Entry _phoneEntry;
		[Child] private Entry _emailEntry;
		
		[Child] private Button _okButton;
		[Child] private Button _applyButton;
		[Child] private Button _editButton;
		[Child] private Button _cancelButton;

		private bool _editing;

		private Entry[] _entries;
		private ClientInfo _clientInfo;
	
		public ClientInfoWindow(ClientInfo client) : base(client.Name)
		{
			_clientInfo = client;
			_clientInfo.Name = "aaaaa";
			
			_entries = new Entry[]
			{
				_nameEntry,
				_companyCodeEntry,
				_addressEntry,
				_cityEntry,
				_countyEntry,
				_phoneEntry,
				_emailEntry
			};

			_editing = true;
			OnEditToggle(this, EventArgs.Empty);
			
			_editButton.Clicked += OnEditToggle;
			_cancelButton.Clicked += OnEditToggle;

			_applyButton.Clicked += OnApply;
			_okButton.Clicked += (sender, args) =>
			{
				OnApply(sender, args);
				Dispose();
			};
		}

		private void LoadClient()
		{
			_nameEntry.Text = _clientInfo.Name;
			_companyCodeEntry.Text = _clientInfo.CompanyNumber;
			_addressEntry.Text = _clientInfo.Address;
			_cityEntry.Text = _clientInfo.City;
			_countyEntry.Text = _clientInfo.County;
		}

		private void SaveClient()
		{
			_clientInfo.Name = _nameEntry.Text;
			_clientInfo.CompanyNumber = _companyCodeEntry.Text;
			_clientInfo.Address = _addressEntry.Text;
			_clientInfo.City = _cityEntry.Text;
			_clientInfo.County = _countyEntry.Text;
		}

		private void OnApply(object sender, EventArgs args)
		{
			SaveClient();
			OnEditToggle(this, EventArgs.Empty);
		}
		
		private void OnEditToggle(object sender, EventArgs args)
		{
			_editing = !_editing;
			foreach (var entry in _entries)
			{
				entry.IsEditable = _editing;
				entry.HasFrame = _editing;
			}

			LoadClient();
			
			_editButton.Visible = !_editing;
			_cancelButton.Visible = _editing;
			_applyButton.Visible = _editing;
			
		}
	}
}
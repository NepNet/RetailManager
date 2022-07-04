using System;
using System.Threading.Tasks;
using GLib;
using Gtk;
using RetailManager.Data;

namespace RetailManager.GUI
{
	[TypeName(nameof(ClientInfoWindow))]
	[Template(GUIConstants.RES + nameof(ClientInfoWindow))]
	public class ClientInfoWindow : Window
	{
		[Child] private Label _idLabel;
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
		[Child] private Button _historyButton;

		private bool _editing;

		private Entry[] _entries;
		private Customer _customer;

		public static async Task<Customer> CreateRegistrationWindow()
		{
			var tcs = new TaskCompletionSource<Customer>();
			
			var window = new ClientInfoWindow(out var client);
			var completed = false;
			window._okButton.Clicked += (sender, args) =>
			{
				if (completed) return;
				
				tcs.SetResult(client);
				completed = true;
				window.Dispose();
			};
			window.Destroyed += (sender, args) =>
			{
				if (!completed)
				{ 
					tcs.SetCanceled();
				}
			};
			
			window.Show();
			
			return await tcs.Task;
		}
		
		private ClientInfoWindow(out Customer customer) : base("Register new customer")
		{
			_customer = new Customer();
			customer = _customer;
			
			_editing = false;
			
			Init();

			_editButton.Visible = false;
			_cancelButton.Visible = false;
			_applyButton.Visible = false;
			_okButton.Visible = true;
			_historyButton.Visible = false;
		}

		public ClientInfoWindow(Customer customer) : base(customer.Name)
		{
			_customer = customer;
			
			_editing = true;
			
			Init();
			
			_editButton.Clicked += OnEditToggle;
			_cancelButton.Clicked += OnEditToggle;

			_applyButton.Clicked += OnApply;
			_okButton.Clicked += (sender, args) => Dispose();
		}

		private void Init()
		{
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

			OnEditToggle(this, EventArgs.Empty);

			_okButton.Clicked += OnApply;
		}

		private void LoadClient()
		{
			_idLabel.Text = _customer.Id.ToString();
			_nameEntry.Text = _customer.Name;
			_companyCodeEntry.Text = _customer.CompanyNumber;
			_addressEntry.Text = _customer.Address;
			_cityEntry.Text = _customer.City;
			_countyEntry.Text = _customer.County;
		}

		private void SaveClient()
		{
			_customer.Name = _nameEntry.Text;
			_customer.CompanyNumber = _companyCodeEntry.Text;
			_customer.Address = _addressEntry.Text;
			_customer.City = _cityEntry.Text;
			_customer.County = _countyEntry.Text;
		}

		private void OnApply(object sender, EventArgs args)
		{
			SaveClient();
			Title = _customer.Name;
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
			_okButton.Visible = !_editing;
			_historyButton.Sensitive = !_editing;
		}
	}
}
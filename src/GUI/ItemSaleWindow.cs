using System;
using GLib;
using Gtk;
using RetailManager.Data;
using RetailManager.PaymentProcessors;
using RetailManager.ReceiptProcessors;
using Task = System.Threading.Tasks.Task;

namespace RetailManager.GUI
{
	[TypeName(nameof(ItemSaleWindow))]
	[Template(GUIConstants.RES + nameof(ItemSaleWindow))]
	public class ItemSaleWindow : Window
	{
		[Child] private SearchEntry _searchEntry;
		[Child] private TreeView _searchTree;

		[Child] private TreeView _cartTree;
		[Child] private Button _confirmButton;
		[Child] private Button _clearButton;

		[Child] private Label _priceLabel;
		[Child] private Label _vatLabel;
		[Child] private Label _totalLabel;

		[Child] private Box _searchArea;
		[Child] private Box _paymentArea;
		[Child] private Box _cartArea;
		
		[Child] private Box _paymentMethodBox;
		[Child] private Box _receiptTypeBox;
		[Child] private Button _confirmSaleButton;

		private ListStore _searchListStore;
		private CartView _cartView;
		private TreeModelFilter _searchFilterModel;

		private PaymentMethod _paymentMethod;
		private ReceiptType _receiptType;
		
		public ItemSaleWindow(User user) : base(user.DisplayName)
		{
			Maximize();
			
			InitSearchTree();
			InitCartTree();
			
			InitSearch();

			_clearButton.Activated += OnClearCart;
			_clearButton.Clicked += OnClearCart;

			var selectingItems = true;
			
			_confirmButton.Clicked += (sender, args) =>
			{
				selectingItems = !selectingItems;
				//_confirmButton.Label = selectingItems ? "Ok" : "Edit";

				_searchArea.Visible = selectingItems;
				_paymentArea.Visible = !selectingItems;
				_cartTree.Sensitive = selectingItems;
				_clearButton.Visible = selectingItems;
			};
			
			_confirmSaleButton.Clicked += OnSaleConfirm;
			
			SetupPaymentTypeArea();
		}

		private async void OnSaleConfirm(object? sender, EventArgs e)
		{
			Sensitive = false;

			var data = new PaymentData(_paymentMethod, _receiptType, _cartView.Model);

			if (await ReceiptHandler.Preprocess(data) == 0)
			{
				var popup = new WaitingPopup("Waiting for response...");
				
				var result = await PaymentHandler.Process(data);

				if (result != 0)
				{
					Sensitive = true;
				}
				popup.Dispose();
			}

			Sensitive = true;
		}

		private void SetupPaymentTypeArea()
		{
			var methods = Utilities.CreateRadioFromEnum<PaymentMethod>
				(pay => _paymentMethod = pay);
			var receipts = Utilities.CreateRadioFromEnum<ReceiptType>
				(type => _receiptType = type);

			foreach (var radio in methods)
			{
				_paymentMethodBox.Add(radio);
			}
			
			foreach (var radio in receipts)
			{
				_receiptTypeBox.Add(radio);
			}
		}
		
		private void OnClearCart(object? sender, EventArgs e)
		{
			_cartView.Clear();
		}

		private void InitSearch()
		{
			_searchFilterModel = new TreeModelFilter(_searchListStore, null);
			
			_searchEntry.Activated += (sender, args) =>
			{
				_searchFilterModel.Refilter();
			};
			_searchEntry.SearchChanged += (sender, args) =>
			{
				_searchFilterModel.Refilter();
			};
			
			_searchFilterModel.VisibleFunc = (model, iter) =>
			{
				var item = (Item) model.GetValue(iter, 0);
				if (item.Name.Contains(_searchEntry.Text, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
				return false;
			};
			
			_searchTree.Model = _searchFilterModel;
		}
		
		private void InitSearchTree()
		{
			var model = new ListStore(typeof(Item));
			_searchListStore = model;
			_searchTree.Model = model;
			
			var data = new SqLiteDataAccess();
			
			foreach (var item in data.GetAllItems())
			{
				model.AppendValues(item);
			}
			
			var nameCell = new CellRendererText();
			var nameColumn = _searchTree.AppendColumn("Name", nameCell, NameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 100;
			nameColumn.MaxWidth = 700;

			_searchTree.RowActivated += (o, args) =>
			{
				_searchFilterModel.GetIter(out var iter, args.Path);
				var source = (Item) _searchFilterModel.GetValue(iter, 0);
				var item = new CartItem(source)
				{
					Quantity = 1,
				};
				_cartView.Add(item);
			};
		}
		
		private void InitCartTree()
		{
			_cartView = new CartView(_cartTree, _priceLabel, _vatLabel, _totalLabel, true);
		}
		
		private void NameCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (Item)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Name));
		}
	}
}
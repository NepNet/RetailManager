using System;
using GLib;
using Gtk;

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
		
		private ListStore _searchListStore;
		private CartView _cart;
		private TreeModelFilter _searchFilterModel;

		public ItemSaleWindow() : base(WindowType.Toplevel)
		{
			InitSearchTree();
			InitCartTree();

			InitSearch();

			_clearButton.Activated += ClearCart;
			_clearButton.Clicked += ClearCart;

			var selectingItems = true;
			
			_confirmButton.Clicked += (sender, args) =>
			{
				//new ClientSelectionWindow().Show();
				//new SaleWindow().Show();
				selectingItems = !selectingItems;

				_searchArea.Visible = selectingItems;
				_paymentArea.Visible = !selectingItems;
				_cartTree.Sensitive = selectingItems;
				_clearButton.Visible = selectingItems;
			};
			
			string[] paymentMethods = new string[]
			{
				"Cash",
				"Card",
				"Payment Order",
			};
			
			RadioButton radioButton = new RadioButton(paymentMethods[0]);
			
			foreach (var paymentMethod in paymentMethods)
			{
				RadioButton radio = new RadioButton(radioButton, paymentMethod);
				_paymentMethodBox.Add(radio);
				radio.Show();
			}
		}

		private void ClearCart(object? sender, EventArgs e)
		{
			_cart.Clear();
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
				var item = (CartItem) model.GetValue(iter, 0);
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
			var model = new ListStore(typeof(CartItem));
			_searchListStore = model;
			_searchTree.Model = model;

			
			for (int i = 0; i < 50; i++)
			{
				model.AppendValues(new CartItem()
				{
					Name = $"Item {i}",
					UnitPrice = i
				});
			}
			
			var nameCell = new CellRendererText();
			var nameColumn = _searchTree.AppendColumn("Name", nameCell, NameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 100;
			nameColumn.MaxWidth = 700;

			_searchTree.RowActivated += (o, args) =>
			{
				_searchFilterModel.GetIter(out var iter, args.Path);
				var source = (CartItem) _searchFilterModel.GetValue(iter, 0);
				var item = new CartItem()
				{
					Name = source.Name,
					Quantity = 1,
					UnitPrice = source.UnitPrice
				};
				_cart.Add(item);
			};
		}
		
		private void InitCartTree()
		{
			_cart = new CartView(_cartTree, _priceLabel, _vatLabel, _totalLabel, true);
		}
		
		private void NameCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (CartItem)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Name));
		}
	}
}
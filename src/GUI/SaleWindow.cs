using System;
using GLib;
using Gtk;
using Key = Gdk.Key;

namespace RetailManager.GUI
{
	[TypeName(nameof(SaleWindow))]
	[Template(GUIConstants.RES + nameof(SaleWindow))]
	public class SaleWindow : Window
	{
		[Child] private SearchEntry _searchEntry;
		[Child] private TreeView _searchTree;

		[Child] private TreeView _cartTree;
		[Child] private Button _confirmButton;
		[Child] private Button _clearButton;

		[Child] private Label _priceLabel;
		[Child] private Label _vatLabel;
		[Child] private Label _totalLabel;

		private ListStore _searchListStore;
		private CartView _cart;
		private TreeModelFilter _searchFilterModel;

		public SaleWindow() : base(WindowType.Toplevel)
		{
			InitSearchTree();
			InitCartTree();

			InitSearch();

			_clearButton.Activated += ClearCart;
			_clearButton.Clicked += ClearCart;
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
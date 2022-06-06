using System;
using System.Globalization;
using GLib;
using Gtk;

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

		public SaleWindow() : base(WindowType.Toplevel)
		{
			InitSearchTree();
			InitCartTree();
		}

		private void InitSearchTree()
		{
			var model = new ListStore(typeof(CartItem));
			_searchTree.Model = model;

			for (int i = 0; i < 5; i++)
			{
				model.AppendValues(new CartItem()
				{
					Name = $"Item {i}",
					UnitPrice = i
				});
			}
			
			var nameCell = new CellRendererText();
			var nameColumn = _searchTree.AppendColumn("Name", nameCell, CartNameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 100;
			nameColumn.MaxWidth = 700;

			_searchTree.RowActivated += (o, args) =>
			{
				_cartTree.Model.GetIter(out TreeIter iter, args.Path);
				var item = (CartItem) _cartTree.Model.GetValue(iter, 0);
				((ListStore) _cartTree.Model).AppendValues(item);
			};
		}
		
		private void InitCartTree()
		{
			var model = new ListStore(typeof(CartItem));
			_cartTree.Model = model;

			for (int i = 0; i < 5; i++)
			{
				model.AppendValues(new CartItem()
				{
					Name = $"Item {i}",
					UnitPrice = i
				});
			}
			
			var quantityCell = new CellRendererText {Editable = true};
			quantityCell.Edited += QuantityCellOnEdited;
			var quantityColumn = _cartTree.AppendColumn("Quantity", quantityCell, CartQuantityCellFunc);

			var nameCell = new CellRendererText();
			var nameColumn = _cartTree.AppendColumn("Name", nameCell, CartNameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 100;
			nameColumn.MaxWidth = 700;
/*
			var discountCell = new CellRendererText() {Editable = true};
			discountCell.Edited += (o, args) => { };
			discountCell.Xalign = 1;
			TreeViewColumn discountColumn = treeView.AppendColumn("Discount", discountCell);
*/
/*
			var pricePieceCell = new CellRendererText();
			pricePieceCell.Xalign = 1;
			var pricePieceColumn = _cartTree.AppendColumn("Price", pricePieceCell, "text", CartColumns.UnitPrice);
			pricePieceColumn.MinWidth = 100;
			/*
			var priceTotalCell = new CellRendererText();
			priceTotalCell.Xalign = 1;
			TreeViewColumn priceTotalColumn = treeView.AppendColumn("Total", priceTotalCell);
			priceTotalColumn.MinWidth = 100;
			*/
			//treeView.AppendColumn("", new CellRendererText());

			
			
		}

		private void CartNameCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (CartItem)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Name));
		}

		private void CartQuantityCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (CartItem)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Quantity));
		}

		private void QuantityCellOnEdited(object o, EditedArgs args)
		{
			if (int.TryParse(args.NewText, out int value))
			{
				_cartTree.Model.GetIter(out TreeIter iter, new TreePath(args.Path));
				var item = (CartItem) _cartTree.Model.GetValue(iter, 0);
				item.Quantity = value;
			}
		}
	}
}
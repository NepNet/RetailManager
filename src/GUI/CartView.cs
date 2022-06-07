using System;
using System.Collections.Generic;
using GLib;
using Gtk;

namespace RetailManager.GUI
{
	public class CartView
	{
		private ListStore _model;
		private List<CartItem> _items;

		private Label _priceLabel;
		private Label _vatLabel;
		private Label _totalLabel;
		
		public CartView(TreeView treeView, Label price, Label vat, Label total, bool editable)
		{
			_priceLabel = price;
			_vatLabel = vat;
			_totalLabel = total;
			
			_model = new ListStore(typeof(CartItem));
			treeView.Model = _model;
			
			Clear();
			
			if (editable)
			{
				treeView.KeyPressEvent += (o, args) =>
				{
					if (args.Event.Key != Gdk.Key.Delete) return;
					
					treeView.Selection.GetSelected(out var iter);
					Remove(ref iter);
				};
			}
			else
			{
				treeView.Selection.Mode = SelectionMode.None;
			}

			var quantityCell = new CellRendererText {Editable = editable};
			quantityCell.Edited += QuantityCellOnEdited;
			var quantityColumn = treeView.AppendColumn("Quantity", quantityCell, CartQuantityCellFunc);

			var nameCell = new CellRendererText();
			var nameColumn = treeView.AppendColumn("Name", nameCell, CartNameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 100;
			nameColumn.MaxWidth = 700;

			var discountCell = new CellRendererText() {Editable = editable};
			discountCell.Edited += DiscountCellOnEdited;
			discountCell.Xalign = 1;
			TreeViewColumn discountColumn = treeView.AppendColumn("Discount", discountCell, DiscountCellFunc);
			
			var unitPriceCell = new CellRendererText();
			unitPriceCell.Xalign = 1;
			var pricePieceColumn = treeView.AppendColumn("Price", unitPriceCell, UnitPriceCellFunc);
			pricePieceColumn.MinWidth = 100;
			
			var priceTotalCell = new CellRendererText();
			priceTotalCell.Xalign = 1;
			TreeViewColumn priceTotalColumn = treeView.AppendColumn("Total", priceTotalCell, TotalPriceCellFunc);
			priceTotalColumn.MinWidth = 100;
			
			treeView.AppendColumn("", new CellRendererText());
		}

		public void Clear()
		{
			_model.Clear();
			_items = new List<CartItem>();
			UpdateTotal();
		}

		public void Add(CartItem item)
		{
			_items.Add(item);
			_model.AppendValues(item);
			UpdateTotal();
		}

		private void Remove(ref TreeIter iter)
		{
			var item = (CartItem)_model.GetValue(iter, 0);
			_items.Remove(item);
			_model.Remove(ref iter);
			UpdateTotal();
		}
		
		private void UpdateTotal()
		{
			float vat = 0;
			float total = 0;

			float price = 0;

			foreach (var item in _items)
			{
				total += item.TotalPrice;
				vat += item.TotalVAT;
			}

			price = total - vat;

			const string currency = "$";
			
			_priceLabel.Text = $"{price:0.00} {currency}";
			_vatLabel.Text = $"{vat:0.00} {currency}";
			_totalLabel.Text = $"{total:0.00} {currency}";
		}
		
		private void TotalPriceCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (CartItem)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.TotalPrice.ToString("0.000")));
		}

		private void DiscountCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (CartItem)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Discount.ToString("0.00%")));
		}

		private void UnitPriceCellFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (CartItem)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.UnitPrice.ToString("0.000")));
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

		private void DiscountCellOnEdited(object o, EditedArgs args)
		{
			if (!float.TryParse(args.NewText, out float value)) return;
			
			if (value >= 100) return;
			
			_model.GetIter(out var iter, new TreePath(args.Path));
			var item = (CartItem) _model.GetValue(iter, 0);
			item.Discount = value / 100;
			
			UpdateTotal();
		}

		private void QuantityCellOnEdited(object o, EditedArgs args)
		{
			if (!int.TryParse(args.NewText, out int value)) return;
			
			_model.GetIter(out var iter, new TreePath(args.Path));
			if (value <= 0) 
			{
				Remove(ref iter);
			}
			else
			{
				var item = (CartItem) _model.GetValue(iter, 0);
				item.Quantity = value;
			}
			
			UpdateTotal();
		}
	}
}
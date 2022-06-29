using GLib;
using Gtk;
using RetailManager.Data;

namespace RetailManager.GUI
{
	public class CartView
	{
		private Cart _cart;
		public Cart Model => _cart;

		private Label _priceLabel;
		private Label _vatLabel;
		private Label _totalLabel;
		
		public CartView(TreeView treeView, Label price, Label vat, Label total, bool editable)
		{
			treeView.SizeAllocated += (o, args) =>
			{
				treeView.Vadjustment.Value = treeView.Vadjustment.Upper;
			};
			
			_priceLabel = price;
			_vatLabel = vat;
			_totalLabel = total;
			
			_cart = new Cart();
			var model = new CartModel(_cart);
			treeView.Model = model;
			
			Clear();
			
			if (editable)
			{
				treeView.KeyPressEvent += (o, args) =>
				{
					if (args.Event.Key != Gdk.Key.Delete) return;
					
					treeView.Selection.GetSelected(out var iter);
					Remove((CartItem)model.GetValue(iter, 0));
				};
			}
			else
			{
				treeView.Selection.Mode = SelectionMode.None;
			}

			var quantityCell = new CellRendererText
			{
				Editable = editable,
				Xalign = 1
			};
			quantityCell.Edited += QuantityCellOnEdited;
			var quantityColumn = treeView.AppendColumn("Quantity", quantityCell, CartQuantityCellFunc);

			var nameCell = new CellRendererText();
			var nameColumn = treeView.AppendColumn("Name", nameCell, CartNameCellFunc);
			nameColumn.Resizable = true;
			nameColumn.MinWidth = 100;
			nameColumn.MaxWidth = 700;

			var discountCell = new CellRendererText
			{
				Editable = editable, 
				Xalign = 1
			};
			discountCell.Edited += DiscountCellOnEdited;
			var discountColumn = treeView.AppendColumn("Discount", discountCell, DiscountCellFunc);

			var unitPriceCell = new CellRendererText
			{
				Xalign = 1
			};
			var unitPriceColumn = treeView.AppendColumn("Price", unitPriceCell, UnitPriceCellFunc);
			unitPriceColumn.MinWidth = 100;

			var priceTotalCell = new CellRendererText
			{
				Xalign = 1
			};
			var priceTotalColumn = treeView.AppendColumn("Total", priceTotalCell, TotalPriceCellFunc);
			priceTotalColumn.MinWidth = 100;
			
			treeView.AppendColumn("", new CellRendererText());
		}

		public void Clear()
		{
			_cart.Clear();
			UpdateTotal();
		}

		public void Add(CartItem item)
		{
			_cart.AddItem(item.Item, item.Quantity);
			UpdateTotal();
		}

		private void Remove(CartItem item)
		{
			_cart.RemoveItem(item);
			UpdateTotal();
		}
		
		private void UpdateTotal()
		{
			decimal vat = 0;
			decimal total = 0;
			
			foreach (var item in _cart)
			{
				total += item.TotalPrice;
				vat += item.TotalVAT;
			}
			
			var price = total - vat;

			const string currency = "$";
			
			_priceLabel.Text = $"{price:0.00} {currency}";
			_vatLabel.Text = $"{vat:0.00} {currency}";
			_totalLabel.Text = $"{total:0.00} {currency}";
		}
		
		private void TotalPriceCellFunc(TreeViewColumn _, CellRenderer cell, ITreeModel model, TreeIter iter)
		{
			var item = (CartItem)model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.TotalPrice.ToString("0.00")));
		}

		private void DiscountCellFunc(TreeViewColumn _, CellRenderer cell, ITreeModel model, TreeIter iter)
		{
			var item = (CartItem)model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Discount.ToString("0.00%")));
		}

		private void UnitPriceCellFunc(TreeViewColumn _, CellRenderer cell, ITreeModel model, TreeIter iter)
		{
			var item = (CartItem)model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.UnitPrice.ToString("0.00")));
		}

		private void CartNameCellFunc(TreeViewColumn _, CellRenderer cell, ITreeModel model, TreeIter iter)
		{
			var item = (CartItem)model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Name));
		}

		private void CartQuantityCellFunc(TreeViewColumn _, CellRenderer cell, ITreeModel model, TreeIter iter)
		{
			var item = (CartItem)model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Quantity.ToString("0.000")));
		}

		private void DiscountCellOnEdited(object o, EditedArgs args)
		{
			if (!decimal.TryParse(args.NewText, out var value)) return;
			
			if (value >= 100) return;
			
			if (int.TryParse(args.Path, out var index))
			{
				var item = _cart[index];
				item.Discount = value / 100;
			}
			
			UpdateTotal();
		}

		private void QuantityCellOnEdited(object o, EditedArgs args)
		{
			if (!decimal.TryParse(args.NewText, out var value)) return;
			
			if (int.TryParse(args.Path, out var index))
			{
				var item = _cart[index];
				
				if (value <= 0)
				{
					Remove(item);
				}
				else
				{
					item.Quantity = value;
				}
			}

			UpdateTotal();
		}
	}
}
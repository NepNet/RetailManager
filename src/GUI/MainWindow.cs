using System;
using System.Collections.Generic;
using System.Globalization;
using GLib;
using Gtk;
using RetailManager.Data;
using DateTime = System.DateTime;

namespace RetailManager.GUI
{
	[TypeName(nameof(MainWindow))]
	[Template(GUIConstants.RES + nameof(MainWindow))]
	public class MainWindow : Window
	{
		[Child] private Notebook _main;
		[Child("_register_treeview")] private TreeView _registerTreeView;
		[Child("_register_in_label")] private Label _registerIn;
		[Child("_register_out_label")] private Label _registerOut;
		[Child("_register_total_label")] private Label _registerTotal;

		private List<RegisterEntry> registerEntries;
		
		public MainWindow(string title) : base(title)
		{
			SetupRegisterView();
		}

		private void SetupRegisterView()
		{
			registerEntries = new List<RegisterEntry>();
			var op = User.Dummy;
			//MockData
			registerEntries.Add(new RegisterEntry()
			{
				Time = DateTime.Now,
				Value = 100,
				Reason = "Initial sum",
				RecordedBy = op
			});

			registerEntries.Add(new RegisterEntry()
			{
				Time = DateTime.Now,
				Value = 5,
				Reason = "Sale",
				RecordedBy = op
			});

			registerEntries.Add(new RegisterEntry()
			{
				Time = DateTime.Now,
				Value = -80,
				Reason = "Expense",
				RecordedBy = op
			});

			var model = new ListStore(typeof(RegisterEntry));
			
			foreach (var entry in registerEntries)
			{
				model.AppendValues(entry);
			}
			
			_registerTreeView.Model = model;
			
			var operatorCell = new CellRendererText();
			var operatorColumn = _registerTreeView.AppendColumn("Operator", operatorCell, RegisterViewOperatorFunc);
			
			var dateCell = new CellRendererText();
			var dateColumn = _registerTreeView.AppendColumn("Time", dateCell, RegisterViewTimeFunc);
			
			var inCell = new CellRendererText();
			var inColumn = _registerTreeView.AppendColumn("In", inCell, RegisterViewInFunc);
		
			var outCell = new CellRendererText();
			var outColumn = _registerTreeView.AppendColumn("Out", outCell, RegisterViewOutFunc);
			
			var reasonCell = new CellRendererText();
			var reasonColumn = _registerTreeView.AppendColumn("Reason", reasonCell, RegisterViewReasonFunc);

		}

		private void RegisterViewReasonFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (RegisterEntry)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Reason));
		}

		private void RegisterViewOperatorFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (RegisterEntry)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.RecordedBy.ToString()));
		}

		private void RegisterViewInFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (RegisterEntry)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Value > 0 ? item.Value.ToString() : ""));
		}
		
		private void RegisterViewOutFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (RegisterEntry)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Value < 0 ? item.Value.ToString() : ""));
		}

		private void RegisterViewTimeFunc(TreeViewColumn tree_column, CellRenderer cell, ITreeModel tree_model, TreeIter iter)
		{
			var item = (RegisterEntry)tree_model.GetValue(iter, 0);
			cell.SetProperty("text", new Value(item.Time.ToString(CultureInfo.InvariantCulture)));
		}
	}
}

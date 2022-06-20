using System;
using GLib;
using Gtk;

namespace RetailManager.GUI
{
	public class CustomModel : ITreeModel
	{
		public IntPtr Handle { get; }
		public void Foreach(TreeModelForeachFunc func)
		{
			throw new NotImplementedException();
		}

		public GType GetColumnType(int index_)
		{
			throw new NotImplementedException();
		}

		public bool GetIter(out TreeIter iter, TreePath path)
		{
			throw new NotImplementedException();
		}

		public bool GetIterFirst(out TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public bool GetIterFromString(out TreeIter iter, string path_string)
		{
			throw new NotImplementedException();
		}

		public TreePath GetPath(TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public string GetStringFromIter(TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public void GetValist(TreeIter iter, IntPtr var_args)
		{
			throw new NotImplementedException();
		}

		public void GetValue(TreeIter iter, int column, ref Value value)
		{
			throw new NotImplementedException();
		}

		public bool IterChildren(out TreeIter iter, TreeIter parent)
		{
			throw new NotImplementedException();
		}

		public bool IterHasChild(TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public int IterNChildren(TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public bool IterNext(ref TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public bool IterNthChild(out TreeIter iter, TreeIter parent, int n)
		{
			throw new NotImplementedException();
		}

		public bool IterParent(out TreeIter iter, TreeIter child)
		{
			throw new NotImplementedException();
		}

		public bool IterPrevious(ref TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public void RefNode(TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public void EmitRowChanged(TreePath path, TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public void EmitRowDeleted(TreePath path)
		{
			throw new NotImplementedException();
		}

		public void EmitRowHasChildToggled(TreePath path, TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public void EmitRowInserted(TreePath path, TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public void EmitRowsReordered(TreePath path, TreeIter iter, int[] new_order)
		{
			throw new NotImplementedException();
		}

		public int RowsReorderedWithLength(TreePath path, TreeIter iter, int length)
		{
			throw new NotImplementedException();
		}

		public void UnrefNode(TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public bool IterChildren(out TreeIter iter)
		{
			throw new NotImplementedException();
		}

		public int IterNChildren()
		{
			throw new NotImplementedException();
		}

		public bool IterNthChild(out TreeIter iter, int n)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, bool value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, double value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, int value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, string value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, float value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, uint value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(TreeIter iter, int column, object value)
		{
			throw new NotImplementedException();
		}

		public object GetValue(TreeIter iter, int column)
		{
			throw new NotImplementedException();
		}

		public TreeModelFlags Flags { get; }
		public int NColumns { get; }
		public event RowChangedHandler RowChanged;
		public event RowHasChildToggledHandler RowHasChildToggled;
		public event RowDeletedHandler RowDeleted;
		public event RowInsertedHandler RowInserted;
		public event RowsReorderedHandler RowsReordered;
	}
}
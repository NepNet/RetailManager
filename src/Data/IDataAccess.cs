using System.Collections.Generic;

namespace RetailManager.Data
{
	public interface IDataAccess
	{
		public IEnumerable<Item> GetAllItems();
	}
}
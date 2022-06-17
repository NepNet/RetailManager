using System.Collections.Generic;

namespace RetailManager.Data
{
	public interface IDataAccess
	{
		public IEnumerable<Item> GetAllItems();
		public bool LoginUser(string username, string password, out User user);
	}
}
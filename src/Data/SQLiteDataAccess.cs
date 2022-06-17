using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace RetailManager.Data
{
	public class SqLiteDataAccess : IDataAccess
	{
		private SqliteConnection Connect() => new SqliteConnection("Data Source=database.db");

		public IEnumerable<Item> GetAllItems()
		{
			using var connection = Connect();
			return connection.Query<Item>("SELECT Items.Id, Items.Name, Items.UnitPrice, Items.VatGroup, VatGroups.Vat FROM Items INNER JOIN VatGroups ON Items.VatGroup=VatGroups.VatGroup;");
		}
	}
}
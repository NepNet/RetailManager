using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace RetailManager.Data
{
	public class SqLiteDataAccess : IDataAccess
	{
		private static Dictionary<string, string> _queries;
		private static bool _initialized;

		private SqliteConnection Connect()
		{
			var connection = new SqliteConnection("Data Source=database.db");
			if (!_initialized)
			{
				var raw = connection.Query<(string Id, string Query)>("SELECT * FROM Queries");
				_queries = new Dictionary<string, string>();
				foreach (var (id, query) in raw)
				{
					_queries.Add(id, query);
				}
				_initialized = true;
			}

			return connection;
		}

		public IEnumerable<Item> GetAllItems()
		{
			using var connection = Connect();
			return connection.Query<Item>(_queries["items.getAll"]);
		}
	}
}
using System;
using System.Collections.Generic;
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

		public bool LoginUser(string username, string password, out User user)
		{
			using var connection = Connect();
//TODO HASH THE PASSWORD
			var data =
				new
				{
					usr = username,
					hash = password
				};

			try
			{
				user = connection.QueryFirst<User>(
					"SELECT Id,Username,DisplayName,Permissions FROM Users WHERE Username=@usr AND Permissions != 0 AND PasswordHash=@hash", data);
				return true;
			}
			catch (Exception e)
			{
				user = null;
				return false;
			}
		}

		public IEnumerable<Customer> FindCustomers(string name)
		{
			using var connection = Connect();
			var data =
				new
				{
					name = $"%{name}%"
				};
			return connection.Query<Customer>("SELECT * FROM Customers WHERE Name LIKE @name OR CompanyCode LIKE @name;", data);
		}
	}
}
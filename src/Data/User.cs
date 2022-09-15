namespace RetailManager.Data
{
	public class User
	{
		public int Id { get; }
		public string Username { get; }
		public string DisplayName { get; }
		public int Permissions { get; }

		//TODO REMOVE THOSE 2 CONSTRUCTORS
		private User(string username, string displayName)
		{
			Id = 0;
			Username = username;
			DisplayName = displayName;
			Permissions = ~0;
		}
		public User()
		{
			
		}

		private static User _dummy;

		public static User Dummy => _dummy ??= new User("dummy", "Dummy user");

		public override string ToString() => DisplayName;
	}
}
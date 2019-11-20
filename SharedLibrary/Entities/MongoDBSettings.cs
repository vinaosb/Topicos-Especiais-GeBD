namespace SharedLibrary.Context.Custom
{
	public class MongoDBSettings : IMongoDBSettings
	{
		public string ExtrasCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
	public interface IMongoDBSettings
	{
		public string ExtrasCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}
}

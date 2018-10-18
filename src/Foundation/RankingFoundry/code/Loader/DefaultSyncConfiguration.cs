namespace Sitecore.Foundation.RankingFoundry.Loader
{
	public class DefaultSyncConfiguration : ISyncConfiguration
	{
		public DefaultSyncConfiguration(bool updateLinkDatabase, bool updateSearchIndex)
		{
			UpdateLinkDatabase = updateLinkDatabase;
			UpdateSearchIndex = updateSearchIndex;
		}

		public bool UpdateLinkDatabase { get; }
		public bool UpdateSearchIndex { get; }
	}
}

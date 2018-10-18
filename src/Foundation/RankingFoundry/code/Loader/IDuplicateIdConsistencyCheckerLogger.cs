namespace Sitecore.Foundation.RankingFoundry.Loader
{
	public interface IDuplicateIdConsistencyCheckerLogger
	{
		void DuplicateFound(DuplicateIdConsistencyChecker.DuplicateIdEntry existingItemData, IItemData duplicateItemData);
	}
}

using System;

namespace Campspot.Repositories
{
    public interface ISearchQueryRepository
    {
        SearchQuery GetSearchQuery();
    }

    internal class SearchQueryRepository : ISearchQueryRepository
    {
        private readonly ImportTestCases _importTestCases;
        private SearchQuery _cacheSearchQuery;
        
        public SearchQueryRepository(ImportTestCases importTestCases)
        {
            _importTestCases = importTestCases;
        }

        public SearchQuery GetSearchQuery()
        {
            if (_cacheSearchQuery != null)
            {
                return _cacheSearchQuery;
            }
            var data = _importTestCases.GetJsonDataIntoObject();
            var searchJson = data.search;
            _cacheSearchQuery = new SearchQuery
            {
                StartDate = Convert.ToDateTime(searchJson.startDate),
                EndDate = Convert.ToDateTime(searchJson.endDate)
            };
            return _cacheSearchQuery;
        }
    }
}

using System;

namespace Campspot.Repositories
{
    class SearchQueryRepository
    {
        private SearchQuery _cacheSearchQuery;
        private readonly ImportTestCases _importTestCases;
        
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

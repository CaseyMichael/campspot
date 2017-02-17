using System.Collections.Generic;
using System.Linq;

namespace Campspot.Repositories
{
    internal class GapRuleRepository
    {
        private readonly ImportTestCases _importTestCases;
        private IEnumerable<GapRule> _cacheGapRules;
        public GapRuleRepository(ImportTestCases importTestCases)
        {
            _importTestCases = importTestCases;
        }

        public IEnumerable<GapRule> GetGapRules()
        {
            if (_cacheGapRules != null)
            {
                return _cacheGapRules;
            }
            var data = _importTestCases.GetJsonDataIntoObject().gapRules;
            _cacheGapRules = data.Select(x => new GapRule
            {
                GapSize = x.gapSize
            }).ToList();
            return _cacheGapRules;
        }
    }
}

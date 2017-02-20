using System.Collections.Generic;
using System.Linq;

namespace Campspot.Repositories
{
    public interface ICampsiteRepository
    {
        IEnumerable<Campsite> GetCampsites();
    }

    internal class CampsiteRepository : ICampsiteRepository
    {
        private readonly ImportTestCases _importTestCases;
        private IEnumerable<Campsite> _cachedCampsite;

        public CampsiteRepository(ImportTestCases importTestCases)
        {
            _importTestCases = importTestCases;
        }

        public IEnumerable<Campsite> GetCampsites()
        {
            if(_cachedCampsite != null)
            {
                return _cachedCampsite;
            }
            var data = _importTestCases.GetJsonDataIntoObject();
            var campsiteJsons = data.campsites;
            _cachedCampsite = campsiteJsons.Select(campsiteJson => new Campsite
            {
                Id = campsiteJson.id,
                Name = campsiteJson.name
            }).ToList();
            return _cachedCampsite;
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Campspot.Repositories
{
    internal class CampsiteRepository
    {
        private readonly ImportTestCases _importTestCases;

        public CampsiteRepository(ImportTestCases importTestCases)
        {
            _importTestCases = importTestCases;
        }

        public IEnumerable<Campsite> GetCampsites()
        {
            var data = _importTestCases.GetJsonDataIntoObject();
            var campsiteJsons = data.campsites;
            return campsiteJsons.Select(campsiteJson => new Campsite
            {
                Id = campsiteJson.id,
                Name = campsiteJson.name
            }).ToList();
        }
    }
}

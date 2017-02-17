using System.Linq;
using Campspot.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Campspot.Test
{
    [TestClass]
    public class CampspotReservationTests
    {

        [TestMethod]
        public void ShouldGetDataFromJsonFile()
        {
            var filePath = @"..\..\..\test-case.json";
            ImportTestCases test = new ImportTestCases(filePath);
            var data = test.GetJsonDataIntoObject();
            Assert.IsNotNull(data);

            ReservationRepository repo = new ReservationRepository(test);
            var reservations = repo.GetReservations();
            Assert.IsNotNull(reservations);

            CampsiteRepository campsiteRepository = new CampsiteRepository(test);
            var campsites = campsiteRepository.GetCampsites();
            Assert.IsNotNull(campsites);

            SearchQueryRepository searchQueryRepository = new SearchQueryRepository(test);
            var searchQuery = searchQueryRepository.GetSearchQuery();
            Assert.IsNotNull(searchQuery);

            GapRuleRepository gapRuleRepository = new GapRuleRepository(test);
            var gapRules = gapRuleRepository.GetGapRules();
            Assert.IsNotNull(gapRules);
        }

        [TestMethod]
        public void ShouldGetAvailableCampsites()
        {
            var filePath = @"..\..\..\test-case.json";
            ImportTestCases test = new ImportTestCases(filePath);
            SearchQueryRepository searchQueryRepository = new SearchQueryRepository(test);
            ReservationEngine engine = ReservationEngineFactory.Create(filePath);

            var data = engine.GetAvailableCampsitesForSearchQuery(searchQueryRepository.GetSearchQuery()).ToList();

            Assert.IsFalse(data.Exists(x => x.Id == 1));
            Assert.IsFalse(data.Exists(x => x.Id == 2));
            Assert.IsFalse(data.Exists(x => x.Id == 3));
            Assert.IsFalse(data.Exists(x => x.Id == 4));
            Assert.IsTrue(data.Exists(x => x.Id == 5));
            Assert.IsTrue(data.Exists(x => x.Id == 6));
            Assert.IsFalse(data.Exists(x => x.Id == 7));
            Assert.IsTrue(data.Exists(x => x.Id == 8));
            Assert.IsTrue(data.Exists(x => x.Id == 9));
        }

        [TestMethod]
        public void ShouldGetAvailableCampsitesForDifferentFile()
        {
            var filePath = @"..\..\..\test-case2.json";
            ImportTestCases test = new ImportTestCases(filePath);
            SearchQueryRepository searchQueryRepository = new SearchQueryRepository(test);
            ReservationEngine engine = ReservationEngineFactory.Create(filePath);

            var data = engine.GetAvailableCampsitesForSearchQuery(searchQueryRepository.GetSearchQuery()).ToList();

            Assert.IsFalse(data.Exists(x => x.Id == 1));
            Assert.IsFalse(data.Exists(x => x.Id == 2));
            Assert.IsFalse(data.Exists(x => x.Id == 3));
            Assert.IsTrue(data.Exists(x => x.Id == 4));
            Assert.IsTrue(data.Exists(x => x.Id == 5));
            Assert.IsFalse(data.Exists(x => x.Id == 6));
            Assert.IsFalse(data.Exists(x => x.Id == 7));
            Assert.IsTrue(data.Exists(x => x.Id == 8));
            Assert.IsTrue(data.Exists(x => x.Id == 9));
        }

        [TestMethod]
        public void ShouldGetAvailableCampsitesForDifferentGapSize()
        {
            var filePath = @"..\..\..\test-case3.json";
            ImportTestCases test = new ImportTestCases(filePath);
            SearchQueryRepository searchQueryRepository = new SearchQueryRepository(test);
            ReservationEngine engine = ReservationEngineFactory.Create(filePath);

            var data = engine.GetAvailableCampsitesForSearchQuery(searchQueryRepository.GetSearchQuery()).ToList();

            Assert.IsTrue(data.Exists(x => x.Id == 1));
            Assert.IsFalse(data.Exists(x => x.Id == 2));
            Assert.IsFalse(data.Exists(x => x.Id == 3));
            Assert.IsFalse(data.Exists(x => x.Id == 4));
            Assert.IsFalse(data.Exists(x => x.Id == 5));
            Assert.IsFalse(data.Exists(x => x.Id == 6));
            Assert.IsTrue(data.Exists(x => x.Id == 7));
            Assert.IsFalse(data.Exists(x => x.Id == 8));
            Assert.IsFalse(data.Exists(x => x.Id == 9));
        }
    }
}

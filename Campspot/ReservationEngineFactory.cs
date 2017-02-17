using Campspot.Repositories;
using Campspot.ReservationRules;

namespace Campspot
{
    internal class ReservationEngineFactory
    {
        public static ReservationEngine Create(string filename)
        {
            ImportTestCases test = new ImportTestCases(filename);
            ReservationRepository reservationRepository = new ReservationRepository(test);
            CampsiteRepository campsiteRepository = new CampsiteRepository(test);
            GapRuleRepository gapRuleRepository = new GapRuleRepository(test);
            ReservationRuleChain ruleChain = new ReservationRuleChain(gapRuleRepository);
            ReservationEngine engine = new ReservationEngine(reservationRepository, campsiteRepository, ruleChain);
            return engine;
        }
    }
}

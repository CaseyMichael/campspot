using Campspot.Repositories;
using Campspot.ReservationRules;

namespace Campspot
{
    public interface IReservationEngineFactory
    {
        IReservationEngine Create(string filepath);
    }
    public class ReservationEngineFactory
    {
        public static IReservationEngine Create(string filename)
        {
            ImportTestCases test = new ImportTestCases(filename);
            ReservationRepository reservationRepository = new ReservationRepository(test);
            CampsiteRepository campsiteRepository = new CampsiteRepository(test);
            GapRuleRepository gapRuleRepository = new GapRuleRepository(test);
            ReservationRuleChain ruleChain = new ReservationRuleChain(gapRuleRepository);
            IReservationEngine engine = new ReservationEngine(reservationRepository, campsiteRepository, ruleChain);
            return engine;
        }
    }
}

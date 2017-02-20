using Campspot.Repositories;

namespace Campspot.ReservationRules
{
    public interface IReservationRuleChain
    {
        ReservationRuleBase GetStartOfChain();
    }

    internal class ReservationRuleChain : IReservationRuleChain
    {
        private readonly ReservationRuleBase _startOfChain;
        public ReservationRuleChain(GapRuleRepository gapRuleRepository)
        {
            ConcurrencyRule concurrencyRule = new ConcurrencyRule();
            GapReservationRule gapRule = new GapReservationRule(gapRuleRepository);
            DefaultRule defaultRule = new DefaultRule();

            concurrencyRule.SetSuccessor(gapRule);
            gapRule.SetSuccessor(defaultRule);

            _startOfChain = concurrencyRule;
        }

        public ReservationRuleBase GetStartOfChain()
        {
            return _startOfChain;
        }
    }
}

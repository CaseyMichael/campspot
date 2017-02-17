using Campspot.Repositories;

namespace Campspot.ReservationRules
{
    internal class ReservationRuleChain
    {
        private readonly ReservationRuleBase _startOfChain;
        public ReservationRuleChain(GapRuleRepository gapRuleRepository)
        {
            ConcurrencyRule concurrencyRule = new ConcurrencyRule();
            GapRule gapRule = new GapRule(gapRuleRepository);
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

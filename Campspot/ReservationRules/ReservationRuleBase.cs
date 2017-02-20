using System.Collections.Generic;

namespace Campspot.ReservationRules
{
    public abstract class ReservationRuleBase
    {
        protected ReservationRuleBase Successor;

        public void SetSuccessor(ReservationRuleBase successor)
        {
            Successor = successor;
        }

        public abstract bool HandleRequest(IEnumerable<Reservation> reservations, SearchQuery searchQuery);
    }
}

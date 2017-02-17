using System.Collections.Generic;
using Campspot.Repositories;

namespace Campspot.ReservationRules
{
    abstract class ReservationRuleBase
    {
        protected ReservationRuleBase Successor;

        public void SetSuccessor(ReservationRuleBase successor)
        {
            Successor = successor;
        }

        public abstract bool HandleRequest(IEnumerable<Reservation> reservations, SearchQuery searchQuery);
    }
}

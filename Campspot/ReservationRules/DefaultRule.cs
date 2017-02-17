using System.Collections.Generic;

namespace Campspot.ReservationRules
{
    internal class DefaultRule : ReservationRuleBase
    {
        public override bool HandleRequest(IEnumerable<Reservation> reservations, SearchQuery searchQuery)
        {
            return true;
        }
    }
}

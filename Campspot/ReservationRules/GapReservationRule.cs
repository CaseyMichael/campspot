using System.Collections.Generic;
using System.Linq;
using Campspot.Repositories;

namespace Campspot.ReservationRules
{

    internal class GapReservationRule : ReservationRuleBase
    {
        private readonly IGapRuleRepository _gapRuleRepository;

        public GapReservationRule(IGapRuleRepository gapRuleRepository)
        {
            _gapRuleRepository = gapRuleRepository;
        }

        /// <summary>
        /// Determine if making this reservation would create any gap days that are not allowed.
        /// </summary>
        /// <param name="reservations"></param>
        /// <param name="searchQuery"></param>
        /// <returns>Return false to indicate that a reservation at this campsite should not be allowed.</returns>
        public override bool HandleRequest(IEnumerable<Reservation> reservations, SearchQuery searchQuery)
        {
            var reservationsForCampsite = reservations.ToList();
            var gapsNotAllowed = _gapRuleRepository.GetGapRules().Select(x => x.GapSize).ToList();
            var reservationBeforeSearchQuery = GetReservationBeforeSearchQuery(searchQuery, reservationsForCampsite);
            var reservationAfterSearchQuery = GetReservationAfterSearchQuery(searchQuery, reservationsForCampsite);

            if (reservationBeforeSearchQuery != null)
            {
                var gapCreated = (searchQuery.StartDate - reservationBeforeSearchQuery.EndDate).Days - 1;
                if (gapsNotAllowed.Contains(gapCreated))
                {
                    return false;
                }
            }

            if (reservationAfterSearchQuery != null)
            {
                var gapCreated = (reservationAfterSearchQuery.StartDate - searchQuery.EndDate).Days - 1;
                if (gapsNotAllowed.Contains(gapCreated))
                {
                    return false;
                }
            }

            return Successor.HandleRequest(reservationsForCampsite, searchQuery);
        }

        private static Reservation GetReservationAfterSearchQuery(SearchQuery searchQuery, List<Reservation> reservationsForCampsite)
        {
            return reservationsForCampsite
                .Where(x => x.StartDate > searchQuery.EndDate)
                .OrderBy(x => x.StartDate)
                .FirstOrDefault();
        }

        private static Reservation GetReservationBeforeSearchQuery(SearchQuery searchQuery, List<Reservation> reservationsForCampsite)
        {
            return reservationsForCampsite
                .Where(x => x.EndDate < searchQuery.StartDate)
                .OrderBy(x => x.StartDate)
                .LastOrDefault();
        }
       
    }
}

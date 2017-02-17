using System;
using System.Collections.Generic;
using System.Linq;

namespace Campspot.ReservationRules
{
    internal class ConcurrencyRule : ReservationRuleBase
    {
        /// <summary>
        /// Determine if any reservations overlap with the search query.
        /// </summary>
        /// <param name="reservations"></param>
        /// <param name="searchQuery"></param>
        /// <returns>Return false to indicate that a reservation would overlap with the search query.</returns>
        public override bool HandleRequest(IEnumerable<Reservation> reservations, SearchQuery searchQuery)
        {
            var enumerable = reservations as IList<Reservation> ?? reservations.ToList();
            foreach (var reservation in enumerable)
            {
                if (reservation.StartDate <= searchQuery.EndDate && searchQuery.StartDate <= reservation.EndDate)
                {
                    return false;
                } 
            }
            return Successor.HandleRequest(enumerable, searchQuery);
        }
    }
}

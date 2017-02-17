using System.Collections.Generic;
using System.Linq;
using Campspot.Repositories;
using Campspot.ReservationRules;

namespace Campspot
{
    internal class ReservationEngine
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly CampsiteRepository _campsiteRepository;
        private readonly ReservationRuleChain _reservationRuleChain;

        public ReservationEngine(ReservationRepository reservationRepository, CampsiteRepository campsiteRepository, 
            ReservationRuleChain reservationRuleChain)
        {
            _reservationRepository = reservationRepository;
            _campsiteRepository = campsiteRepository;
            _reservationRuleChain = reservationRuleChain;
        }

        public IEnumerable<Campsite> GetAvailableCampsitesForSearchQuery(SearchQuery searchQuery)
        {
            var availableCampsites = new List<Campsite>();
            var ruleChain = _reservationRuleChain.GetStartOfChain();
            foreach (var campsite in _campsiteRepository.GetCampsites())
            {
                var reservationsForCampsite =
                    _reservationRepository.GetReservations()
                        .Where(x => x.CampsiteId == campsite.Id)
                        .ToList();
                if (ruleChain.HandleRequest(reservationsForCampsite, searchQuery))
                {
                    availableCampsites.Add(campsite);
                }
            }
            return availableCampsites;
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using Campspot.Repositories;
using Campspot.ReservationRules;

namespace Campspot
{
    public interface IReservationEngine
    {
        IEnumerable<Campsite> GetAvailableCampsitesForSearchQuery(SearchQuery searchQuery);
    }

    internal class ReservationEngine : IReservationEngine
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICampsiteRepository _campsiteRepository;
        private readonly IReservationRuleChain _reservationRuleChain;

        public ReservationEngine(IReservationRepository reservationRepository, ICampsiteRepository campsiteRepository, 
            IReservationRuleChain reservationRuleChain)
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

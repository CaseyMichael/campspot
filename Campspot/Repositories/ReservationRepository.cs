using System;
using System.Collections.Generic;
using System.Linq;

namespace Campspot.Repositories
{
    internal class ReservationRepository
    {
        private readonly ImportTestCases _importTestCases;
        private IEnumerable<Reservation> _cacheReservations;

        public ReservationRepository(ImportTestCases importTestCases)
        {
            _importTestCases = importTestCases;
        }

        public IEnumerable<Reservation> GetReservations()
        {
            if (_cacheReservations != null)
            {
                return _cacheReservations;
            }
            var data = _importTestCases.GetJsonDataIntoObject();
            var reservationJsons = data.reservations;
            _cacheReservations = reservationJsons.Select(reservationJson => new Reservation
            {
                CampsiteId = reservationJson.campsiteId,
                StartDate = Convert.ToDateTime(reservationJson.startDate),
                EndDate = Convert.ToDateTime(reservationJson.endDate)
            }).ToList();
            return _cacheReservations;
        }
    }
}

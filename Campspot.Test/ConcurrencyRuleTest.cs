using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Campspot.ReservationRules;

namespace Campspot.Test
{
    [TestClass]
    public class ConcurrencyRuleTest
    {
        private ConcurrencyRule GetRule()
        {
            ConcurrencyRule rule = new ConcurrencyRule();
            DefaultRule endOfChain = new DefaultRule();
            rule.SetSuccessor(endOfChain);
            return rule;
        }

        [TestMethod]
        public void DoesReservationOccurDuringQuery()
        {
            List<Reservation> reservations = new List<Reservation>
            {
                new Reservation
                {
                    CampsiteId = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3)
                }
            };

            SearchQuery query = new SearchQuery
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            };           

            var result = GetRule().HandleRequest(reservations, query);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoesReservationEndWhenSearchQueryBegins()
        {
            List<Reservation> reservations = new List<Reservation>
            {
                new Reservation
                {
                    CampsiteId = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3)
                }
            };

            SearchQuery query = new SearchQuery
            {
                StartDate = DateTime.Now.AddDays(3),
                EndDate = DateTime.Now.AddDays(6)
            };

            var result = GetRule().HandleRequest(reservations, query);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoesReservationStartWhenSearchQueryEnds()
        {
            List<Reservation> reservations = new List<Reservation>
            {
                new Reservation
                {
                    CampsiteId = 0,
                    StartDate = DateTime.Now.AddDays(3),
                    EndDate = DateTime.Now.AddDays(5)
                }
            };

            SearchQuery query = new SearchQuery
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3)
            };

            var result = GetRule().HandleRequest(reservations, query);

            Assert.IsFalse(result);
        }
    }
}

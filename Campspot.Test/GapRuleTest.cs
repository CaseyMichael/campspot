using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Campspot.ReservationRules;
using Campspot.Repositories;
using System.Collections.Generic;
using NSubstitute;

namespace Campspot.Test
{
    [TestClass]
    public class GapRuleTest
    {
        private GapReservationRule GetGapRule(int gapNotAllowed)
        {
            var repo = Substitute.For<IGapRuleRepository>();
            repo.GetGapRules().Returns(new List<GapRule>
            {
                new GapRule
                {
                    GapSize = gapNotAllowed
                }
            });
            GapReservationRule rule = new GapReservationRule(repo);
            DefaultRule defaultRule = new DefaultRule();
            rule.SetSuccessor(defaultRule);
            return rule;
        }

        [TestMethod]
        public void DoesPreviousReservationCreateOneDayGap()
        {
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    CampsiteId = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3)
                }
            };
            var query = new SearchQuery
            {
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(7)
            };
            var result = GetGapRule(1).HandleRequest(reservations, query);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoesNextReservationCreateOneDayGap()
        {
            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    CampsiteId = 0,
                    StartDate = DateTime.Now.AddDays(4),
                    EndDate = DateTime.Now.AddDays(7)
                }
            };
            var query = new SearchQuery
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            };
            var result = GetGapRule(1).HandleRequest(reservations, query);
            Assert.IsFalse(result);
        }
    }
}

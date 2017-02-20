using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campspot
{

    internal class Rootobject
    {
        public SearchJson search { get; set; }
        public CampsiteJson[] campsites { get; set; }
        public GapruleJson[] gapRules { get; set; }
        public ReservationJson[] reservations { get; set; }
    }

    internal class SearchJson
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    internal class CampsiteJson
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    internal class GapruleJson
    {
        public int gapSize { get; set; }
    }

    internal class ReservationJson
    {
        public int campsiteId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
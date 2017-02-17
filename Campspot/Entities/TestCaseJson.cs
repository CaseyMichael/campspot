using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campspot
{

    public class Rootobject
    {
        public SearchJson search { get; set; }
        public CampsiteJson[] campsites { get; set; }
        public GapruleJson[] gapRules { get; set; }
        public ReservationJson[] reservations { get; set; }
    }

    public class SearchJson
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

    public class CampsiteJson
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class GapruleJson
    {
        public int gapSize { get; set; }
    }

    public class ReservationJson
    {
        public int campsiteId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
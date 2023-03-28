using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("car")]
    public class ExportCarWithDistanceDto
    {
        //      <cars>
        //<car>
        //  <make>BMW</make>
        //  <model>1M Coupe</model>
        //  <traveled-distance>39826890</traveled-distance>
        //</car>
        [XmlElement("make")]
        public string Make { get; set; } = null!;
        [XmlElement("model")]

        public string Model { get; set; } = null!;
        [XmlElement("traveled-distance")]

        public long TraveledDistance { get; set; }



    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Part")]
    public class ImportPartDto
    {
        //    <Part>
        //    <name>Bonnet/hood</name>
        //    <price>1001.34</price>
        //    <quantity>10</quantity>
        //    <supplierId>17</supplierId>
        //</Part>
        [XmlElement("name")]
        public string Name { get; set; } = null!;
        [XmlElement("price")]

        public decimal Price { get; set; }
        [XmlElement("quantity")]

        public int Quantity { get; set; }
        [XmlElement("supplierId")]

        public int SupplierId { get; set; }





    }
}

using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Sale")]
    public class ImportSaleDto
    {
        //            <Sales> => root
        //<Sale> => type
        //    <carId>105</carId> => element
        //    <customerId>30</customerId>
        //    <discount>30</discount>
        //</Sale>

        [XmlElement("carId")]
        public int CarId { get; set; }
        [XmlElement("customerId")]

        public int CustomerId { get; set; }
        [XmlElement("discount")]

        public decimal Discount { get; set; }


    }
}

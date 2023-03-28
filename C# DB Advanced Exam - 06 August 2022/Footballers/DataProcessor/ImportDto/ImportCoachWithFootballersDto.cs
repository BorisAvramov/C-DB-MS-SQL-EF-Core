using Footballers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Coach")]
    public class ImportCoachWithFootballersDto
    {
        //      <Coaches>
        //<Coach>
        //  <Name>S</Name>
        //  <Nationality>25/01/2018</Nationality>

        [Required]
        [MaxLength(ValidationsConstants.CoachNameMaxLength)]
        [MinLength(ValidationsConstants.CoachNameMInLength)]
        [XmlElement(nameof(Name))]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(Nationality))]

        public string Nationality { get; set; } = null!;



        [XmlArray(nameof(Footballers))]
        public ImportCoachFootballerDto[] Footballers { get; set; }

    }
}

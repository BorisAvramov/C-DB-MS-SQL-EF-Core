using Footballers.Data.Models.Enums;
using Footballers.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Footballers.Data;
using System.ComponentModel.DataAnnotations;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Footballer")]
    public class ImportCoachFootballerDto
    {
        //  <Footballers>
        //<Footballer>
        //  <Name>Benjamin Bourigeaud</Name>
        //  <ContractStartDate>22/03/2020</ContractStartDate>
        //  <ContractEndDate>24/02/2026</ContractEndDate>
        //  <BestSkillType>2</BestSkillType>
        //  <PositionType>2</PositionType>
        //</Footballer >

        [Required]
        [MaxLength(ValidationsConstants.FootballerNameMaxLength)]
        [MinLength(ValidationsConstants.FootballerNameMinLength)]
        [XmlElement(nameof(Name))]
        public string Name { get; set; } = null!;

        [Required]
        [XmlElement(nameof(ContractStartDate))]
        public string ContractStartDate { get; set; } = null!;
        [Required]
        [XmlElement(nameof(ContractEndDate))]

        public string ContractEndDate { get; set; } = null!;

        [Required]
        [XmlElement(nameof(BestSkillType))]

        public string BestSkillType { get; set; } = null!;

        [Required]
        [XmlElement(nameof(PositionType))]

        public string PositionType { get; set; } = null!;


    }
}

using SoftJail.Data.Models.Enums;
using SoftJail.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using SoftJail.Common;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficerDto
    {
        //      <Officer>
        //  <Name>Minerva Holl</Name>
        //  <Money>2582.55</Money>
        //  <Position>Overseer</Position>
        //  <Weapon>ChainRifle</Weapon>
        //  <DepartmentId>2</DepartmentId>
        //  <Prisoners>
        //    <Prisoner id = "15" />
        //  </ Prisoners >
        //</ Officer >

        [Required]
        [XmlElement("Name")]
        [MinLength(ValidationsConstants.OfficerFullNameMinLength)]
        [MaxLength(ValidationsConstants.OfficerFullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [XmlElement("Money")]
        [Range(typeof(decimal), ValidationsConstants.salaryMinValue, ValidationsConstants.salaryMaxValue)]
        public decimal Salary { get; set; }
        [Required]
        [XmlElement("Position")]
        public string Position { get; set; }
        [Required]
        [XmlElement("Weapon")]

        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public ImportOfficerPrisonerDto[] Prisoners { get; set; }






    }
}

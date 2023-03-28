using Newtonsoft.Json;
using SoftJail.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Schema;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentCellDto
    {
        [Range(ValidationsConstants.CellMinValue, ValidationsConstants.CellMaxValue)]
        [JsonProperty(nameof(CellNumber))]
        public int CellNumber { get; set; }
        [JsonProperty(nameof(HasWindow))]

        public bool HasWindow { get; set; }


    }
}

using Newtonsoft.Json;
using SoftJail.Common;
using SoftJail.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentWithCellsDto
    {

        [Required]
        [MaxLength(ValidationsConstants.DepartmentNameMaxLength)]
        [MinLength(ValidationsConstants.DepartmentNameMinLength)]
        [JsonProperty(nameof(Name))]
        public string Name { get; set; } = null!;

        [JsonProperty(nameof(Cells))]
        public ImportDepartmentCellDto[] Cells { get; set; }



    }
}

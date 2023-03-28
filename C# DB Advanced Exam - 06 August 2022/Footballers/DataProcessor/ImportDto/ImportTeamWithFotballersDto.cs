using Footballers.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footballers.DataProcessor.ImportDto
{
    public class ImportTeamWithFotballersDto
    {
        //        "Name": "Brentford F.C.",
        //"Nationality": "The United Kingdom",
        //"Trophies": "5",
        //"Footballers": [
        //  28,
        //  28,
        //  39,
        //  57
        //]

        [Required]
        [MaxLength(ValidationsConstants.TeamNameMaxLength)]
        [MinLength(ValidationsConstants.TeamNameMinLength)]
        [RegularExpression(ValidationsConstants.TeamNameRegexPattern)]
        [JsonProperty(nameof(Name))]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ValidationsConstants.NationalityMaxLength)]
        [MinLength(ValidationsConstants.NationalityMinLength)]
        [JsonProperty(nameof(Nationality))]
        public string Nationality { get; set; } = null!;

        [Required]
        [JsonProperty(nameof(Trophies))]
        public string Trophies { get; set; }

        public int[] Footballers { get; set; }
    }
}

using Newtonsoft.Json;
using SoftJail.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerWithMailsDto
    {
        //    "FullName": "",
        //"Nickname": "The Wallaby",
        //"Age": 32,
        //"IncarcerationDate": "29/03/1957",
        //"ReleaseDate": "27/03/2006",
        //"Bail": null,
        //"CellId": 5,
        //"Mails": [
        //  {
        //    "Description": "Invalid FullName",
        //    "Sender": "Invalid Sender",
        //    "Address": "No Address"
        //  },

        [Required]
        [JsonProperty(nameof(FullName))]
        [MaxLength(ValidationsConstants.PrisonerFullNameMaxLength)]
        [MinLength(ValidationsConstants.PrisonerFullNameMinLength)]
        public string FullName { get; set; } = null!;

        [Required]
        [JsonProperty(nameof(Nickname))]
        [RegularExpression(ValidationsConstants.PrisonerNicknameRegexPattern)]
        public string Nickname { get; set; } = null!;
        [Required]
        [JsonProperty(nameof(Age))]
        [Range(ValidationsConstants.AgeMInValue, ValidationsConstants.AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        [JsonProperty(nameof(IncarcerationDate))]
        public string IncarcerationDate { get; set; }

        [JsonProperty(nameof(ReleaseDate))]
        public string? ReleaseDate { get; set; }

        [JsonProperty(nameof(Bail))]
        [Range(typeof(decimal), ValidationsConstants.bailMinValue, ValidationsConstants.bailMaxValue)]
        public decimal? Bail { get; set; }
        [JsonProperty(nameof(CellId))]
        public int? CellId { get; set; }
        [JsonProperty(nameof(Mails))]
        public List<ImportPrisonerMailDto>  Mails { get; set; }



    }
}

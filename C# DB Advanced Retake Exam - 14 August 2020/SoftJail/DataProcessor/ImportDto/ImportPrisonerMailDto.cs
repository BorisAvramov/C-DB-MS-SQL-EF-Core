using Newtonsoft.Json;
using SoftJail.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerMailDto
    {
        //"Mails": [
        //  {
        //    "Description": "Invalid FullName",
        //    "Sender": "Invalid Sender",
        //    "Address": "No Address"
        //  },

        [Required]
        [JsonProperty(nameof(Description))]
        public string Description { get; set; } = null!;
        [Required]
        [JsonProperty(nameof(Sender))]
        public string Sender { get; set; } = null!;

        [Required]
        [JsonProperty(nameof(Address))]
        [RegularExpression(ValidationsConstants.MailAddressRegexPattern)]
         public string Address { get; set; } = null!;

    }
}

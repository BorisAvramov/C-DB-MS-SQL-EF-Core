using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Song
    {
        public int Id { get; set; }

        [MaxLength(ValidateConstants.songNameMaxLength)]
        public string Name { get; set; } = null!;

        // in db will be stored as BIGINT
        //required by default
        public TimeSpan Duration  { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedOn  { get; set; }

        // enumerators are stored in db as int
        public Genre Genre  { get; set; }

        public int? AlbumId  { get; set; }

        public int WriterId  { get; set; }

        public decimal Price  { get; set; }

        




    }
}

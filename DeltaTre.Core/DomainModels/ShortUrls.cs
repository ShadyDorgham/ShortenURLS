using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeltaTre.Core.DomainModels
{
    public class ShortUrl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string OriginalUrl   { get; set; }
        [Required]
        public string ShortenUrl { get; set; }
        [Required]
        public DateTime InsertDate
        {
            get =>
                _dateCreated ?? DateTime.Now;

            set => _dateCreated = value;
        }

        private DateTime? _dateCreated;
    }
}

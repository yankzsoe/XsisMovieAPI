using System.ComponentModel.DataAnnotations;
using XsisMovieAPI.Domain.Common;

namespace XsisMovieAPI.Domain.Entities {
    public class Movie : AuditEntity {

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        public float Rating { get; set; }

        public string Image { get; set; }
    }
}

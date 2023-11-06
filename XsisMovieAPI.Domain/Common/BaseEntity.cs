using System.ComponentModel.DataAnnotations;

namespace XsisMovieAPI.Domain.Common {
    public class BaseEntity {
        [Required]
        public int Id { get; set; }
    }
}

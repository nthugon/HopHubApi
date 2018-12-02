using System.ComponentModel.DataAnnotations;

namespace HopHubApi.Models
{
    public class Beer
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Style { get; set; }
        [Required]
        public string Brewery { get; set; }
        [Required]
        public decimal Abv { get; set; }
    }
}

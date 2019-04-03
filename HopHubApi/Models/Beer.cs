using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopHubApi.Models
{
    public class Beer
    {
        public int BeerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Style { get; set; }
        [Required]
        public string Brewery { get; set; }
        [Required]
        public decimal Abv { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}

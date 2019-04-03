using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HopHubApi.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Required]
        public string DrinkAgain { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public int BeerId { get; set; }           
    }
}

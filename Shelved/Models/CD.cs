using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class CD
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Artist { get; set; }
        public string Year { get; set; }

        [Display(Name = "I've Heard This")]
        public bool IsHeard { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<CDGenre> CDGenres { get; set; }
    }
}

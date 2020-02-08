using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Display(Name = "Heard")]
        public bool IsHeard { get; set; }

        [Display(Name = "Album Cover")]
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<CDGenre> CDGenres { get; set; }

        [Display(Name = "Add To My Music")]
        public bool MyMusic { get; set; }

        [Display(Name = "Add To List List")]
        public bool ListenList { get; set; }

        [Display(Name = "Add To Wish List")]
        public bool WishList { get; set; }

        [Display(Name = "Add To Heard It List")]
        public bool HeardList { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}

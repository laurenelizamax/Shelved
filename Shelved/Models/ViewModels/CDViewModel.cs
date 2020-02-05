using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models.ViewModels
{
    public class CDViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Artist { get; set; }
        public string Year { get; set; }

        [Display(Name = "I've Heard This")]
        public bool IsHeard { get; set; }

        [Display(Name = "Album Cover")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Please Select Genre(s)")]
        [Display(Name = "Genres")]
        public List<int> GenreIds { get; set; }
        public GenresForCDs GenresForCDs { get; set; }

        [Display(Name = "Add To My Music")]
        public bool MyMusic { get; set; }

        [Display(Name = "Add To Listen List")]
        public bool ListenList { get; set; }

        [Display(Name = "Add To Wish List")]
        public bool WishList { get; set; }

        [Display(Name = "Add To Heard That List")]
        public bool HeardList { get; set; }
        public IFormFile File { get; set; }

    }
}

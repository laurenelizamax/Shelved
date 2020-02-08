using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Year { get; set; }

        [Display(Name = "Watched")]
        public bool IsWatched { get; set; }

        [Display(Name = "Movie Cover")]
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<MovieGenre> MovieGenres { get; set; }

        [Display(Name = "Add To My Movies")]
        public bool MyMovies { get; set; }

        [Display(Name = "Add To Watch List")]
        public bool WatchList { get; set; }

        [Display(Name = "Add To Wish List")]
        public bool WishList { get; set; }

        [Display(Name = "Add To Seen List")]
        public bool SeenList { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}

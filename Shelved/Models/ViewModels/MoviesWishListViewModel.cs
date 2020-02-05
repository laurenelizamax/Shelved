using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models.ViewModels
{
    public class MoviesWishListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Year { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<int> GenreIds { get; set; }
        public GenresForMovies GenresForMovies { get; set; }

        [Display(Name = "Genres")]
        public List<MovieGenre> MovieGenres { get; set; }
    }
}

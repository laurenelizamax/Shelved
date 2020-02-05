using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }

        [Display(Name = "I've Read This")]
        public bool IsRead { get; set; }

        [Display(Name = "Book Cover")]
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<BookGenre> BookGenres { get; set; }

        [Display(Name = "Add To My Books")]
        public bool MyBooks { get; set; }

        [Display(Name = "Add To Read List")]
        public bool ReadList { get; set; }

        [Display(Name = "Add To Wish List")]
        public bool WishList { get; set; }

        [Display(Name = "Add To Read It List")]
        public bool ReadItList { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

    }
}

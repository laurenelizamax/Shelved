using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Required]
        public string Title { get; set; }
        public string  ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Year { get; set; }

        [Display(Name = "Read")]
        public bool IsRead { get; set; }

        [Display(Name = "Book Cover")]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Genres")]
        public List<BookGenre> BookGenres { get; set; }

        [Display(Name = "Add To My Books")]
        public bool MyBooks { get; set; }

        [Display(Name = "Add To Books to Read")]
        public bool ReadList { get; set; }

        [Display(Name = "Add To Wish List")]
        public bool WishList { get; set; }

        [Display(Name = "Add To Read It List")]
        public bool ReadItList { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

    }
}

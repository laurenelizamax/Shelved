﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Author { get; set; }
   
        [Required]
        public string Year { get; set; }

        [Display(Name = "I've Read This")]
        public bool IsRead { get; set; }

        [Display(Name = "Book Cover")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Please Select Genre(s)")]
        [Display(Name = "Genres")]
        public List<int> GenreIds { get; set; }
        public List<BookGenre> BookGenres { get; set; }

        [Display(Name = "Add To My Books")]
        public bool MyBooks { get; set; }

        [Display(Name = "Add To Read List")]
        public bool ReadList { get; set; }

        [Display(Name = "Add To Wish List")]
        public bool WishList { get; set; }

        [Display(Name = "Add To Read That List")]
        public bool ReadItList { get; set; }

        public IFormFile File { get; set; }

        public int CurrentPage { get; set; }
        public int RecordsPerPage { get; set; } = 5;

    }
}

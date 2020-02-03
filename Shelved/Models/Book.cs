using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<BookGenre> BookGenres { get; set; }

    }
}

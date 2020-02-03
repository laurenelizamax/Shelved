﻿using System;
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
        public string ImagePath { get; set; }

        [Display(Name = "Genres")]
        public List<int> GenreIds { get; set; }
        public GenresForCDs GenresForCDs { get; set; }

    }
}
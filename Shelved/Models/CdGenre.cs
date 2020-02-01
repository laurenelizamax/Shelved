using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class CDGenre
    {
        public int Id { get; set; }
        public int CDId { get; set; }
        public CD CD { get; set; }
        public int GenreId { get; set; }
        public GenresForCDs GenresForCDs { get; set; }
    }
}


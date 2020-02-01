using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class GenresForCDs
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<CDGenre> CDGenres { get; set; }
    }
}

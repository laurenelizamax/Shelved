using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class GenresForBooks
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}

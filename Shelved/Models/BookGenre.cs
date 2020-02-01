using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelved.Models
{
    public class BookGenre
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int GenreId { get; set; }
        public GenresForBooks GenresForBooks { get; set; }
    }
}

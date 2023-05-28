using System.Collections.Generic;

namespace Lec11.Data
{
    public class Language
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }    
    }
}

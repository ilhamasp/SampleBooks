﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoBooks.Models
{
    public class Author
    {
        public int AuthorID { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
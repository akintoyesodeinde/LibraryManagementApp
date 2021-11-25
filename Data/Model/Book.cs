using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Model
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public Customer Borrower { get; set; }
        public int BorrowerId { get; set; }
    }
}

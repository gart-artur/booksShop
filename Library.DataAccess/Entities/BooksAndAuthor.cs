using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("BooksAndAuthors")]

    public class BooksAndAuthor : BaseEntity
    {
        public  int BookId {get;set;}
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

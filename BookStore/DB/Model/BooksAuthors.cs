namespace BookStore.DB.Model;

public class BooksAuthors
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }

    public BookEntity BookEntity { get; set; }
    public AuthorEntity AuthorEntity { get; set; }
}
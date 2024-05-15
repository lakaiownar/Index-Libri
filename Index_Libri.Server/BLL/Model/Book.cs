using Index_Libri.Server.BLL.Model;

public class Book
{
    public string ISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Pages { get; set; }
    public int Rating { get; set; }
    public string BookCover { get; set; }

    // Foreign key for BookList
    internal int BookListId { get; set; }

    // Navigation property for BookList
    internal BookList BookList { get; set; }

    public Book()
    {
    }
}

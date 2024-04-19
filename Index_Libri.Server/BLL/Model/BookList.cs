using Index_Libri.Server.BLL.Model;

public class BookList
{
    public int BookListId { get; set; }
    public string UserEmail { get; set; }

    // Navigation property for ApplicationUser
    internal ApplicationUser ApplicationUser { get; set; }

    // Make this property public and virtual
    public virtual List<Book> Books { get; set; }

    public BookList()
    {
        Books = new List<Book>();
    }
}

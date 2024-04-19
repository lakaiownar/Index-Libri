using Index_Libri.Server.BLL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Index_Libri.Server.Controllers
{
    [ApiController]
    [Route("libri/[controller]")]
    public class BookListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/booklist")]
        public async Task<IActionResult> GetBookList([FromHeader] string email)
        {
            try
            {
                // Retrieve the BookList for the user with the given email
                var bookList = await _context.BookList
                    .Include(b => b.Books)
                    .Where(b => b.UserEmail == email)
                    .FirstOrDefaultAsync();

                return Ok(bookList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Add a book to the user's BookList
        [HttpPost("/booklist/add")]
        public async Task<IActionResult> AddBook([FromBody] Book book, [FromQuery] string email)
        {
            try
            {
                // Retrieve the BookList for the user with the given email
                var bookList = await _context.BookList
                    .Include(b => b.Books)
                    .Where(b => b.UserEmail == email)
                    .FirstOrDefaultAsync();

                // If the BookList does not exist, create a new one
                if (bookList == null)
                {
                    bookList = new BookList
                    {
                        UserEmail = email,
                        Books = new List<Book>()
                    };
                    _context.BookList.Add(bookList);
                }

                // Add the book to the BookList
                bookList.Books.Add(book);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // Remove book from list
        [HttpDelete("/booklist/remove")]
        public async Task<IActionResult> RemoveBook([FromQuery] string email, [FromQuery] string isbn)
        {
            try
            {
                // Retrieve the BookList for the user with the given email
                var bookList = await _context.BookList
                    .Include(b => b.Books)
                    .Where(b => b.UserEmail == email)
                    .FirstOrDefaultAsync();

                // If the BookList does not exist, return a 404 Not Found
                if (bookList == null)
                {
                    return NotFound();
                }

                // Retrieve the book with the given ISBN
                var book = bookList.Books
                    .Where(b => b.ISBN == isbn)
                    .FirstOrDefault();

                // If the book does not exist, return a 404 Not Found
                if (book == null)
                {
                    return NotFound();
                }

                // Remove the book from the BookList
                bookList.Books.Remove(book);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

using Index_Libri.Server.BLL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Index_Libri.Server.Controllers
{
    public class BookListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public BookListController(ApplicationDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
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
        [HttpDelete("/booklist/delete")]
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

        // Update a book from the list
        [HttpPut("/booklist/update")]
        public async Task<IActionResult> UpdateBook([FromQuery] string email, [FromQuery] string isbn, [FromBody] Book updatedBook)
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

                // Update the book's properties
                book.Pages = updatedBook.Pages;
                book.Rating = updatedBook.Rating;
                book.Status = updatedBook.Status;

                // Save the changes to the databa
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("/booklist/recommendations")]
        public async Task<IActionResult> GetRecommendations([FromQuery] string email)
        {
            // Find the user's highest-rated book
            var highestRatedBook = await _context.BookList
                .Where(bl => bl.UserEmail == email)
                .SelectMany(bl => bl.Books)
                .OrderByDescending(b => b.Rating)
                .FirstOrDefaultAsync();

            if (highestRatedBook == null)
            {
                return NotFound("No books found for this user.");
            }

            // Fetch recommendations from Google Books API
            var url = $"https://www.googleapis.com/books/v1/volumes/{highestRatedBook.GoogleId}/associated";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error fetching recommendations from Google Books API.");
            }

            var json = await response.Content.ReadAsStringAsync();
            var recommendations = JObject.Parse(json)["items"].ToObject<List<Book>>();

            return Ok(recommendations);
        }

        [HttpGet("/booklist/favouritebook")]
        public async Task<IActionResult> FetchHighRatedBook([FromQuery] string email)
        {
            // Find the user's highest-rated book
            var highestRatedBook = await _context.BookList
                .Where(bl => bl.UserEmail == email)
                .SelectMany(bl => bl.Books)
                .OrderByDescending(b => b.Rating)
                .FirstOrDefaultAsync();

            if (highestRatedBook == null)
            {
                return NotFound("No books found for this user.");
            }

            return Ok(highestRatedBook);
        }
    }
}

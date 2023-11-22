using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;

namespace BookStore.Controllers
{
    /// <summary>
    /// Book Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness bookBusiness;
        private readonly ILogger<BookController> logger;
        public BookController(IBookBusiness bookBusiness,ILogger<BookController> logger)
        {
            this.bookBusiness = bookBusiness;
            this.logger = logger;
        }
        /// <summary>
        /// Adding Book authorized by Admin
        /// </summary>
        /// <param name="bookModel"> Book to add </param>
        /// <returns> Book info </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("addbook")]
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                var result = bookBusiness.AddBook(bookModel);
                if (result != null)
                {
                    return Ok(new { Success = true , Message = "Book Added Successfully", Data = result});
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Book can't be added." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during Adding Book");
                return BadRequest(new { Success = false, Message = "Adding Book could not completed" });
            }
        }
        /// <summary>
        /// Getting particular which is Authorized by Admin
        /// </summary>
        /// <param name="bookId"> Book Id </param>
        /// <returns> Book Info </returns>
        [HttpGet]
        [Route("bookbyid")]
        public IActionResult GetParticularBookById(int bookId)
        {
            try
            {
                var book = bookBusiness.GetBookById(bookId);
                if(book != null)
                {
                    return Ok(new { Success = true, Message = "Book by bookId = " + bookId + ".", Data = book });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Book by bookId = " + bookId + " cannot be fetch." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during fetching book by Id");
                return BadRequest(new { Success = false, Message = "Fetching Book could not be completed" });
            }
        }
        /// <summary>
        /// Getting all Books.
        /// </summary>
        /// <returns> All Books </returns>
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = bookBusiness.GetAllBooks();
                if (result != null)
                {
                    return Ok(new { Success = true, Message = "All books...", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false,Message = "Can't fetch all books."});
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occur during get all Books");
                return BadRequest(new { Success = false, Message = "Fetching all books could not be completed" });
            }
        }
        /// <summary>
        /// Updating particular and authorized by Admin
        /// </summary>
        /// <param name="updateBookModel"> Book info to update </param>
        /// <param name="bookId"> Book Id </param>
        /// <returns> Updated Book Result </returns>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("updatebook")]
        public IActionResult UpdateBook(BookModel updateBookModel, int bookId)
        {
            try
            {
                var result = bookBusiness.UpdateBooks(updateBookModel, bookId);
                if(result != null)
                {
                    return Ok(new { Success = true,Message = "Books details updated Successfully.",Data = result});
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Books details can not be updated" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during updating the book ");
                return BadRequest(new { Success = false, Message = "Updating books could not be completed" });
            }
        }
        /// <summary>
        /// Deleting book which are authorized by Admin
        /// </summary>
        /// <param name="bookId"> Book Id </param>
        /// <returns> Boolean Value </returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("deletebook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if(bookId != 0)
                {
                    bookBusiness.DeleteBook(bookId);
                    return Ok(new { Success = true, Message = "Book deleted successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Book can't be deleted" });
                }
            }
            catch (Exception ex )
            {
                logger.LogError(ex, "Error come during deleting Book");
                return BadRequest(new { success = false, Message = "Deleting book could not be completed" });
            }
        }
    }
}

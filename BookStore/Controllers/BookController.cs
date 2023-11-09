using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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
        public BookController(IBookBusiness bookBusiness)
        {
            this.bookBusiness = bookBusiness;
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
            catch (System.Exception)
            {

                throw;
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
            catch (System.Exception)
            {

                throw;
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
            catch (System.Exception)
            {

                throw;
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
            catch (System.Exception)
            {

                throw;
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
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

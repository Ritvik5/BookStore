using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Interface;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    /// <summary>
    /// Book CRUD Operations
    /// </summary>
    public class BookRepo : IBookRepo
    {
        private readonly BookStoreDBContext bookStoreDBContext;
        private readonly IConfiguration configuration;
        public BookRepo(BookStoreDBContext bookStoreDBContext, IConfiguration configuration)
        {
            this.bookStoreDBContext = bookStoreDBContext;
            this.configuration = configuration;
        }
        /// <summary>
        /// Adding book
        /// </summary>
        /// <param name="addBookModel"> Book information</param>
        /// <returns> Book Entity </returns>
        public BookTable AddBook(BookModel addBookModel)
        {
            try
            {
                BookTable bookTable = new BookTable();
                bookTable.BookName = addBookModel.BookName;
                bookTable.AuthorName = addBookModel.AuthorName;
                bookTable.DiscountPrice = addBookModel.DiscountPrice;
                bookTable.OriginalPrice = addBookModel.OriginalPrice;
                bookTable.BookDescription = addBookModel.BookDescription;
                bookTable.BookImage = addBookModel.BookImage;
                bookTable.BookQuantity = addBookModel.BookQuantity;

                bookStoreDBContext.BookTable.Add(bookTable);
                bookStoreDBContext.SaveChanges();
                if(bookTable != null )
                {
                    return bookTable;
                }
                else { return null; }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get all books from database
        /// </summary>
        /// <returns> All Books </returns>
        public List<BookTable> GetAllBooks()
        {
            try
            {
                List<BookTable> books = bookStoreDBContext.BookTable.ToList();
                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Update particular book
        /// </summary>
        /// <param name="updateBookModel"> Book Info </param>
        /// <param name="bookId"> Book Id </param>
        /// <returns> Updated Book Info </returns>
        public BookTable UpdateBooks(BookModel updateBookModel ,int bookId)
        {
            try
            {
                var book = bookStoreDBContext.BookTable.FirstOrDefault(b => b.BookId == bookId);
                if(book != null)
                {
                    book.BookName = updateBookModel.BookName;
                    book.AuthorName = updateBookModel.AuthorName;
                    book.DiscountPrice = updateBookModel.DiscountPrice;
                    book.OriginalPrice = updateBookModel.OriginalPrice;
                    book.BookDescription = updateBookModel.BookDescription;
                    book.BookImage = updateBookModel.BookImage;
                    book.BookQuantity = updateBookModel.BookQuantity;
                    bookStoreDBContext.SaveChanges();
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get a particular book
        /// </summary>
        /// <param name="bookId"> Book Id </param>
        /// <returns> Book Info for particular book </returns>
        public BookTable GetBookById(int bookId)
        {
            try
            {
                var book = bookStoreDBContext.BookTable.FirstOrDefault(b => b.BookId == bookId);
                if(book != null)
                {
                    return book;
                }
                else { return null; }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Delete particular book
        /// </summary>
        /// <param name="bookId"> Book Id </param>
        /// <returns> Boolean Value </returns>
        public bool DeleteBook(int bookId)
        {
            try
            {
                var book = bookStoreDBContext.BookTable.FirstOrDefault(b => b.BookId == bookId);
                if(book != null)
                {
                    bookStoreDBContext.Remove(book);
                    bookStoreDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

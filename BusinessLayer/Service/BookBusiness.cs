using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IBookRepo bookRepo;
        public BookBusiness(IBookRepo bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public BookTable AddBook(BookModel addBookModel)
        {
            try
            {
                return bookRepo.AddBook(addBookModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteBook(int bookId)
        {
            try
            {
                return bookRepo.DeleteBook(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BookTable> GetAllBooks()
        {
            try
            {
                return bookRepo.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookTable GetBookById(int bookId)
        {
            try
            {
                return bookRepo.GetBookById(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BookTable UpdateBooks(BookModel updateBookModel, int bookId)
        {
            try
            {
                return bookRepo.UpdateBooks(updateBookModel, bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

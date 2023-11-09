using CommonLayer.Model;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBusiness
    {
        BookTable AddBook(BookModel addBookModel);
        List<BookTable> GetAllBooks();
        BookTable UpdateBooks(BookModel updateBookModel, int bookId);
        BookTable GetBookById(int bookId);
        bool DeleteBook(int bookId);
    }
}

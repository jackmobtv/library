using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class BookAccessor : IBookAccessor
    {
        public void deactivateCopy(int copyId)
        {
            throw new NotImplementedException();
        }

        public void insertBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void insertCopy(Copy copy)
        {
            throw new NotImplementedException();
        }

        public List<Book> selectAllBooks()
        {
            List<Book> books = new List<Book>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_books", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book()
                    {
                        BookId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Genre = reader.GetString(3),
                        Author = reader.GetString(4),
                        Publisher = reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return books;
        }

        public Book selectBookById(int bookId)
        {
            throw new NotImplementedException();
        }

        public List<Copy> selectCopiesByBookId(int bookId)
        {
            List<Copy> copies = new List<Copy>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_copies_by_book_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BookID", SqlDbType.Int);
            cmd.Parameters[0].Value = bookId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    copies.Add(new Copy()
                    {
                        CopyId = reader.GetInt32(0),
                        BookId = reader.GetInt32(1),
                        Condition = reader.GetString(2),
                        Active = reader.GetBoolean(3)
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return copies;
        }

        public Copy selectCopyById(int copyId)
        {
            throw new NotImplementedException();
        }

        public void updateBook(Book book, Book oldBook)
        {
            throw new NotImplementedException();
        }

        public void updateCopy(Copy copy, Copy oldCopy)
        {
            throw new NotImplementedException();
        }
    }
}

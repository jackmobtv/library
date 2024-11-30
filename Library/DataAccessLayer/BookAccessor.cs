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
    }
}

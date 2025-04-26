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
        public void activateCopy(int copyId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_activate_copy", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);
            cmd.Parameters[0].Value = copyId;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void deactivateCopy(int copyId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_deactivate_copy", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);
            cmd.Parameters[0].Value = copyId;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Book> selectBookTable()
        {
            List<Book> books = new List<Book>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_book_table", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book()
                    {
                        BookID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        GenreId = reader.GetInt32(3),
                        PublisherId = reader.GetInt32(4)
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
            return books;
        }

        public void insertAuthor(string name, int bookId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_author", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BookID", SqlDbType.Int);

            cmd.Parameters[0].Value = name;
            cmd.Parameters[1].Value = bookId;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void insertBook(Book book, int genreId, int publisherId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_book", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GenreID", SqlDbType.Int);
            cmd.Parameters.Add("@PublisherID", SqlDbType.Int);

            cmd.Parameters[0].Value = book.Name;
            cmd.Parameters[1].Value = book.Description;
            cmd.Parameters[2].Value = genreId;
            cmd.Parameters[3].Value = publisherId;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void insertBookAuthor(int authorId, int bookId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_bookauthor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BookID", SqlDbType.Int); 
            cmd.Parameters.Add("@AuthorID", SqlDbType.Int);

            cmd.Parameters[0].Value = bookId;
            cmd.Parameters[1].Value = authorId;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void insertCopy(Copy copy)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_copy", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BookID", SqlDbType.Int);
            cmd.Parameters.Add("@Condition", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = copy.BookId;
            cmd.Parameters[1].Value = copy.Condition;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void insertGenre(Genre genre)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_genre", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = genre.Name;
            cmd.Parameters[1].Value = genre.Description;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void insertPublisher(string name)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_publisher", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = name;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Author> selectAllAuthors()
        {
            List<Author> authors = new List<Author>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_authors", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    authors.Add(new Author()
                    {
                        AuthorID = reader.GetInt32(0),
                        BookId = reader.GetInt32(1),
                        Name = reader.GetString(2)
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
            return authors;
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
                        BookID = reader.GetInt32(0),
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
            finally
            {
                conn.Close();
            }
            return books;
        }

        public List<Genre> selectAllGenres()
        {
            List<Genre> genres = new List<Genre>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_genres", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genres.Add(new Genre()
                    {
                        GenreID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
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

            return genres;
        }

        public List<Publisher> selectAllPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_all_publishers", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    publishers.Add(new Publisher()
                    {
                        PublisherID = reader.GetInt32(0),
                        Name = reader.GetString(1),
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

            return publishers;
        }

        public Book selectBookById(int bookId)
        {
            Book book = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_book_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BookID", SqlDbType.Int);

            cmd.Parameters[0].Value = bookId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    book = new Book()
                    {
                        BookID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Genre = reader.GetString(3),
                        Author = reader.GetString(4),
                        Publisher = reader.GetString(5),
                        GenreId = reader.GetInt32(6),
                        AuthorId = reader.GetInt32(7),
                        PublisherId = reader.GetInt32(8)
                    };
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

            return book;
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
                        CopyID = reader.GetInt32(0),
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
            Copy copy = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_copy_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);
            cmd.Parameters[0].Value = copyId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    copy = new Copy()
                    {
                        CopyID = reader.GetInt32(0),
                        BookId = reader.GetInt32(1),
                        Condition = reader.GetString(2),
                        Active = reader.GetBoolean(3)
                    };
                }
                else
                {
                    throw new ArgumentException("Copy Not Found");
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

            return copy;
        }

        public void updateAuthor(int authorId, int bookId)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_author", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BookID", SqlDbType.Int);
            cmd.Parameters.Add("@AuthorID", SqlDbType.Int);

            cmd.Parameters[0].Value = bookId;
            cmd.Parameters[1].Value = authorId;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateBook(Book book, Book oldBook, int genreId, int publisherId, int genreId_Old, int publisherId_Old)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_book", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BookID", SqlDbType.Int);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@GenreID", SqlDbType.Int);
            cmd.Parameters.Add("@PublisherID", SqlDbType.Int);
            cmd.Parameters.Add("@Name_Old", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@Description_Old", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@GenreID_Old", SqlDbType.Int);
            cmd.Parameters.Add("@PublisherID_Old", SqlDbType.Int);

            cmd.Parameters[0].Value = book.BookID;
            cmd.Parameters[1].Value = book.Name;
            cmd.Parameters[2].Value = book.Description;
            cmd.Parameters[3].Value = genreId;
            cmd.Parameters[4].Value = publisherId;
            cmd.Parameters[5].Value = oldBook.Name;
            cmd.Parameters[6].Value = oldBook.Description;
            cmd.Parameters[7].Value = genreId_Old;
            cmd.Parameters[8].Value = publisherId_Old;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateCopy(Copy copy, Copy oldCopy)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_update_copy", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);
            cmd.Parameters.Add("@Condition", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Condition_Old", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = copy.CopyID;
            cmd.Parameters[1].Value = copy.Condition;
            cmd.Parameters[2].Value = oldCopy.Condition;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public CopyVM selectCopyVMById(int copyId)
        {
            CopyVM copy = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_copy_vm_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);
            cmd.Parameters[0].Value = copyId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    copy = new CopyVM()
                    {
                        CopyID = reader.GetInt32(0),
                        BookId = reader.GetInt32(1),
                        Condition = reader.GetString(2),
                        Active = reader.GetBoolean(3),
                        Name = reader.GetString(4)
                    };
                }
                else
                {
                    throw new ArgumentException("Copy Not Found");
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

            return copy;
        }
    }
}

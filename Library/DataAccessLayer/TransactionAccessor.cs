using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class TransactionAccessor : ITransactionAccessor
    {
        public void checkinBook(Transaction transaction)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_checkin_book", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);

            cmd.Parameters[0].Value = transaction.CopyId;

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

        public void checkoutBook(Transaction transaction)
        {
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_checkout_book", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CopyID", SqlDbType.Int);
            cmd.Parameters.Add("@TransactionID", SqlDbType.Int);

            cmd.Parameters[0].Value = transaction.CopyId;
            cmd.Parameters[1].Value = transaction.TransactionId;

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

        public int insertTransaction(int userId, string transactionType)
        {
            int id;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_transaction", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@TransactionType", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = userId;
            cmd.Parameters[1].Value = transactionType;

            try
            {
                conn.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return id;
        }

        public List<CopyVM> selectCheckedOutCopies(int userId)
        {
            List<CopyVM> copies = new List<CopyVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_checked_out_books_by_user_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters[0].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    copies.Add(new CopyVM()
                    {
                        CopyId = reader.GetInt32(0),
                        BookId = reader.GetInt32(1),
                        Condition = reader.GetString(2),
                        Name = reader.GetString(3),
                        Active = true
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

        public List<CopyVM> selectCopiesByTransactionId(int transactionId)
        {
            List<CopyVM> copies = new List<CopyVM>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_copies_by_transaction_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TransactionID", SqlDbType.Int);

            cmd.Parameters[0].Value = transactionId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    copies.Add(new CopyVM()
                    {
                        CopyId= reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Condition = reader.GetString(2)
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

        public List<Transaction> selectTransactionsByUserId(int userId)
        {
            List<Transaction> transactions = new List<Transaction>();

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_transactions_by_user_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters[0].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transactions.Add(new Transaction()
                    {
                        TransactionId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TransactionType = reader.GetString(2),
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

            return transactions;
        }
    }
}

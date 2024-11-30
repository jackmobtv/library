using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public void insertUser(string firstName, string lastName, string email, string passwordHash)
        {
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_insert_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64);

            cmd.Parameters[0].Value = firstName;
            cmd.Parameters[1].Value = lastName;
            cmd.Parameters[2].Value = email;
            cmd.Parameters[3].Value = passwordHash;

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

        public void updateUser(string firstName, string lastName, string old_email, string new_email, string old_passwordHash, string new_passwordHash)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_update_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Old_Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64);

            cmd.Parameters[0].Value = firstName;
            cmd.Parameters[1].Value = lastName;
            cmd.Parameters[2].Value = old_email;
            cmd.Parameters[3].Value = new_email;
            cmd.Parameters[4].Value = new_passwordHash;

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            if (result == 0)
            {
                throw new ArgumentException("Update Failed");
            }
        }

        public List<string> selectRolesByUserId(int userId)
        {
            List<string> roles = new List<string>();

            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand("sp_select_roles_by_user_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(reader.GetString(0));
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        public UserVM selectUserByEmail(string email)
        {
            UserVM user = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_user_by_email", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserVM
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = email,
                        Active = reader.GetBoolean(4)
                    };
                }
                else
                {
                    throw new ArgumentException("User Not Found");
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

            return user;
        }

        public UserVM selectUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            UserVM user = null;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_select_user_by_email_and_password", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char);
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new UserVM
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = email,
                        Active = reader.GetBoolean(4)
                    };
                }
                else
                {
                    throw new ArgumentException("Bad Email Or Password");
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

            return user;
        }

        public List<UserVM> selectAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}

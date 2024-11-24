using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    internal class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=localhost;Initial Catalog=library;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}

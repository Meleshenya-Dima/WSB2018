using System.Data.SqlClient;

namespace WorldSkills.ViewModel
{
    public static class WorkWithDatabase
    {
        private static readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\meles\Desktop\WorldSkills\WorldSkills\Resources\Database\WorldSkills.mdf;Integrated Security=True";

        private static SqlConnection _sqlConnection;

        private static SqlCommand _sqlCommand;

        public static SqlCommand SqlCommand
        {
            get
            {
                return _sqlCommand;
            }
        }

        public static SqlConnection SqlConnection
        {
            get
            {
                if (_sqlConnection == null)
                {
                    _sqlConnection = new SqlConnection(_connectionString);
                }
                return _sqlConnection;
            }
        }

        public static void OpenConnection()
        {
            SqlConnection.Open();
        }

        public static void CloseConnection()
        {
            _sqlConnection.Close();
        }

        public static void SetSqlCommand(string command)
        {
            _sqlCommand = new SqlCommand(command, SqlConnection);
        }
    }
}
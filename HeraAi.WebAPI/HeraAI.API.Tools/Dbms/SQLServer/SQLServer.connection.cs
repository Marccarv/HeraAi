using System.Data;
using System.Data.SqlClient;


namespace HeraAI.API.Tools.Dbms
{

    public partial class SQLServer
    {

        /// <summary>
        /// This method establishes connection with database
        /// </summary>
        public void OpenConnection()
        {

            // CREATE OBJECT
            if (sqlConnection == null)
                sqlConnection = new SqlConnection(connectionString);

            // OPEN CONNECTION
            sqlConnection.Open();

        }


        /// <summary>
        /// Use this method to close connection to database
        /// </summary>
        public void CloseConnection()
        {

            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                sqlConnection = null;
            }

        }

    }

}

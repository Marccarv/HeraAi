using System.Data.SqlClient;
using HeraAI.API.Exceptions;


namespace HeraAI.API.Tools.Dbms
{

    public partial class SQLServer
    {

        // ENUMS
        public enum SQLConnectionModes { auto, manual }


        // READONLY VARIABLES
        private readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        // VARIABLES
        string connectionString;
        SqlConnection sqlConnection;
        SqlTransaction sqlTransaction;
        SQLConnectionModes openCloseConnectionMode;


        /// <summary>
        /// SQLServer constructor
        /// </summary>
        /// <param name="connectionString">connection string to connect to database</param>
        /// <param name="sqlConnectionMode">sql connection mode to be used on the connection with database</param>
        public SQLServer(string connectionString, SQLConnectionModes sqlConnectionMode)
        {
            this.connectionString = connectionString;
            this.openCloseConnectionMode = sqlConnectionMode;
        }


        ~SQLServer()
        {
            if (IsConnectionOpen())
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_DESTRUCTION_CONNECTION_STILL_OPEN);
        }

    }

}

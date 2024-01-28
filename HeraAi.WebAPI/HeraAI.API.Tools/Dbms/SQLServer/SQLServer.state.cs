using System.Data;


namespace HeraAI.API.Tools.Dbms
{

    public partial class SQLServer
    {

        /// <summary>
        /// Use this method if you want to know if a connection is stablished with sql server
        /// </summary>
        /// <returns>if true you can use the connection</returns>
        public bool IsConnectionOpen()
        {

            if (sqlConnection == null)
                return false;

            if (sqlConnection.State == ConnectionState.Broken || sqlConnection.State == ConnectionState.Closed)
                return false;

            return true;

        }


        /// <summary>
        /// Use this method to see if there is an active transation
        /// </summary>
        public bool IsTransactionAlive
        {
            get { return (sqlTransaction != null); }
        }

    }

}

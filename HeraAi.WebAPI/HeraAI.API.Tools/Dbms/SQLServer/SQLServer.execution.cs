using System.Data;
using System.Data.SqlClient;
using HeraAI.API.Exceptions;


namespace HeraAI.API.Tools.Dbms
{
    public partial class SQLServer
    {

        /// <summary>
        /// This methods executes a query or a stored procedure on database
        /// </summary>
        /// <param name="commandType">sql command type</param>
        /// <param name="commandText">sql command text (query or stored procedure name)</param>
        /// <param name="runUnderTransaction">this parameter indicates if commandText should be executed under a transaction scope</param>
        /// <returns>return number of affected lines</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, bool runUnderTransaction)
        {
            return ExecuteNonQuery(commandType, commandText, runUnderTransaction, null);
        }


        /// <summary>
        /// This methods executes a query or a stored procedure on database passing a list of sql parameters
        /// </summary>
        /// <param name="commandType">sql command type</param>
        /// <param name="commandText">sql command text (query or stored procedure name)</param>
        /// <param name="runUnderTransaction">this parameter indicates if commandText should be executed under a transaction scope</param>
        /// <param name="sqlParameters">a list of sql parameters to be sent to database</param>
        /// <returns>returns number of affected rows</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, bool runUnderTransaction, List<SqlParameter> sqlParameters)
        {

            // VARIABLES
            int affectedLines;
            SqlCommand sqlCommand;


            // VALIDATIONS
            if (!IsConnectionOpen())
            {
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.OpenConnection();
                else
                    throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_CONNECTIONS_NO_CONNECTION);
            }


            // VALIDATE IF EXECUTION MUST BE EXECUTED UNDER A TRANSACTION
            if (runUnderTransaction && sqlTransaction == null)
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_TRANSACTIONS_TRANSACTION_IS_MANDATORY);


            // PREPARE EXECUTION
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;


            // IF THERE IS ALREADY A EXISTING TRANSACTION, USE-IT
            if (sqlTransaction != null)
                sqlCommand.Transaction = sqlTransaction;


            // PREPARE COMMAND TO BE EXECUTED
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;


            // IF THERE IS PARAMETERS TO BE SENT, ADD THEM TO SQLCOMMAND.PARAMETERS
            if (sqlParameters != null)
            {
                foreach (SqlParameter p in sqlParameters)
                    sqlCommand.Parameters.Add(p);
            }


            try
            {
                // EXECUTE QUERY AND KEEP NUMBER OF AFFECTEDLINES
                affectedLines = sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                // DISPOSE OBJECT
                sqlCommand.Dispose();


                // CLOSE CONNECTION IF USING AUTO CONNECTION MODE
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();


                // GET THE PROPER ERROR MESSAGE TRANSLATED
                if (ex.Message.StartsWith("SQLSERVER_"))
                    throw new HeraAIExceptionError(currentNamespace, "sqldatabase", commandType == CommandType.StoredProcedure ? commandText : "((sql query))", Resources.Resources.SQLResources.ResourceManager.GetString(ex.Message) ?? ex.Message);
                else
                    throw new HeraAIExceptionError(currentNamespace, "sqldatabase", commandType == CommandType.StoredProcedure ? commandText : "((sql query))", Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);

            }
            catch (Exception ex)
            {

                // DISPOSE OBJECT
                sqlCommand.Dispose();


                // CLOSE CONNECTION IF USING AUTO CONNECTION MODE
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();


                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            
            }
            finally
            {

                // DISPOSE OBJECT
                sqlCommand.Dispose();

                // CLOSE CONNECTION IF USING AUTO CONNECTION MODE
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();

            }


            // return number of affected lines
            return affectedLines;

        }


        /// <summary>
        /// This methods executes a query or a stored procedure on database passing a list of sql parameters
        /// </summary>
        /// <param name="commandType">sql command type</param>
        /// <param name="commandText">sql command text (query or stored procedure name)</param>
        /// <param name="runUnderTransaction">this parameter indicates if commandText should be executed under a transaction scope</param>
        /// <returns>returned object by sql engine</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, bool runUnderTransaction)
        {
            return this.ExecuteScalar(commandType, commandText, runUnderTransaction, null);
        }


        /// <summary>
        /// This methods executes a query or a stored procedure on database passing a list of sql parameters
        /// </summary>
        /// <param name="commandType">sql command type</param>
        /// <param name="commandText">sql command text (query or stored procedure name)</param>
        /// <param name="runUnderTransaction">this parameter indicates if commandText should be executed under a transaction scope</param>
        /// <param name="sqlParameters">a list of sql parameters to be sent to database</param>
        /// <returns>returned object by sql engine</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, bool runUnderTransaction, List<SqlParameter> sqlParameters)
        {

            // VARIABLES
            object obj;
            SqlCommand sqlCommand;


            // VALIDATIONS
            if (!IsConnectionOpen())
            {
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.OpenConnection();
                else
                    throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_CONNECTIONS_NO_CONNECTION);
            }


            // VALIDATE IF EXECUTION MUST BE EXECUTED UNDER A TRANSACTION
            if (runUnderTransaction && sqlTransaction == null)
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_TRANSACTIONS_TRANSACTION_IS_MANDATORY);


            // PREPARE EXECUTION
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;


            // IF THERE IS ALREADY A EXISTING TRANSACTION, USE-IT
            if (sqlTransaction != null)
                sqlCommand.Transaction = sqlTransaction;


            // PREPARE EXECUTION
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;


            // IF THERE IS PARAMETERS TO BE SENT, ADD THEM TO SQLCOMMAND.PARAMETERS
            if (sqlParameters != null)
            {
                foreach (SqlParameter p in sqlParameters)
                    sqlCommand.Parameters.Add(p);
            }


            try
            {
                // EXECUTE QUERY AND KEEP THE RETURNED OBJECT
                obj = sqlCommand.ExecuteScalar();
            }
            catch (SqlException ex)
            {

                // DISPOSE OBJECT
                sqlCommand.Dispose();


                // CLOSE CONNECTION IF USING AUTO CONNECTION MODE
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();


                throw new DbmsErrorException(currentNamespace, "sqldatabase", commandType == CommandType.StoredProcedure ? commandText : "((sql query))", Resources.Resources.SQLResources.SQLSERVER_PREFIX_INTERNAL_ERROR + ' ' + ex.Message);
            
            }
            catch (Exception ex)
            {

                // DISPOSE OBJECT
                sqlCommand.Dispose();


                // CLOSE CONNECTION IF USING AUTO CONNECTION MODE
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();

                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            
            }
            finally
            {

                // DISPOSE OBJECT
                sqlCommand.Dispose();

                // CLOSE CONNECTION IF USING AUTO CONNECTION MODE
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();

            }

            // RETURN OBJECT RETURNED BY SQL ENGINE
            return obj;

        }


        /// <summary>
        /// This methods executes a query or a stored procedure on database passing a list of sql parameters
        /// It is used forwardonly for a faster reading process
        /// </summary>
        /// <param name="commandType">sql command type</param>
        /// <param name="commandText">sql command text (query or stored procedure name)</param>
        /// <param name="sqlParameters">a list of sql parameters to be sent to database</param>
        /// <returns>a pointer for the reader object</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, List<SqlParameter> sqlParameters)
        {

            // VARIABLES
            SqlCommand sqlCommand;


            // VALIDATIONS
            if (!IsConnectionOpen())
            {
                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.OpenConnection();
                else
                    throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_CONNECTIONS_NO_CONNECTION);
            }


            // PREPARE EXECUTION
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;


            // IF THERE IS PARAMETERS TO BE SENT, ADD THEM TO SQLCOMMAND.PARAMETERS
            if (sqlParameters != null)
            {
                foreach (SqlParameter p in sqlParameters)
                    sqlCommand.Parameters.Add(p);
            }


            try
            {
                // RETURN READER TO CALLER
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException ex)
            {

                // DISPOSE OBJECTS
                sqlCommand.Dispose();

                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();


                // GET THE PROPER ERROR MESSAGE TRANSLATED
                if (ex.Message.StartsWith("SQLSERVER_"))
                    throw new HeraAIExceptionError(currentNamespace, "sqldatabase", commandType == CommandType.StoredProcedure ? commandText : "((sql query))", Resources.Resources.SQLResources.ResourceManager.GetString(ex.Message) ?? ex.Message);
                else
                    throw new HeraAIExceptionError(currentNamespace, "sqldatabase", commandType == CommandType.StoredProcedure ? commandText : "((sql query))", Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);

            }
            catch (Exception ex)
            {

                // DISPOSE OBJECTS
                sqlCommand.Dispose();


                if (openCloseConnectionMode == SQLConnectionModes.auto)
                    this.CloseConnection();


                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            
            }
            finally
            {
                // WHEN THE READER ENDS, CONNECTION IS AUTOMATICALLY CLOSED (COMMANDBEHAVIOR.CLOSECONNECTION).
            }

        }

    }

}

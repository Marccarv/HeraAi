using HeraAI.API.Exceptions;
using System.Data.SqlClient;


namespace HeraAI.API
{

    public class BaseEntity
    {

        // READONLY VARIABLES
        private readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        // INFO: Please use System.Reflection.MethodBase.GetCurrentMethod().Name to get name of current method. 


        // STATE VARIABLES
        protected Engine engine;


        /// <summary>
        /// This method executes stored procedures that returns a scalar value
        /// </summary>
        /// <param name="storedProcedureName">stored procedure to be executed</param>
        /// <param name="sqlParameters">list of necessary sqlParameters</param>
        /// <returns>integer with the scalar returned by database</returns>
        protected int DBGetScalar(string storedProcedureName, List<SqlParameter> sqlParameters)
        {

            // VARIABLES
            int count;
            bool setNewConnection;


            // FLAG THAT DEFINES IF A NEW CONNECTION MUST BE SETTLE TO DATABASE
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            try
            {

                // IF A NEW CONNECTION IS REQUIRED, OPEN-IT
                if (setNewConnection)
                    this.engine.SQLServer.OpenConnection();


                // GET COUNTER FROM DATABASE ENGINE
                count = (int)this.engine.SQLServer.ExecuteScalar(System.Data.CommandType.StoredProcedure, storedProcedureName, false, sqlParameters);


                // IF A NEW CONNECTION WAS REQUIRED, CLOSE-IT
                if (setNewConnection)
                    this.engine.SQLServer.CloseConnection();

            }
            catch (SqlException ex)
            {

                // CLOSE CONNECTION
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();


                throw new DbmsErrorException(currentNamespace, "sqldatabase", storedProcedureName, Resources.Resources.SQLResources.SQLSERVER_PREFIX_INTERNAL_ERROR + ' ' + ex.Message);

            }
            catch (HeraAIExceptionError)
            {

                // CLOSE CONNECTION
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();


                throw;

            }
            catch (Exception ex)
            {

                // CLOSE CONNECTION
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();


                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);

            }


            return count;

        }


        /// <summary>
        /// This method executes stored procedures and returns the number of affected rows
        /// </summary>
        /// <param name="storedProcedureName">stored procedure to be executed</param>
        /// <param name="sqlParameters">list of necessary sqlParameters</param>
        /// <returns>integer with the number of affected records on database</returns>
        protected virtual int DBExecuteNonQuery(string storedProcedureName, List<SqlParameter> sqlParameters)
        {

            // VARIABLES
            int recordsAffected;
            bool setNewConnection;


            // FLAG THAT DEFINES IF A NEW CONNECTION MUST BE SETTLE TO DATABASE
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            try
            {

                // IF A NEW CONNECTION IS REQUIRED, OPEN-IT AND INITIATE A NEW TRANSACTION
                if (setNewConnection)
                {
                    this.engine.SQLServer.OpenConnection();
                    this.engine.SQLServer.BeginTransaction();
                }


                // GET NUMBER OF AFFECTED RECORDS FROM DATABASE ENGINE
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, storedProcedureName, true, sqlParameters);


                // IF A NEW CONNECTION WAS REQUIRED, COMMIT THE TRANSACTION AND CLOSE-IT
                if (setNewConnection)
                {
                    this.engine.SQLServer.CommitTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

            }
            catch (SqlException ex)
            {

                // CLOSE CONNECTION
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }


                throw new DbmsErrorException(currentNamespace, "sqldatabase", storedProcedureName, Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);

            }
            catch (HeraAIExceptionError)
            {

                // CLOSE CONNECTION
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }


                throw;

            }
            catch (Exception ex)
            {

                // CLOSE CONNECTION
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }


                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);

            }

            // RETURN NUMBER OF AFFECTED RECORDS
            return recordsAffected;

        }

    }

}

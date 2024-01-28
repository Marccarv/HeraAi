using HeraAI.API.Exceptions;


namespace HeraAI.API.Tools.Dbms
{

    public partial class SQLServer
    {

        /// <summary>
        /// This method initiates a new transaction on database connection
        /// </summary>
        public void BeginTransaction()
        {

            // VALIDATIONS
            if (openCloseConnectionMode == SQLConnectionModes.auto)
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_TRANSACTIONS_CONNECTIONMODE_INCONSISTENCY);


            if (!this.IsConnectionOpen())
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_TRANSACTIONS_CONNECTION_CLOSED);


            // START TRANSACTION
            sqlTransaction = sqlConnection.BeginTransaction();

        }


        /// <summary>
        /// This method commits current transaction to database
        /// </summary>
        public void CommitTransaction()
        {

            // VALIDATIONS
            if (sqlTransaction == null)
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_TRANSACTIONS_COMMIT_MISSED_TRANSACTION);


            // COMMIT TRANSACTION
            sqlTransaction.Commit();
            sqlTransaction.Dispose();
            sqlTransaction = null;

        }


        /// <summary>
        /// This method rollsback the current transaction on database
        /// </summary>
        public void RollbackTransaction()
        {

            // VALIDATIONS
            if (sqlTransaction == null)
                throw new DbmsErrorException(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SQLResources.SQLSERVER_TRANSACTIONS_ROLLBACK_MISSED_TRANSACTION);


            // ROLLBACK TRANSACTION
            sqlTransaction.Rollback();
            sqlTransaction.Dispose();
            sqlTransaction = null;

        }

    }

}

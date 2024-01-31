using HeraAI.API.Enums;
using HeraAI.API.Tools;
using HeraAI.API.Models;
using HeraAI.API.Exceptions;
using System.Data.SqlClient;
using HeraAI.API.Models.Specs;

namespace HeraAI.API
{

    public partial class Users
    {

        public void Insert(User user, string userId, string customerId)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // initializing variables
            info = "";

            // invalidate dangerous words
            UserSpecs.InvalidateDangerousWords(ref user, GlobalVars.DangerousWords, GlobalVars.SymbolToInvalidateWords);

            // object validation
            if (!UserSpecs.IsValid(user, DataOperation.Insert, ref info))
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                        string.Format("{0} [{1}] {2};",
                        Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                        user.Id,
                        info));
            }

            // prepare the list of sql parameters
            sqlParameters = this.Serialize(user, userId, customerId, DataOperation.Insert);

            // flag that defines if a new connection must be settle to database
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            try
            {
                // if a new connection is required, open it and initiate a new transaction
                if (setNewConnection)
                {
                    this.engine.SQLServer.OpenConnection();
                    this.engine.SQLServer.BeginTransaction();
                }

                // get number of affected records from database engine
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_INSERT, true, sqlParameters);

                // get the returned value
                user.Id = (string)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newId")).Value;

                // prevent wrong number of affected records
                if (recordsAffected == 0)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_0);
                else if (recordsAffected > 1)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_MORE_THAN);

                // if a new connection was required, commit the transaction and close it
                if (setNewConnection)
                {
                    this.engine.SQLServer.CommitTransaction();
                    this.engine.SQLServer.CloseConnection();
                }
            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", SP_INSERT, Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (HeraAIExceptionWarning)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }
        }

        

        public void Register(User user)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // initializing variables
            info = "";

            // invalidate dangerous words
            UserSpecs.InvalidateDangerousWords(ref user, GlobalVars.DangerousWords, GlobalVars.SymbolToInvalidateWords);

            // object validation
            if (!UserSpecs.IsValid(user, DataOperation.Insert, ref info))
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                        string.Format("{0} [{1}] {2};",
                        Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                        user.Id,
                        info));
            }

            // prepare the list of sql parameters
            sqlParameters = this.Serialize(user, "", "", DataOperation.Insert);

            // flag that defines if a new connection must be settle to database
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            try
            {
                // if a new connection is required, open it and initiate a new transaction
                if (setNewConnection)
                {
                    this.engine.SQLServer.OpenConnection();
                    this.engine.SQLServer.BeginTransaction();
                }

                // get number of affected records from database engine
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_INSERT03, true, sqlParameters);

                // get the returned value
                user.Id = (string)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newId")).Value;

                //if values are null, present default values
                if (user.CountryId == null)
                    user.CountryId = "COUN01";
                if (user.Language == null)
                    user.Language = "en-US";
            

                // prevent wrong number of affected records
                if (recordsAffected == 0)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_0);
                else if (recordsAffected > 1)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_MORE_THAN);

                // if a new connection was required, commit the transaction and close it
                if (setNewConnection)
                {
                    this.engine.SQLServer.CommitTransaction();
                    this.engine.SQLServer.CloseConnection();
                }
            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", SP_INSERT, Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (HeraAIExceptionWarning)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }
        }


        public void Update(User user, string userId, string customerId)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // initializing variables
            info = "";

            // invalidate dangerous words
            UserSpecs.InvalidateDangerousWords(ref user, GlobalVars.DangerousWords, GlobalVars.SymbolToInvalidateWords);

            // object validation
            if (!UserSpecs.IsValid(user, DataOperation.Update, ref info))
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                        string.Format("{0} [{1}] {2};",
                        Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                        user.Id,
                        info));
            }


            // prepare the list of sql parameters
            sqlParameters = this.Serialize(user, userId, customerId, DataOperation.Update);

            // flag that defines if a new connection must be settle to database
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            try
            {
                // if a new connection is required, open it and initiate a new transaction
                if (setNewConnection)
                {
                    this.engine.SQLServer.OpenConnection();
                    this.engine.SQLServer.BeginTransaction();
                }

                // get number of affected records from database engine
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_UPDATE, true, sqlParameters);

                // get the returned value
                user.LastUpdate = (DateTime)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newLastUpdateOtp")).Value;


                // prevent wrong number of affected records
                if (recordsAffected == 0)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_0);
                else if (recordsAffected > 1)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_MORE_THAN);

                // if a new connection was required, commit the transaction and close it
                if (setNewConnection)
                {
                    this.engine.SQLServer.CommitTransaction();
                    this.engine.SQLServer.CloseConnection();
                }
            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", SP_UPDATE, Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (HeraAIExceptionWarning)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }
        }



        /// <summary>
        /// Deletes the received object on database
        /// </summary>
        /// <param name="user">Object to be deleted</param>
        public void Delete(User user, string userId, string customerId)
        {
            // Variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // Initializing variables
            info = "";

            // Object validation
            if (!UserSpecs.IsValid(user, DataOperation.Delete, ref info))
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                        string.Format("{0} [{1}] {2};",
                        Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                        user.Id,
                        info));
            }

            // Prepare the list of sql parameters
            sqlParameters = this.Serialize(user, userId, customerId, DataOperation.Delete);

            // Flag that defines if a new connection must be settle to database
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            try
            {
                // If a new connection is required, open it and initiate a new transaction
                if (setNewConnection)
                {
                    this.engine.SQLServer.OpenConnection();
                    this.engine.SQLServer.BeginTransaction();
                }

                // Get number of affected records from database engine
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_DELETE, true, sqlParameters);

                // prevent wrong number of affected records
                if (recordsAffected == 0)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_0);
                else if (recordsAffected > 1)
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_MORE_THAN);

                // If a new connection was required, commit the transaction and close it
                if (setNewConnection)
                {
                    this.engine.SQLServer.CommitTransaction();
                    this.engine.SQLServer.CloseConnection();
                }
            }
            catch (SqlException ex)
            {
                // Close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", SP_DELETE, Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // Close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (HeraAIExceptionWarning)
            {
                // Close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw;
            }
            catch (Exception ex)
            {
                // Close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                {
                    this.engine.SQLServer.RollbackTransaction();
                    this.engine.SQLServer.CloseConnection();
                }

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }
        }


        /// <summary>
        /// Evaluates if a record is present on database
        /// </summary>
        /// <param name="email">Email to filter</param>
        /// <param name="password">Password to filter</param>
        /// <param name="dataState">Data state to filter</param>
        /// <returns>Number of existing records that fulfill the filter params received</returns>
        public bool Exists(string email, string password, DataStates dataState)
        {

            // Perform a count
            if (this.Count(email, password, dataState) > 0)
                return true;

            // If count method return zero records, id does not exist or may be inactive.
            return false;
        }


        /// <summary>
        /// Evaluates if a record is present on database
        /// </summary>
        /// <param name="email">Email to filter</param>
        /// <param name="dataState">Data state to filter</param>
        /// <returns>Number of existing records that fulfill the filter params received</returns>
        public bool Exists(string email, DataStates dataState)
        {

            // Perform a count
            if (this.Count(email, dataState) > 0)
                return true;

            // If count method return zero records, id does not exist or may be inactive.
            return false;
        }


        /// <summary>
        /// Return the number of existing records for the received params
        /// </summary>
        /// <param name="email">Email to filter</param>
        /// <param name="dataState">Data state to filter</param>
        /// <returns>Number of existing records that fulfill the filter params received</returns>
        public int Count(string email, DataStates dataState)
        {
            // Variables
            string info;
            string whereClause;
            List<SqlParameter> sqlParameters;

            // Initializing variables
            info = "";

            // Data validation (email)
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // Sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false )
            };

            // Execute and return count
            return DBGetScalar(SP_COUNT_USERS02, sqlParameters);
        }


        /// <summary>
        /// Return the number of existing records for the received params
        /// </summary>
        /// <param name="email">Email to filter</param>
        /// <param name="password">Password to filter</param>
        /// <param name="dataState">Data state to filter</param>
        /// <returns>Number of existing records that fulfill the filter params received</returns>
        public int Count(string email, string password, DataStates dataState)
        {
            // Variables
            string info;
            List<SqlParameter> sqlParameters;

            // Initializing variables
            info = "";

            // Data validation (email)
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // Data validation (password)
            if (!UserSpecs.Password.IsValid(password, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    password,
                    info));

            // Sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", password),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false )
            };


            // Execute and return count
            return DBGetScalar(SP_COUNT_USERS01, sqlParameters);
        }

    }

}

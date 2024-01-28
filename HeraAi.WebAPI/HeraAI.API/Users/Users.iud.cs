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

        public void InsertUserWithdrawal(Withdrawal withdrawal, string userId, string customerId)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // initializing variables
            info = "";

            // prepare the list of sql parameters
            sqlParameters = this.SerializeUserWithdrawal(withdrawal, userId, customerId, DataOperation.Insert);

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
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_SELECT_GET_DRIVERWITHDRAWAL01, true, sqlParameters);

                //// prevent wrong number of affected records
                //if (recordsAffected == 0)
                //    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_0);
                //else if (recordsAffected > 1)
                //    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNEXPECTED_NUMBER_OF_AFFECTED_RECORDS_MORE_THAN);

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
                if (user.CurrencyId == null)
                    user.CurrencyId = "EUR";
                if (user.Language == null)
                    user.Language = "en-US";
                if (user.UnitId == null)
                    user.UnitId = "UNIT01";

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


        public void UpdateMobile(User user, string userId)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // initializing variables
            info = "";

            // invalidate dangerous words
            //UserSpecs.InvalidateDangerousWords(ref user, GlobalVars.DangerousWords, GlobalVars.SymbolToInvalidateWords);

            user.LastUserId = userId;

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
            sqlParameters = this.SerializeMobile(user, userId, DataOperation.Update);

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
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_UPDATE03, true, sqlParameters);

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


        public void UpdateUser(User user, string userId)
        {

            // Variables
            string info;
            int recordsAffected;
            bool setNewConnection;
            List<SqlParameter> sqlParameters;


            // Initializing variables
            info = "";


            // Invalidate dangerous words
            UserSpecs.InvalidateDangerousWords(ref user, GlobalVars.DangerousWords, GlobalVars.SymbolToInvalidateWords);


            // Object validation
            // if (!UserSpecs.IsValid(user, DataOperation.Update, ref info))
            // {
            //     throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
            //             string.Format("{0} [{1}] {2};",
            //             Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
            //             user.Id,
            //             info));
            // }


            // Prepare the list of sql parameters
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@firstName", user.FirstName),
                new SqlParameter("@lastName", user.LastName),
                new SqlParameter("@countryId", user.CountryId),
                new SqlParameter("@language", user.Language),

                // new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                // new SqlParameter("@driver_Address1", user.Driver_Address1 != null ? user.Driver_Address1 : DBNull.Value),
                // new SqlParameter("@driver_Address2", user.Driver_Address2 != null ? user.Driver_Address2 : DBNull.Value),
                // new SqlParameter("@driver_PostalCode", user.Driver_PostalCode != null ? user.Driver_PostalCode : DBNull.Value),
                // new SqlParameter("@driver_PostalLocation", user.Driver_PostalLocation != null ? user.Driver_PostalLocation : DBNull.Value),
                // new SqlParameter("@driver_Vat", user.Driver_VAT != null ? user.Driver_VAT : DBNull.Value),
                
                new SqlParameter("@lastUpdate", user.LastUpdate),
                new SqlParameter("@lastUserId", userId),
                new SqlParameter
                {
                    ParameterName = "@newLastUpdateOtp",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.DateTime
                }
            };


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
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_UPDATE_USER, true, sqlParameters);


                // Get the returned value
                user.LastUpdate = (DateTime)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newLastUpdateOtp")).Value;


                // Prevent wrong number of affected records
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

                throw new HeraAIExceptionError
                (
                    currentNamespace,
                    "sqldatabase",
                    SP_UPDATE,
                    Resources.Resources.SQLResources.SQLSERVER_PREFIX_UNKNOWN_ERROR + ' ' + ex.Message
                );

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

                throw new HeraAIExceptionError
                (
                    currentNamespace,
                    currentClassName,
                    System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message
                );

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


        public Guid AddDSN(string email, string password, string deviceName)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;
            Guid newDSN;


            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", Settings.Encrypt(password)),
                new SqlParameter("@deviceName", deviceName),
                new SqlParameter {
                    ParameterName = "@newDriverSerialNumber",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                },
            };


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
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_UPDATE_SERIALNUMBER01, true, sqlParameters);

                newDSN = (Guid)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newDriverSerialNumber")).Value != Guid.Empty ? (Guid)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newDriverSerialNumber")).Value : Guid.Empty;

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

            return newDSN;
        }


        public Guid UpdateDSN(string email, string password, string deviceName, int operation)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;
            Guid newDSN;


            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", Settings.Encrypt(password)),
                new SqlParameter("@deviceName", deviceName),
                new SqlParameter("@operation", operation),
                new SqlParameter {
                    ParameterName = "@newDriverSerialNumber",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                },
            };


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
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_UPDATE_SERIALNUMBER02, true, sqlParameters);

                newDSN = (Guid)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newDriverSerialNumber")).Value != Guid.Empty ? (Guid)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@newDriverSerialNumber")).Value : Guid.Empty;


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

            return newDSN;
        }


        public void UpdateNextGoal(string userId, decimal nextGoal)
        {
            // variables
            string info;
            List<SqlParameter> sqlParameters;
            int recordsAffected;
            bool setNewConnection;

            // initializing variables
            info = "";

            // prepare the list of sql parameters
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@driver_NextGoal", nextGoal)
            };

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
                recordsAffected = this.engine.SQLServer.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, SP_UPDATENEXTGOAL, true, sqlParameters);

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
    
    }

}

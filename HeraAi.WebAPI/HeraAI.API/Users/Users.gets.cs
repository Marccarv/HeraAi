using HeraAI.API.Enums;
using HeraAI.API.Tools;
using HeraAI.API.Models;
using HeraAI.API.Exceptions;
using System.Data.SqlClient;
using HeraAI.API.Models.Specs;
using HeraAI.API.Tools.Validations;


namespace HeraAI.API
{

    public partial class Users
    {


        public List<User> GetWeighingAuthorizationInUsers(string userId, string customerId, string weighingAuthorizationId, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {

            // VARIABLES
            List<SqlParameter> sqlParameters;
            List<User> users;


            // SQL parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@weighingAuthorizationId", weighingAuthorizationId),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
            };


            // Get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_DETAILS05, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);


            // If no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count < 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);


            // If code arrives here thats because object list has only one record
            return users;

        }


        public List<User> GetWeighingAuthorizationNotInUsers(string userId, string customerId, string weighingAuthorizationId, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {

            // Variables
            List<SqlParameter> sqlParameters;
            List<User> users;


            // SQL parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@userId", userId),
                new SqlParameter("@weighingAuthorizationId", weighingAuthorizationId),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
            };


            // Get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_DETAILS06, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);


            // If no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count < 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);


            // If code arrives here thats because object list has only one record
            return users;

        }


        public User Get(string userId, string customerId, string id, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            id = Strings.InvalidateDangerousWords(id, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Id.IsValid(id, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    id,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@id", id),
                new SqlParameter("@countryId", DBNull.Value),
                new SqlParameter("@unitId", DBNull.Value),
                new SqlParameter("@currencyId", DBNull.Value),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_DETAILS01, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User GetMobile(string userId, string id, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            id = Strings.InvalidateDangerousWords(id, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Id.IsValid(id, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    id,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@id", id),
                new SqlParameter("@countryId", DBNull.Value),
                new SqlParameter("@unitId", DBNull.Value),
                new SqlParameter("@currencyId", DBNull.Value),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.GetMobile(userId, SP_SELECT_GET_DETAILS03, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User GetInternalUser(string userId, string customerId, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId)
            };


            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_INTERNAL, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User GetDeviceNames(string email, string password)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", Settings.Encrypt(password))
            };


            // get objects from database
            users = this.Get(SP_SELECT_GET_DEVICENAMES, sqlParameters);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User GetInternalUserMobile(string userId, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId)
            };


            // get objects from database
            users = this.GetMobile(userId, SP_SELECT_GET_INTERNAL, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public List<User> GetDefaultUsers(string userId, string customerId, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;
            List<SqlParameter> sqlParameters;
            List<User> users;

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId)
            };

            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_DEFAULT_USERS, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // return the list of objects
            return users;
        }


        public List<User> GetDefaultUsersMobile(string userId, DataStates dataState, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;
            List<SqlParameter> sqlParameters;
            List<User> users;

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId)
            };

            // get objects from database
            users = this.GetMobile(userId, SP_SELECT_GET_DEFAULT_USERS03, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // return the list of objects
            return users;
        }


        public User Get(string userId, string customerId, string email, DataStates dataState)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            email = Strings.InvalidateDangerousWords(email, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@email", email),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_USERS_AUTHENTICATION02, sqlParameters, 0, 0, 0);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User Get(string email, string password, DataStates dataState)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            email = Strings.InvalidateDangerousWords(email, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", password),
                new SqlParameter {
                    ParameterName = "@newDriverSerialNumber",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                },
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.Get("", "", SP_SELECT_USERS_AUTHENTICATION03, sqlParameters, 0, 0, 0);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User GetMobile(string email, string password, DataStates dataState)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            email = Strings.InvalidateDangerousWords(email, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", Settings.Encrypt(password)),
                new SqlParameter {
                    ParameterName = "@newDriverSerialNumber",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                },
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.GetMobile("", SP_SELECT_USERS_AUTHENTICATION03, sqlParameters, 1, 1, 1);

            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User Get02(string email, string password, DataStates dataState)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            email = Strings.InvalidateDangerousWords(email, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", Settings.Encrypt(password)),
                new SqlParameter {
                    ParameterName = "@newDriverSerialNumberMessage",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 150
                },
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.Get("", "", SP_SELECT_USERS_AUTHENTICATION03, sqlParameters, 0, 0, 0);


            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        public User Get(string email, string password, Guid serialNumber, DataStates dataState)
        {
            // variables
            List<SqlParameter> sqlParameters;
            List<User> users;
            string info = "";

            // invalidate dangerous words
            email = Strings.InvalidateDangerousWords(email, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Email.IsValid(email, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    email,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", Settings.Encrypt(password)),
                new SqlParameter("@driver_SerialNumber", serialNumber),
                new SqlParameter {
                    ParameterName = "@newDriverSerialNumberMessage",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Size = 150
                },
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", "{AU-ID}"),
                new SqlParameter("@_pageNumber", 1),
                new SqlParameter("@_pageSize", 1)
            };


            // get objects from database
            users = this.Get("", "", SP_SELECT_USERS_AUTHENTICATION04, sqlParameters, 0, 0, 0);


            // if no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            // if code arrives here thats because object list has only one record
            return users[0];
        }


        


        public List<User> GetSortedList(string userId, string customerId, string order, DataStates dataState, int pageNumber, int pageSize, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;
            List<SqlParameter> sqlParameters;
            List<User> users;

            // pagesize validation
            if (pageSize > GlobalVars.PageSizeMax)
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_PAGE_SIZE_EXCEEDED);
            }

            // invalidate dangerous words
            if (order != null)
            {
                order = Strings.InvalidateDangerousWords(order, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

                Strings.TrimOrder(order, ORDER_OPTIONS);
            }

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@sentUserId", userId),
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", order == null ? DBNull.Value : order),
                new SqlParameter("@_pageNumber", pageNumber),
                new SqlParameter("@_pageSize", pageSize)
            };

            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_SUMMARY02, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // return the list of objects
            return users;
        }


        public (List<User>, int) Get(string userId, string customerId, string search, DataStates dataState, string order, int pageNumber, int pageSize, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;
            List<SqlParameter> sqlParameters;
            List<User> users;

            // pagesize validation
            if (pageSize > GlobalVars.PageSizeMax)
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_PAGE_SIZE_EXCEEDED);
            }

            // invalidate dangerous words
            search = Strings.InvalidateDangerousWords(search, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            Strings.TrimOrder(order, ORDER_OPTIONS);

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@search", search),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", order),
                new SqlParameter("@_pageNumber", pageNumber),
                new SqlParameter("@_pageSize", pageSize),
                new SqlParameter {
                    ParameterName = "@totalPages",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.Int
                }
            };

            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_SEARCH01, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // return the list of objects
            int totalPages = (int)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@totalPages")).Value;

            return (users, totalPages);
        }


        public (List<User>, int) Get(string userId, string customerId, DataStates dataState, string order, int pageNumber, int pageSize, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;
            List<SqlParameter> sqlParameters;
            List<User> users;

            // pagesize validation
            if (pageSize > GlobalVars.PageSizeMax)
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_PAGE_SIZE_EXCEEDED);
            }

            Strings.TrimOrder(order, ORDER_OPTIONS);

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", order),
                new SqlParameter("@_pageNumber", pageNumber),
                new SqlParameter("@_pageSize", pageSize),
                new SqlParameter {
                    ParameterName = "@totalPages",
                    Direction = System.Data.ParameterDirection.Output,
                    SqlDbType = System.Data.SqlDbType.Int
                }
            };

            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_SUMMARY01, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // return the list of objects
            int totalPages = (int)sqlParameters.FirstOrDefault(p => p.ParameterName.Equals("@totalPages")).Value;

            return (users, totalPages);
        }

        public List<User> GetDrivers(string userId, string customerId, DataStates dataState, string order, int pageNumber, int pageSize, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;
            List<SqlParameter> sqlParameters;
            List<User> users;

            // pagesize validation
            if (pageSize > GlobalVars.PageSizeMax)
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_PAGE_SIZE_EXCEEDED);
            }

            Strings.TrimOrder(order, ORDER_OPTIONS);

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@userCustomerId", customerId),
                new SqlParameter("@inactive", dataState.Equals(DataStates.All) ? DBNull.Value : dataState.Equals(DataStates.Inactive) ? true : false ),
                new SqlParameter("@_orderColumns", order),
                new SqlParameter("@_pageNumber", pageNumber),
                new SqlParameter("@_pageSize", pageSize)
            };

            // get objects from database
            users = this.Get(userId, customerId, SP_SELECT_GET_DRIVERS01, sqlParameters, loadLevelCountry, loadLevelCurrency, loadLevelUnit);

            // return the list of objects
            return users;
        }


        private List<User> Get(string userId, string customerId, string storedProcedureName, List<SqlParameter> sqlParameters, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            List<User> users;
            SqlDataReader sqlDataReader;
            bool setNewConnection;

            // initializing variables
            users = new List<User>();
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            // get records from database and deserialize them
            try
            {
                // if a new connection is required, open it
                if (setNewConnection)
                    this.engine.SQLServer.OpenConnection();

                // get sqlDataReader pointer to database
                sqlDataReader = this.engine.SQLServer.ExecuteReader(System.Data.CommandType.StoredProcedure, storedProcedureName, sqlParameters);

                // if no records were returned from database return a clear list
                if (!sqlDataReader.HasRows)
                {
                    // search for a next resultset for errors
                    while (sqlDataReader.NextResult()) ;

                    // free resources of the sqlDataReader
                    sqlDataReader.Dispose();
                    this.engine.SQLServer.CloseConnection();
                    return users;
                }

                // proceed to deserialization
                while (sqlDataReader.Read())
                    users.Add(this.Deserialize(ref sqlDataReader));

                // search for a next resultset for errors
                while (sqlDataReader.NextResult()) ;

                // free resources of the sqlDataReader
                sqlDataReader.Dispose();

                // if a new connection was required, close it
                if (setNewConnection)
                    this.engine.SQLServer.CloseConnection();

            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", storedProcedureName, Resources.Resources.SQLResources.SQLSERVER_PREFIX_INTERNAL_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }

            // return object list
            return users;
        }


        private List<User> GetMobile(string userId, string storedProcedureName, List<SqlParameter> sqlParameters, int loadLevelCountry, int loadLevelCurrency, int loadLevelUnit)
        {
            // variables
            List<User> users;
            SqlDataReader sqlDataReader;
            bool setNewConnection;

            // initializing variables
            users = new List<User>();
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            // get records from database and deserialize them
            try
            {
                // if a new connection is required, open it
                if (setNewConnection)
                    this.engine.SQLServer.OpenConnection();

                // get sqlDataReader pointer to database
                sqlDataReader = this.engine.SQLServer.ExecuteReader(System.Data.CommandType.StoredProcedure, storedProcedureName, sqlParameters);

                // if no records were returned from database return a clear list
                if (!sqlDataReader.HasRows)
                {
                    // search for a next resultset for errors
                    while (sqlDataReader.NextResult()) ;

                    // free resources of the sqlDataReader
                    sqlDataReader.Dispose();
                    this.engine.SQLServer.CloseConnection();
                    return users;
                }

                // proceed to deserialization
                while (sqlDataReader.Read())
                    users.Add(this.Deserialize(ref sqlDataReader));

                // search for a next resultset for errors
                while (sqlDataReader.NextResult()) ;

                // free resources of the sqlDataReader
                sqlDataReader.Dispose();

                // if a new connection was required, close it
                if (setNewConnection)
                    this.engine.SQLServer.CloseConnection();

            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", storedProcedureName, Resources.Resources.SQLResources.SQLSERVER_PREFIX_INTERNAL_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw;
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }




            // return object list
            return users;
        }


        private List<User> Get(string storedProcedureName, List<SqlParameter> sqlParameters)
        {
            // variables
            List<User> users;
            SqlDataReader sqlDataReader;
            bool setNewConnection;

            // initializing variables
            users = new List<User>();
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            // get records from database and deserialize them
            try
            {
                // if a new connection is required, open it
                if (setNewConnection)
                    this.engine.SQLServer.OpenConnection();

                // get sqlDataReader pointer to database
                sqlDataReader = this.engine.SQLServer.ExecuteReader(System.Data.CommandType.StoredProcedure, storedProcedureName, sqlParameters);

                // if no records were returned from database return a clear list
                if (!sqlDataReader.HasRows)
                {
                    // search for a next resultset for errors
                    while (sqlDataReader.NextResult()) ;

                    // free resources of the sqlDataReader
                    sqlDataReader.Dispose();
                    this.engine.SQLServer.CloseConnection();
                    return users;
                }

                // proceed to deserialization
                while (sqlDataReader.Read())
                    users.Add(this.Deserialize(ref sqlDataReader));

                // search for a next resultset for errors
                while (sqlDataReader.NextResult()) ;

                // free resources of the sqlDataReader
                sqlDataReader.Dispose();

                // if a new connection was required, close it
                if (setNewConnection)
                    this.engine.SQLServer.CloseConnection();

            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", storedProcedureName, Resources.Resources.SQLResources.SQLSERVER_PREFIX_INTERNAL_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw;
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }

            // return object list
            return users;
        }


        public User GetInformations(string userId)
        {

            // Variables
            string info = "";
            List<User> users;
            List<SqlParameter> sqlParameters;


            // Data validation
            if (!UserSpecs.Id.IsValid(userId, ref info))
                throw new HeraAIExceptionError
                (
                    currentNamespace,
                    currentClassName,
                    System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    userId,
                    info
                )
            );


            // SQL parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId),
            };


            // Get objects from database
            users = this.Get(userId, null, SP_SELECT_GET_DETAILS04, sqlParameters, 0, 0, 0);


            // If no records were returned from database throw an error
            if (users.Count == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (users.Count > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);


            // If code arrives here thats because object list has only one record
            return users[0];

        }




        private int Count(string storedProcedureName, List<SqlParameter> sqlParameters)
        {
            // variables
            int userCount = 0;
            bool setNewConnection;

            // initializing variables
            setNewConnection = !this.engine.SQLServer.IsConnectionOpen();

            // get records from database and deserialize them
            try
            {
                // if a new connection is required, open it
                if (setNewConnection)
                    this.engine.SQLServer.OpenConnection();

                // get sqlDataReader pointer to database
                object result = this.engine.SQLServer.ExecuteScalar(System.Data.CommandType.StoredProcedure, storedProcedureName, false, sqlParameters);

                userCount = int.Parse(result.ToString());


                // if a new connection was required, close it
                if (setNewConnection)
                    this.engine.SQLServer.CloseConnection();

            }
            catch (SqlException ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, "sqldatabase", storedProcedureName, Resources.Resources.SQLResources.SQLSERVER_PREFIX_INTERNAL_ERROR + ' ' + ex.Message);
            }
            catch (HeraAIExceptionError)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw;
            }
            catch (Exception ex)
            {
                // close connection
                if (this.engine.SQLServer.IsConnectionOpen())
                    this.engine.SQLServer.CloseConnection();

                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_EXCEPTION_PREFIX_ERROR + ' ' + ex.Message);
            }

            // return count
            return userCount;
        }



        public bool DoesUserExists(string userId)
        {

            // variables
            string info = "";
            int userCount;
            List<SqlParameter> sqlParameters;

            // invalidate dangerous words
            userId = Strings.InvalidateDangerousWords(userId, GlobalVars.SymbolToInvalidateWords, GlobalVars.DangerousWords);

            // data validation
            if (!UserSpecs.Id.IsValid(userId, ref info))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(),
                    string.Format("{0} [{1}] {2};",
                    Resources.Resources.SpecsResources.SPECS_PREFIX_INVALID_VALUE,
                    userId,
                    info));

            // sql parameter list preparation
            sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@userId", userId)
            };


            // get objects from database
            userCount = this.Count(SP_COUNT_USERS03, sqlParameters);

            // if no records were returned from database throw an error
            if (userCount == 0)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_RECORDS_RETURNED_FROM_DATABASE);
            else if (userCount > 1)
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_INVALID_NUMBER_OF_RECORDS_BASED_ON_IDENTIFIER_FILTER);

            return true;

        }


    }

}


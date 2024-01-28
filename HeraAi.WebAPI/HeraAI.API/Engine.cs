using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Exceptions;
using HeraAI.API.Models;
using HeraAI.API.Models.Specs;
using HeraAI.API.Resources.Resources;
using HeraAI.API.Tools;
using HeraAI.API.Tools.Dbms;


namespace HeraAI.API
{

    public class Engine
    {

        // READONLY VARIABLES
        private readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        // STATE VARIABLES
        private readonly string connectionString;
        private SQLServer sqlServer;
        private User authenticatedUser;


        private Dictionary<Guid, string> _drinks = new Dictionary<Guid, string>()
        {
            { Guid.Parse("642E1905-1111-EC11-9FFF-00155E011509"), "Vodka" },
            { Guid.Parse("652E1905-1111-EC11-9FFF-00155E011509"), "Whisky" },
            { Guid.Parse("662E1905-1111-EC11-9FFF-00155E011509"), "Gin" },
        };


        /// <summary>
        /// Engine constructor
        /// </summary>
        /// <param name="connectionString">Connection string to Commercial database connection</param>
        public Engine(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(Resources.Resources.SpecsResources.FORBIDDEN_EMPTY_CONNECTIONSTRING);
        }


        /// <summary>
        /// Current authenticated user
        /// </summary>
        public User AuthenticatedUser
        {
            get { return authenticatedUser; }
        }


        /// <summary>
        /// To know if an user is currently logged in
        /// </summary>
        public bool UserIsLoggedIn
        {
            get { return !(this.AuthenticatedUser is null); }
        }

        #region Login

        /// <summary>
        /// User login with list of permissions
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Authenticated user</returns>
        public bool Login(string email, string password)
        {
            string message = "";
            //encrypt password

            // Validate if username or password are not empty
            if (!UserSpecs.Email.IsValid(email, ref message) || !UserSpecs.Password.IsValid(password, ref message))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), SpecsResources.LOGIN_INVALID_CREDENTIALS);

            password = Settings.Encrypt(password);

            if (this.Users.Exists(email, password, DataStates.Active) == false)
            {
                this.authenticatedUser = null;
                return false;
            }

            // set authenticated user and clean savedVersion and LastUser because is not necessary
            this.authenticatedUser = this.Users.Get(email, password, DataStates.Active);

            // Return authentication sucess
            return true;
        }


        /// <summary>
        /// Mobile User login with list of permissions
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Authenticated user</returns>
        public bool LoginMobile(string email, string password)
        {
            string message = "";
            //encrypt password

            // Validate if username or password are not empty
            if (!UserSpecs.Email.IsValid(email, ref message) || !UserSpecs.Password.IsValid(password, ref message))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), SpecsResources.LOGIN_INVALID_CREDENTIALS);

            password = Settings.Encrypt(password);

            if (this.Users.Exists(email, password, DataStates.Active) == false)
            {
                this.authenticatedUser = null;
                return false;
            }

            // set authenticated user and clean savedVersion and LastUser because is not necessary
            this.authenticatedUser = this.Users.GetMobile(email, password, DataStates.Active);

            // Return authentication sucess
            return true;
        }

        /// <summary>
        /// User login with list of permissions
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Authenticated user</returns>
        public bool LoginHeraAI(string email, string password)
        {
            string message = "";
            //encrypt password

            // Validate if username or password are not empty
            if (!UserSpecs.Email.IsValid(email, ref message) || !UserSpecs.Password.IsValid(password, ref message))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), SpecsResources.LOGIN_INVALID_CREDENTIALS);

            password = Settings.Encrypt(password);

            if (this.Users.Exists(email, password, DataStates.Active) == false)
            {
                this.authenticatedUser = null;
                return false;
            }

            // set authenticated user and clean savedVersion and LastUser because is not necessary
            this.authenticatedUser = this.Users.Get02(email, password, DataStates.Active);

            // Return authentication sucess
            return true;
        }

        public bool Login(string email, string password, Guid serialNumber)
        {
            string message = "";
            //encrypt password

            // Validate if username or password are not empty
            if (!UserSpecs.Email.IsValid(email, ref message) || !UserSpecs.Password.IsValid(password, ref message))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), SpecsResources.LOGIN_INVALID_CREDENTIALS);

            password = Settings.Encrypt(password);

            if (this.Users.Exists(email, password, DataStates.Active) == false)
            {
                this.authenticatedUser = null;
                return false;
            }

            // set authenticated user and clean savedVersion and LastUser because is not necessary
            this.authenticatedUser = this.Users.Get(email, password, serialNumber, DataStates.Active);

            // Return authentication sucess
            return true;
        }

        public bool LoginMobile(string email, string password, Guid? serialNumber, string deviceName)
        {
            string message = "";
            //encrypt password

            // Validate if username or password are not empty
            if (!UserSpecs.Email.IsValid(email, ref message) || !UserSpecs.Password.IsValid(password, ref message))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), SpecsResources.LOGIN_INVALID_CREDENTIALS);

            password = Settings.Encrypt(password);

            if (this.Users.Exists(email, password, DataStates.Active) == false)
            {
                this.authenticatedUser = null;
                return false;
            }

            // set authenticated user and clean savedVersion and LastUser because is not necessary
            this.authenticatedUser = this.Users.GetMobile(email, password, serialNumber != null ? serialNumber : null, deviceName, DataStates.Active);

            // Return authentication sucess
            return true;
        }


        public bool CheckPermission(string permissionName, Guid roleId)
        {

            if (this.Permissions.HasPermission(permissionName, roleId) == 0)
            {
                return false;
            }

            // Return authentication sucess
            return true;
        }

        public bool CheckPermission(string permissionName, string userId, string customerId)
        {

            if (this.Permissions.HasPermission(permissionName, userId, customerId) == 0)
            {
                return false;
            }

            // Return authentication sucess
            return true;
        }

        #endregion

        /// <summary>
        /// Logout authenticated user
        /// </summary>
        public void Logout()
        {
            // set autenticatedUser with null
            this.authenticatedUser = null;
        }


        /// <summary>
        /// SQLServer object access
        /// </summary>
        public SQLServer SQLServer
        {
            get
            {
                // if first time use then create object
                if (sqlServer is null)
                    sqlServer = new SQLServer(this.connectionString, SQLServer.SQLConnectionModes.manual);

                // return object
                return sqlServer;
            }
        }

        /// <summary>
        /// Data object access
        /// </summary>
        public EnumOptions EnumOptions
        {
            get
            {
                // If first time use than create object
                if (enumOptions is null)
                    enumOptions = new EnumOptions(this);

                // Return object
                return enumOptions;
            }
        }

    }

}

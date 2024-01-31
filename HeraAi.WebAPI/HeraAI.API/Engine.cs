using HeraAI.API.Enums;
using HeraAI.API.Tools;
using HeraAI.API.Models;
using HeraAI.API.Exceptions;
using HeraAI.API.Tools.Dbms;
using HeraAI.API.Models.Specs;
using HeraAI.API.Resources.Resources;


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

        private Users users;
        private EnumOptions enumOptions;

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


            // VALIDATE IF USERNAME OR PASSWORD ARE NOT EMPTY
            if (!UserSpecs.Email.IsValid(email, ref message) || !UserSpecs.Password.IsValid(password, ref message))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), SpecsResources.LOGIN_INVALID_CREDENTIALS);


            password = Settings.Encrypt(password);


            if (this.Users.Exists(email, password, DataStates.Active) == false)
            {
                this.authenticatedUser = null;
                return false;
            }


            // SET AUTHENTICATED USER AND CLEAN SAVEDVERSION AND LASTUSER BECAUSE IS NOT NECESSARY
            this.authenticatedUser = this.Users.Get(email, password, DataStates.Active);


            // RETURN AUTHENTICATION SUCESS
            return true;

        }

        #endregion


        /// <summary>
        /// Logout authenticated user
        /// </summary>
        public void Logout()
        {
            // SET AUTENTICATEDUSER WITH NULL
            this.authenticatedUser = null;
        }


        /// <summary>
        /// SQLServer object access
        /// </summary>
        public SQLServer SQLServer
        {

            get
            {

                // IF FIRST TIME USE THEN CREATE OBJECT
                if (sqlServer is null)
                    sqlServer = new SQLServer(this.connectionString, SQLServer.SQLConnectionModes.manual);


                // RETURN OBJECT
                return sqlServer;

            }

        }


        /// <summary>
        /// Data object access
        /// </summary>
        public Users Users
        {

            get
            {

                // IF FIRST TIME USE THAN CREATE OBJECT
                if (users is null)
                    users = new Users(this);


                // RETURN OBJECT
                return users;

            }

        }


        /// <summary>
        /// Data object access
        /// </summary>
        public EnumOptions EnumOptions
        {

            get
            {

                // IF FIRST TIME USE THAN CREATE OBJECT
                if (enumOptions is null)
                    enumOptions = new EnumOptions(this);


                // RETURN OBJECT
                return enumOptions;

            }

        }

    }

}

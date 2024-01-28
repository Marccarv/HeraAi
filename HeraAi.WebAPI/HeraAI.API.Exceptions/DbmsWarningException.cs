namespace HeraAI.API.Exceptions
{

    public class DbmsWarningException : ApplicationException
    {

        // VARIABLES
        private string module;
        private string _class;
        private string origin;
        private string[] entityKeys;
        private string info;


        /// <summary>
        /// Use this method to create a new Cuco Exception Warning without entity keys
        /// </summary>
        /// <param name="module">module where the exception was created</param>
        /// <param name="_class">class where the exception was created (if sql error insert "sqldatabase")</param>
        /// <param name="origin">method or stored procedure where the exception was created (if sql error use the name of stored procedure)</param>
        /// <param name="info">adicional information to help track back the exception</param>
        public DbmsWarningException(string module, string _class, string origin, string info)
        {
            this.module = module;
            this._class = _class;
            this.origin = origin;
            this.entityKeys = new string[] { };
            this.info = info;
        }


        /// <summary>
        /// Use this method to create a new Cuco Exception Warning with entity keys
        /// </summary>
        /// <param name="module">module where the exception was created</param>
        /// <param name="_class">class where the exception was created (if sql error insert "sqldatabase")</param>
        /// <param name="origin">method or stored procedure where the exception was created (if sql error use the name of stored procedure)</param>
        /// <param name="entityKeys">list of primary keys that originated the exception</param>
        /// <param name="info">adicional information to help track back the exception</param>
        public DbmsWarningException(string module, string _class, string origin, string[] entityKeys, string info)
        {
            this.module = module;
            this._class = _class;
            this.origin = origin;
            this.entityKeys = entityKeys;
            this.info = info;
        }


        public string Module
        {
            get { return CleanText(module); }
            set { module = value; }
        }


        public string Class
        {
            get { return CleanText(_class); }
            set { _class = value; }
        }


        public string Origin
        {
            get { return CleanText(origin); }
            set { origin = value; }
        }


        public string[] EntityKeys
        {
            get => entityKeys;
            set => entityKeys = value;
        }


        public string GetEntityKeysSet()
        {

            // VARIABLES
            string setOfKeys;


            // BUILD JSON TO REPRESENT ENTITYKEYS COLLECTION
            setOfKeys = "";
            setOfKeys += "{";


            // ADD ENTITYKEY1 IF IT EXISTS
            if (entityKeys.Length > 0)
            {

                setOfKeys += string.Format(" \"{0}\"", entityKeys[0]);


                // ADD ENTITYKEY2 IF IT EXISTS
                if (entityKeys.Length > 1)
                {

                    setOfKeys += ",";
                    setOfKeys += string.Format(" \"{0}\"", entityKeys[1]);


                    // ADD ENTITYKEY3 IF IT EXISTS
                    if (entityKeys.Length > 2)
                    {

                        setOfKeys += ",";
                        setOfKeys += string.Format(" \"{0}\"", entityKeys[2]);


                        // ADD ENTITYKEY3 IF IT EXISTS
                        if (entityKeys.Length > 3)
                        {
                            setOfKeys += ",";
                            setOfKeys += string.Format(" \"{0}\"", entityKeys[3]);
                        }

                    }

                }

            }


            // END JSON ELEMENT
            setOfKeys += " }";


            // RETURN JSON
            return setOfKeys;

        }


        public string Info
        {
            get { return CleanText(info); }
            set { info = value; }
        }


        public string ToJson()
        {

            // VARIABLES
            string json;


            // BUILD JSON STRING
            json = "";
            json += "{";
            json += string.Format("\"{0}\" : \"{1}\"", "module", this.Module);
            json += ", ";
            json += string.Format("\"{0}\" : \"{1}\"", "class", this.Class);
            json += ", ";
            json += string.Format("\"{0}\" : \"{1}\"", "origin", this.Origin);
            json += ", ";
            json += string.Format("\"{0}\" : \"{1}\"", "entityKeys", this.GetEntityKeysSet().Replace("\"", "'"));
            json += ", ";
            json += string.Format("\"{0}\" : \"{1}\"", "info", CleanText(info));
            json += ", ";
            json += string.Format("\"{0}\" : \"{1}\"", "stackTrace", CleanText(this.StackTrace));
            json += "} ";

            // RETURN JSON
            return json;

        }


        private string CleanText(string content)
        {

            // VARIABLES
            string strippedContent;


            // INITIALIZING VARIABLES
            strippedContent = content;


            // CLEAN TEXT
            strippedContent = strippedContent.Replace("\r\n", string.Empty);
            strippedContent = strippedContent.Replace("\'", string.Empty);
            strippedContent = strippedContent.Replace("\"", string.Empty);
            strippedContent = strippedContent.Replace("`", " ");
            strippedContent = strippedContent.Replace("[", "((");
            strippedContent = strippedContent.Replace("]", "))");
            strippedContent = strippedContent.Replace("&", string.Empty);
            strippedContent = strippedContent.Replace(@"\", @"/");
            strippedContent = strippedContent.Replace(@"sqldatabase", @"database");
            strippedContent = strippedContent.Replace(@"dbo.", @"");
            strippedContent = strippedContent.Replace(@"UV_", @"VIEW____");
            strippedContent = strippedContent.Replace(@"USP_", @"PROC____");


            // RETURN STRIPPEDTEXT
            return strippedContent;

        }

    }

}

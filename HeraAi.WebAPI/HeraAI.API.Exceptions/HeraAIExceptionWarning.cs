namespace HeraAI.API.Exceptions
{

    public class HeraAIExceptionWarning : Exception
    {

        // VARIABLES
        private string module;
        private string _class;
        private string origin;
        private string[] entityKeys;
        private string info;


        /// <summary>
        /// Create a new Standard Exception
        /// </summary>
        /// <param name="module">Module where the exception was created</param>
        /// <param name="_class">Class where the exception was created (if sql error insert "sqldatabase")</param>
        /// <param name="origin">Method or stored procedure where the exception was created (if sql error use the name of stored procedure)</param>
        /// <param name="info">Adicional information to help track back the exception</param>
        public HeraAIExceptionWarning(string module, string _class, string origin, string info)
        {
            this.module = module;
            this._class = _class;
            this.origin = origin;
            this.entityKeys = new string[] { };
            this.info = info;
        }


        /// <summary>
        /// Create a new Standard Exception with entity keys
        /// </summary>
        /// <param name="module">Module where the exception was created</param>
        /// <param name="_class">Class where the exception was created (if sql error insert "sqldatabase")</param>
        /// <param name="origin">Method or stored procedure where the exception was created (if sql error use the name of stored procedure)</param>
        /// <param name="entityKeys">Identifier(s) for entity involved in exception</param>
        /// <param name="info">Adicional information to help track back the exception</param>
        public HeraAIExceptionWarning(string module, string _class, string origin, string[] entityKeys, string info)
        {
            this.module = module;
            this._class = _class;
            this.origin = origin;
            this.entityKeys = entityKeys;
            this.info = info;
        }


        /// <summary>
        /// Project where the exception originated
        /// </summary>
        public string Module
        {
            get { return CleanText(module); }
            set { module = value; }
        }


        /// <summary>
        /// Class where the exception originated
        /// </summary>
        public string Class
        {
            get { return CleanText(_class); }
            set { _class = value; }
        }


        /// <summary>
        /// Method where the exception originated
        /// </summary>
        public string Origin
        {
            get { return CleanText(origin); }
            set { origin = value; }
        }


        /// <summary>
        /// Entity identifiers involved in the exception
        /// </summary>
        public string[] EntityKeys { get => entityKeys; set => entityKeys = value; }

        /// <summary>
        /// Get information of entity keys array
        /// </summary>
        /// <returns>String with information about all entity keys</returns>
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


        /// <summary>
        /// Aditional information about the exception
        /// </summary>
        public string Info
        {
            get { return CleanText(info); }
            set { info = value; }
        }


        /// <summary>
        /// Translate Exception object to an JSON string
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Remove or replace unnecessary characters
        /// </summary>
        /// <param name="content">Text for Exception</param>
        /// <returns>Clean Exception text</returns>
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


            // RETURN STRIPPEDTEXT
            return strippedContent;

        }

    }

}

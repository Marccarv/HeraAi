using System.Text;
using Newtonsoft.Json.Linq;
using HeraAI.API.Exceptions;
using System.Runtime.Serialization.Json;


namespace HeraAI.API.Tools.DataInterchange
{

    public static class Json
    {

        // READONLY VARIABLES
        private static readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private static readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        /// <summary>
        /// Serializes a list of objects to json format
        /// </summary>
        /// <param name="obj">Object to be serialized</param>
        /// <param name="dateTimeFormat">Datetime format to be used on datetime properties</param>
        /// <returns>String with a json representation of the list of object</returns>
        public static string Serialize(object obj, string dateTimeFormat)
        {

            // DOCUMENTATION: HTTPS://DOCS.MICROSOFT.COM/EN-US/DOTNET/FRAMEWORK/WCF/FEATURE-DETAILS/HOW-TO-SERIALIZE-AND-DESERIALIZE-JSON-DATA


            // VARIABLES
            MemoryStream memoryStream;
            DataContractJsonSerializer dataContractJsonSerializer;
            DataContractJsonSerializerSettings dataContractJsonSerializerSettings;
            byte[] result;


            // INITIALIZE VARIABLES
            memoryStream = new MemoryStream();
            dataContractJsonSerializerSettings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat(dateTimeFormat)
            };


            // SET DATA CONTRACT JSON SERIALIZER SETTINGS
            dataContractJsonSerializer = new DataContractJsonSerializer(obj.GetType(), dataContractJsonSerializerSettings);


            // SERIALIZE TO STRING
            dataContractJsonSerializer.WriteObject(memoryStream, obj);
            result = memoryStream.ToArray();
            memoryStream.Close();


            // RETURN STRING WITH JSON RESULT
            return Encoding.UTF8.GetString(result, 0, result.Length);

        }


        /// <summary>
        /// Deserialize a Json string to a generic object
        /// </summary>
        /// <param name="jsonString">Json string to deserialize</param>
        /// <returns>Generic object with json string information</returns>
        public static JObject Deserialize(string jsonString)
        {

            try
            {
                return JObject.Parse(jsonString);
            }
            catch
            {
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.DATAINTERCHANGE_JSON_DESERIALIZE_ERROR);
            }

        }


        /// <summary>
        /// Validates Json string
        /// </summary>
        /// <param name="strInput">Json string to validate</param>
        /// <returns>True if Json string is valid</returns>
        public static bool IsValidJson(string strInput)
        {

            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || // For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) // For array
            {

                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch // SOME PROBABLY MEAN INVALID JSON EXCEPTION
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
    
    }

}

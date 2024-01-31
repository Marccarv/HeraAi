using HeraAI.API.Tools;
using System.Globalization;
using HeraAI.API.Tools.Validations;


namespace HeraAI.API.Models.Specs.Others.Properties
{

    public partial class DateTimeProperty
    {

        /// <summary>
        /// Verifies if the value respects the defined input pattern to became a valid datetime
        /// Value must be provided in the format yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        /// <param name="value">Value to be verified (format: yyyy-MM-dd HH:mm:ss.fff)</param>
        /// <param name="resultDateTime">Converted value (only fileed if conversion is possible)</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if value (string) can be converted to double</returns>
        public bool IsValid(string value, out DateTime? resultDateTime, ref string info)
        {

            // VARIABLES
            bool isValid;
            string messages;
            DateTime dateTime;


            // INITIALIZE VARIABLES
            messages = "";
            isValid = true;
            

            // VALIDATE PATTERN
            if (!Tools.Regex.IsValid(value, this.InputRegexPattern))
            {

                info += string.Format("{0}: {1};", this.Name, Resources.Resources.SpecsResources.REGEX_PATTERN_INCOMPATIBILITY);
                
                
                resultDateTime = null;
                
                
                return false;

            }


            // PARSE TO DATETIME OBJECT (CONVERTED DATETIME IS ASSIGNED AUTOMATICALLY TO OUT PARAMETER)
            if (!DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime))
            {

                info += string.Format("{0}: {1};", this.Name, Resources.Resources.SpecsResources.TYPE_CONVERSION_IMCOMPATIBILITY);
                
                
                resultDateTime = null;
                

                return false;
                
            }


            // SET OUT RESULTDATETIME PARAMETER TO DATETIME
            resultDateTime = dateTime;


            // VALIDATE THE OTHER RULES
            isValid = this.IsValid(dateTime, ref messages);


            // RETURN RESULTS
            info += messages;


            // RETURN ISVALID
            return isValid;

        }


        /// <summary>
        /// Verifies if the value is in a valid interval 
        /// </summary>
        /// <param name="value">Value to be verified</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if object is valid</returns>
        public bool IsValid(DateTime? value, ref string info)
        {
            // Validate empty value
            if (!this.AllowEmptyValue && value is null)
            {
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.FORBIDDEN_NULL);
                return false;
            }
            else if (this.AllowEmptyValue && value is null)
                return true;

            // Validate datetime interval
            if (!DateTimes.IsValid(value, this.MinValue, this.MaxValue))
            {
                info += string.Format("{0} [{1}, {2}]: {3};",
                    this.Name,
                    this.MinValue.ToString(this.OutputFormat),
                    this.MaxValue.ToString(this.OutputFormat),
                    Resources.Resources.SpecsResources.OUT_OF_ALLOWED_INTERVAL);
                return false;
            }

            // Ok
            return true;
        }

        /// <summary>
        /// Verifies if the value is in a valid interval 
        /// </summary>
        /// <param name="value">Value to be verified</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if object is valid</returns>
        public bool IsValidDateTime(DateTime? value, bool isUpdating, ref string info)
        {
            // Validate empty value
            if (!this.AllowEmptyValue && value is null)
            {
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.FORBIDDEN_NULL);
                return false;
            }
            else if (this.AllowEmptyValue && value is null)
                return true;

            // Validate datetime interval
            if (!DateTimes.IsValid(value, isUpdating, this.MinValue, this.MaxValue))
            {
                info += string.Format("{0} [{1}, {2}]: {3};",
                    this.Name,
                    this.MinValue.ToString(this.OutputFormat),
                    this.MaxValue.ToString(this.OutputFormat),
                    Resources.Resources.SpecsResources.OUT_OF_ALLOWED_INTERVAL);
                return false;
            }

            // Ok
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Tools;
using HeraAI.API.Tools.Validations;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class TimeOnlyProperty
    {

        /// <summary>
        /// Verifies if the value respects the defined input pattern to became a valid datetime
        /// Value must be provided in the format yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        /// <param name="value">Value to be verified (format: yyyy-MM-dd HH:mm:ss.fff)</param>
        /// <param name="resultDateTime">Converted value (only fileed if conversion is possible)</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if value (string) can be converted to double</returns>
        public bool IsValid(string value, out TimeOnly? resultTimeOnly, ref string info)
        {
            // Variables
            bool isValid;
            string messages;
            TimeOnly timeOnly;

            // Initialize variables
            isValid = true;
            messages = "";

            // Validate pattern
            if (!Tools.Regex.IsValid(value, this.InputRegexPattern))
            {
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.REGEX_PATTERN_INCOMPATIBILITY);
                resultTimeOnly = null;
                return false;
            }

            // Parse to datetime object (converted datetime is assigned automatically to out parameter)
            if (!TimeOnly.TryParseExact(value, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out timeOnly))
            {
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.TYPE_CONVERSION_IMCOMPATIBILITY);
                resultTimeOnly = null;
                return false;
            }

            // Set out resultDateTime parameter to dateTime
            resultTimeOnly = timeOnly;

            // Validate the other rules
            isValid = this.IsValid(timeOnly, ref messages);

            // Return results
            info += messages;

            // Return isValid
            return isValid;
        }


        /// <summary>
        /// Verifies if the value is in a valid interval 
        /// </summary>
        /// <param name="value">Value to be verified</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if object is valid</returns>
        public bool IsValid(TimeOnly? value, ref string info)
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
            if (!TimeOnlys.IsValid(value, this.MinValue, this.MaxValue))
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
        public bool IsValidDateTime(TimeOnly? value, bool isUpdating, ref string info)
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
            if (!TimeOnlys.IsValid(value, isUpdating, this.MinValue, this.MaxValue))
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

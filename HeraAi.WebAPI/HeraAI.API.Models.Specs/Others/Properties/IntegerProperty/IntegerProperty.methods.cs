using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Tools;
using HeraAI.API.Tools.Validations;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class IntegerProperty
    {

        /// <summary>
        /// Verifies if the value respects the defined input pattern and number of digits defined in the object
        /// </summary>
        /// <param name="value">Value to be verified</param>
        /// <param name="resultInt">Converted value (only filled if conversion is possible)</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if value (string) can be converted to double</returns>
        public bool IsValid(string value, out int? resultInt, ref string info)
        {
            // Variables
            bool isValid;
            string messages;
            int intValue;

            // Initialize variables
            isValid = true;
            messages = "";

            // Validate pattern
            if (!Tools.Regex.IsValid(value, this.InputRegexPattern))
            {
                // Add the info with the encountered problem
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.REGEX_PATTERN_INCOMPATIBILITY);
                resultInt = null;
                return false;
            }

            // Cast to int object
            if (!int.TryParse(value, out intValue))
            {
                // Add the info with the encountered problem
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.TYPE_CONVERSION_IMCOMPATIBILITY);
                resultInt = null;
                return false;
            }

            // Set out resultInt parameter to intValue
            resultInt = intValue;

            // Validate the other rules
            isValid = this.IsValid(intValue, ref messages);

            // Return results
            info += messages;

            // Return isValid
            return isValid;
        }


        /// <summary>
        /// Verifies if the value is valid 
        /// </summary>
        /// <param name="value">Value to be verified</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if object is valid</returns>
        public bool IsValid(long? value, ref string info)
        {
            // Validate empty value
            if (!this.AllowEmptyValue && value is null)
            {
                info += string.Format("{0}: {1}",
                    this.Name,
                    Resources.Resources.SpecsResources.FORBIDDEN_NULL);
                return false;
            }
            else if (this.AllowEmptyValue && value is null)
                return true;

            // Validate interval
            if (!Integers.IsValid(value, this.MinValue, this.MaxValue))
            {
                info += string.Format("{0} [{1},{2}]: {3};",
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class StringProperty
    {
        /// <summary>
        /// Validates if the string received respects all defined rules of the StringProperty
        /// </summary>
        /// <param name="value">String value to be validated</param>
        /// <returns>True if object is valid</returns>
        public bool IsValid(string value)
        {
            // Variables
            string dummy = "";

            // Return if object is valid
            return IsValid(value, ref dummy);
        }


        /// <summary>
        /// Validates if the string received respects all defined rules of the StringProperty,
        /// </summary>
        /// <param name="value">String value to be validated</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if object is valid</returns>
        public bool IsValid(string value, ref string info)
        {
            // Validate empty value
            if (!this.AllowEmptyValue && value.Trim().Length == 0)
            {
                info += string.Format("{0}: {1};",
                    this.Name,
                    Resources.Resources.SpecsResources.FORBIDDEN_NULL
                    );
                return false;
            }

            // Validate pattern (chars whitelist)
            if (!Tools.Regex.IsValid(value, this.InputRegexPattern))
            {
                info += string.Format("{0} length:[{1},{2}]: {3};",
                    this.Name,
                    this.MinLength.ToString(),
                    this.MaxLength.ToString(),
                    Resources.Resources.SpecsResources.REGEX_PATTERN_INCOMPATIBILITY
                    );
                return false;
            }

            // is valid
            return true;
        }
    }
}

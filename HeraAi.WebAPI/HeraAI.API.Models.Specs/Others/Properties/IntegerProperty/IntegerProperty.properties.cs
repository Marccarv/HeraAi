using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Tools;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class IntegerProperty
    {

        // Variables
        private readonly string name;
        private readonly bool allowEmptyValue;
        private readonly IntegerAllowedIntervals allowedInterval;
        private readonly int? customNumberOfDigits;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if mandatory</param>
        /// <param name="allowedInterval">Allowed integer interval</param>
        public IntegerProperty(string name, bool allowEmptyValue, IntegerAllowedIntervals allowedInterval)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedInterval = allowedInterval;
            this.customNumberOfDigits = null;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if mandatory</param>
        /// <param name="allowedInterval">Allowed integer interval</param>
        /// <param name="customNumberOfDigits">Number of decimal digits allowed</param>
        public IntegerProperty(string name, bool allowEmptyValue, IntegerAllowedIntervals allowedInterval, int customNumberOfDigits)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedInterval = allowedInterval;
            this.customNumberOfDigits = customNumberOfDigits;
        }


        public string Name { get => name; }


        public bool AllowEmptyValue { get => allowEmptyValue; }


        public IntegerAllowedIntervals AllowedInterval { get => allowedInterval; }


        public string OutputFormat
        {
            get
            {
                return GlobalVars.GetIntegerOutputFormat(this.NumberOfDigits);
            }
        }


        public string InputRegexPattern
        {
            get
            {
                return GlobalVars.GetIntegerInputPattern(
                    this.NumberOfDigits,
                    this.AllowNegativeNumbers,
                    this.AllowEmptyValue);
            }
        }


        public int MinValue
        {
            get
            {
                return GlobalVars.GetIntegerMinValue(this.AllowedInterval);
            }
        }


        public long MaxValue
        {
            get
            {
                return GlobalVars.GetIntegerMaxValue(this.AllowedInterval);
            }
        }


        public bool AllowNegativeNumbers
        {
            get
            {
                return GlobalVars.GetIntegerMinValue(this.AllowedInterval) < 0 ? true : false;
            }
        }


        public bool NegativeNumbersAllowed
        {
            get
            {
                return GlobalVars.GetIntegerMinValue(this.AllowedInterval) < 0 ? true : false;
            }
        }


        public int NumberOfDigits
        {
            get
            {
                return this.customNumberOfDigits is null ? GlobalVars.GetIntegerDigits(this.AllowedInterval) : (this.customNumberOfDigits ?? 0);
            }
        }

    }
}

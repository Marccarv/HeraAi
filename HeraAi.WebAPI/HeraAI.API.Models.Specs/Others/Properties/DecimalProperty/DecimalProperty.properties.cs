using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Tools;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class DecimalProperty
    {

        // Variables
        private readonly string name;
        private readonly bool allowEmptyValue;
        private readonly DoubleAllowedIntervals allowedInterval;
        private readonly int? customNumberOfDotRightDigits;
        private readonly int? customNumberOfDotLeftDigits;

        #region Contructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if mandatory</param>
        /// <param name="allowedInterval">Double interval allowed</param>
        public DecimalProperty(string name, bool allowEmptyValue, DoubleAllowedIntervals allowedInterval)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedInterval = allowedInterval;
            this.customNumberOfDotRightDigits = null;
            this.customNumberOfDotLeftDigits = null;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if mandatory</param>
        /// <param name="allowedInterval">Double interval allowed</param>
        /// <param name="customNumberOfDotRightDigits">Number of decimal digits allowed (right side of the decimal dot)</param>
        public DecimalProperty(string name, bool allowEmptyValue, DoubleAllowedIntervals allowedInterval, int customNumberOfDotRightDigits)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedInterval = allowedInterval;
            this.customNumberOfDotRightDigits = customNumberOfDotRightDigits;
            this.customNumberOfDotLeftDigits = null;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if mandatory</param>
        /// <param name="allowedInterval">Double interval allowed</param>
        /// <param name="customNumberOfDotLeftDigits">Number of unitary digits allowed (left side of the decimal dot)</param>
        /// <param name="customNumberOfDotRightDigits">Number of decimal digits allowed (right side of the decimal dot)</param>
        public DecimalProperty(string name, bool allowEmptyValue, DoubleAllowedIntervals allowedInterval, int customNumberOfDotLeftDigits, int customNumberOfDotRightDigits)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedInterval = allowedInterval;
            this.customNumberOfDotLeftDigits = customNumberOfDotLeftDigits;
            this.customNumberOfDotRightDigits = customNumberOfDotRightDigits;
        }

        #endregion

        #region Properties

        public string Name { get => name; }


        public bool AllowEmptyValue { get => allowEmptyValue; }


        public DoubleAllowedIntervals AllowedInterval { get => allowedInterval; }


        public string OutputFormat
        {
            get
            {
                return GlobalVars.GetDoubleOutputFormat(this.NumberOfDotLeftDigits, this.NumberOfDotRightDigits);
            }
        }


        public string InputRegexPattern
        {
            get
            {
                return GlobalVars.GetDoubleInputPattern(
                    this.NumberOfDotLeftDigits,
                    this.NumberOfDotRightDigits,
                    this.AllowNegativeNumbers,
                    this.AllowEmptyValue);
            }
        }


        public Decimal MinValue
        {
            get
            {
                return GlobalVars.GetDecimalMinValue(this.AllowedInterval);
            }
        }


        public Decimal MaxValue
        {
            get
            {
                return GlobalVars.GetDecimalMaxValue(this.AllowedInterval);
            }
        }


        public bool AllowNegativeNumbers
        {
            get
            {
                return GlobalVars.GetDecimalMinValue(this.AllowedInterval) < 0 ? true : false;
            }
        }


        public int NumberOfDotLeftDigits
        {
            get
            {
                return this.customNumberOfDotLeftDigits is null ? GlobalVars.GetDoubleDotLeftDigits(this.AllowedInterval) : (this.customNumberOfDotLeftDigits ?? 0);
            }
        }


        public int NumberOfDotRightDigits
        {
            get
            {
                return this.customNumberOfDotRightDigits is null ? GlobalVars.GetDoubleDotRightDigits(this.AllowedInterval) : (this.customNumberOfDotRightDigits ?? 0);
            }
        }
        #endregion
    }
}

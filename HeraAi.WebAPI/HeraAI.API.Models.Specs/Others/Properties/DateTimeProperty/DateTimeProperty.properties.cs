using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Tools;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class DateTimeProperty
    {
        // Variables
        private readonly string name;
        private readonly bool allowEmptyValue;
        private readonly DateTimeAllowedPeriods allowedPeriod;
        private readonly DateTimeFormats dateTimeFormat;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if not mandatory</param>
        /// <param name="allowedPeriod">Allowed period of time to validate dates</param>
        /// <param name="dateTimeFormat">Format of datetime to be used</param>

        public DateTimeProperty(string name, bool allowEmptyValue, DateTimeAllowedPeriods allowedPeriod, DateTimeFormats dateTimeFormat)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedPeriod = allowedPeriod;
            this.dateTimeFormat = dateTimeFormat;
        }

        public string Name { get => name; }


        public bool AllowEmptyValue { get => allowEmptyValue; }


        public DateTimeAllowedPeriods AllowedPeriod { get => allowedPeriod; }


        public DateTimeFormats DateTimeFormat { get => dateTimeFormat; }



        public string OutputFormat
        {
            get
            {
                return GlobalVars.GetDateTimeOutputFormat(this.DateTimeFormat);
            }
        }


        public string InputRegexPattern
        {
            get
            {
                return GlobalVars.GetDateTimeInputPattern(this.AllowEmptyValue);
            }
        }


        public DateTime MinValue
        {
            get
            {
                return GlobalVars.GetDateTimeMinValue(this.AllowedPeriod);
            }
        }


        public DateTime MaxValue
        {
            get
            {
                return GlobalVars.GetDateTimeMaxValue(this.AllowedPeriod);
            }
        }
    }
}

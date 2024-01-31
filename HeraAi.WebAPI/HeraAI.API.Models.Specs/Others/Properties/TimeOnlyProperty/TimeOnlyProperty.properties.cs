using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Tools;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class TimeOnlyProperty
    {
        // Variables
        private readonly string name;
        private readonly bool allowEmptyValue;
        private readonly TimeOnlyAllowedPeriods allowedPeriod;
        private readonly TimeOnlyFormats timeOnlyFormat;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if not mandatory</param>
        /// <param name="allowedPeriod">Allowed period of time to validate dates</param>
        /// <param name="dateTimeFormat">Format of datetime to be used</param>

        public TimeOnlyProperty(string name, bool allowEmptyValue, TimeOnlyAllowedPeriods allowedPeriod, TimeOnlyFormats timeOnlyFormat)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.allowedPeriod = allowedPeriod;
            this.timeOnlyFormat = timeOnlyFormat;
        }

        public string Name { get => name; }


        public bool AllowEmptyValue { get => allowEmptyValue; }


        public TimeOnlyAllowedPeriods AllowedPeriod { get => allowedPeriod; }


        public TimeOnlyFormats DateTimeFormat { get => timeOnlyFormat; }



        public string OutputFormat
        {
            get
            {
                return GlobalVars.GetTimeOnlyOutputFormat(this.DateTimeFormat);
            }
        }


        public string InputRegexPattern
        {
            get
            {
                return GlobalVars.GetTimeOnlyInputPattern(this.AllowEmptyValue);
            }
        }


        public TimeOnly MinValue
        {
            get
            {
                return GlobalVars.GetTimeOnlyMinValue(this.AllowedPeriod);
            }
        }


        public TimeOnly MaxValue
        {
            get
            {
                return GlobalVars.GetTimeOnlyMaxValue(this.AllowedPeriod);
            }
        }
    }
}

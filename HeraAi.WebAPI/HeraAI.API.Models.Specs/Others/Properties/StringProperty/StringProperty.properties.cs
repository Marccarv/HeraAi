using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Tools;

namespace HeraAI.API.Models.Specs.Others.Properties
{
    public partial class StringProperty
    {
        // Variables
        private readonly string name;
        private readonly bool allowEmptyValue;
        private readonly StringContentTypes stringContentType;
        private readonly StringLengthTypes stringLengthType;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if empty values are allowed</param>
        /// <param name="stringContentType">String content type</param>
        /// <param name="stringLengthType">String length type</param>
        public StringProperty(string name, bool allowEmptyValue, StringContentTypes stringContentType, StringLengthTypes stringLengthType)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
            this.stringContentType = stringContentType;
            this.stringLengthType = stringLengthType;
        }


        public string Name { get => name; }


        public bool AllowEmptyValue { get => allowEmptyValue; }


        public StringContentTypes StringContentType { get => stringContentType; }


        public StringLengthTypes StringLengthType { get => stringLengthType; }





        public string InputRegexPattern
        {
            get
            {
                return GlobalVars.GetStringInputPattern(this.StringContentType, this.StringLengthType, this.AllowEmptyValue);
            }
        }


        public int MaxLength
        {
            get
            {
                return GlobalVars.GetStringMaxLength(this.StringLengthType);
            }
        }


        public int MinLength
        {
            get
            {
                return GlobalVars.GetStringMinLength(this.StringLengthType);
            }
        }
    }
}

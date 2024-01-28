using HeraAI.API.Enums;
using System.Globalization;
using HeraAI.API.Exceptions;


namespace HeraAI.API.Models.Specs
{
    public static class GlobalVars
    {

        //  VARIABLES
        static readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        static readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        public static string DefaultExternalUserId
        {
            get { return "000001"; }
        }


        /// <summary>
        /// Blacklist of dangerous words to be forbidden in strings
        /// </summary>
        public static string[] DangerousWords
        {

            get
            {

                return new string[] {
                    "DROP ",
                    "TRUNCATE ",
                    "SHUTDOWN ",
                    "SHELL ",
                    "SCRIPT ",
                    "DECLARE ",
                    "DELETE ",
                    "UPDATE ",
                    "INSERT ",
                    "SELECT ",
                    "ALTER ",
                    "GRANT ",
                    "SYS.",
                    "SYSOBJECTS",
                    "SYSCOLUMNS",
                    "DATABASE ",
                    "TABLE ",
                    "CREATE ",
                    "BEGIN ",
                    "ASC ",
                    "DESC ",
                    "CAST ",
                    "EXEC ",
                    "EXECUTE ",
                    "EXEC(",
                    "EXECUTE(",
                    "FETCH ",
                    "KILL ",
                    "UNION ",
                    "SP_",
                    "XP_",
                    "--",
                    "/*",
                    "*/",
                    "@@"
                };

            }

        }


        /// <summary>
        /// Symbol to be used to invalidate dangerous words (e.g. drop to d!rop, symbol used to invalidate is !)
        /// </summary>
        public static string SymbolToInvalidateWords
        {
            get { return "!"; }
        }


        #region DateTime
        /// <summary>
        /// Returns the datetime pattern representation to use in json.
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeDataInterchangeRepresentation()
        {
            return GetDateTimeOutputFormat(DateTimeFormats.fullDateTime);
        }


        /// <summary>
        /// Returns the min TimeOnly allowed to the Allowed Period that is received as a parameter.
        /// </summary>
        /// <param name="allowedPeriod">Allowed peiod to be used</param>
        /// <returns>Min datetime allowed</returns>
        public static TimeOnly GetTimeOnlyMinValue(TimeOnlyAllowedPeriods allowedPeriod)
        {

            // LOCAL VARIABLES
            TimeOnly minTimeOnly;


            // MIN VALUE DEPENDS ON THE ALLOWED PERIOD
            switch (allowedPeriod)
            {

                case TimeOnlyAllowedPeriods.undefinedPast:
                    minTimeOnly = new TimeOnly();

                    break;

                case TimeOnlyAllowedPeriods.hours:
                    minTimeOnly = new TimeOnly(00,00);

                    break;

                case TimeOnlyAllowedPeriods.seconds:
                    minTimeOnly = new TimeOnly(00, 00,00);

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            
            }


            // RETURN DATETIME
            return minTimeOnly;

        }


        /// <summary>
        /// Returns the max TimeOnly allowed to the Allowed Period that is received as a parameter.
        /// </summary>
        /// <param name="allowedPeriod">Allowed peiod to be used</param>
        /// <returns>Min datetime allowed</returns>
        public static TimeOnly GetTimeOnlyMaxValue(TimeOnlyAllowedPeriods allowedPeriod)
        {

            // LOCAL VARIABLES
            TimeOnly maxTimeOnly;


            // MIN VALUE DEPENDS ON THE ALLOWED PERIOD
            switch (allowedPeriod)
            {

                case TimeOnlyAllowedPeriods.undefinedPast:
                case TimeOnlyAllowedPeriods.seconds:
                    maxTimeOnly = new TimeOnly(23,59,59);

                    break;

                case TimeOnlyAllowedPeriods.hours:
                    maxTimeOnly = new TimeOnly(23, 59);

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            
            }


            // RETURN DATETIME
            return maxTimeOnly;

        }


        /// <summary>
        /// Returns datetime format to be used.
        /// </summary>
        /// <param name="dateTimeFormat">Date time format type</param>
        /// <returns>String format to be used</returns>
        public static string GetTimeOnlyOutputFormat(TimeOnlyFormats timeOnlyFormat)
        {

            // VARIABLES
            string format;


            // NOTE: DO NOT FORGET THAT REGEX PATTERNS MUST BE UPDATED TOO IF SOME NEW ENUM OPTION IS ADDED


            // FORMAT DEPENDS ON THE DATE TIME FORMAT
            switch (timeOnlyFormat)
            {

                case TimeOnlyFormats.hour:
                    format = "HH:mm";

                    break;

                case TimeOnlyFormats.second:
                    format = "HH:mm:ss";

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            
            }

            // RETURN PROCESSED FORMAT
            return format;

        }


        /// <summary>
        /// Return a datetime regex pattern.
        /// </summary>
        /// <param name="allowEmptyValue">True if empty values are allowed<</param>
        /// <returns>Regex pattern</returns>
        public static string GetTimeOnlyInputPattern(bool allowEmptyValue)
        {

            // VARIABLES
            string pattern;


            // INITIALIZE VARIABLES
            pattern = @"^(?:(?:[01]?[0-9]|2[0-3])(?::(?:[0-5][0-9]?)?)?)?$";


            // ALLOW EMPTY VALUE
            if (allowEmptyValue)
                pattern = pattern.Replace("#1#", "?");
            else
                pattern = pattern.Replace("#1#", "");


            // RETURN PATTERN
            return pattern;

        }


        /// <summary>
        /// Returns the min datetime allowed to the Allowed Period that is received as a parameter.
        /// </summary>
        /// <param name="allowedPeriod">Allowed peiod to be used</param>
        /// <returns>Min datetime allowed</returns>
        public static DateTime GetDateTimeMinValue(DateTimeAllowedPeriods allowedPeriod)
        {

            // LOCAL VARIABLES
            DateTime minDateTime;


            // MIN VALUE DEPENDS ON THE ALLOWED PERIOD
            switch (allowedPeriod)
            {

                case DateTimeAllowedPeriods.undefinedPast:
                    minDateTime = new DateTime();

                    break;

                case DateTimeAllowedPeriods.pastToPresent:
                    minDateTime = new DateTime(2000, 1, 1, 1, 1, 1);

                    break;

                case DateTimeAllowedPeriods.nearPastToNearFuture:
                    minDateTime = new DateTime(
                        DateTime.Now.Year - 1,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        0, 0, 0);

                    break;

                case DateTimeAllowedPeriods.present:
                case DateTimeAllowedPeriods.presentToNearFuture:
                case DateTimeAllowedPeriods.presentToLongFuture:
                    minDateTime = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        0, 0, 0);

                    break;

                case DateTimeAllowedPeriods.all:
                    minDateTime = DateTime.MinValue;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            
            }

            // RETURN DATETIME
            return minDateTime;

        }


        /// <summary>
        /// Returns the max datetime allowed to the allowed period that is received as a parameter.
        /// </summary>
        /// <param name="allowedPeriod">Allowed period to be used</param>
        /// <returns>Max datetime allowed</returns>
        public static DateTime GetDateTimeMaxValue(DateTimeAllowedPeriods allowedPeriod)
        {

            // LOCAL VARIABLES
            DateTime maxDateTime;


            // MIN VALUE DEPENDS ON THE ALLOWED PERIOD
            switch (allowedPeriod)
            {

                case DateTimeAllowedPeriods.undefinedPast:
                case DateTimeAllowedPeriods.pastToPresent:
                case DateTimeAllowedPeriods.present:
                    maxDateTime = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        23, 59, 59, 999);
                    break;

                case DateTimeAllowedPeriods.nearPastToNearFuture:
                    maxDateTime = new DateTime(
                        DateTime.Now.Year + 1,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        23, 59, 59, 999);
                    break;

                case DateTimeAllowedPeriods.presentToNearFuture:
                    maxDateTime = DateTime.Now.AddYears(1);
                    break;

                case DateTimeAllowedPeriods.presentToLongFuture:
                    maxDateTime = DateTime.Now.AddYears(10);
                    break;

                case DateTimeAllowedPeriods.all:
                    maxDateTime = DateTime.MaxValue;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            
            }

            // RETURN DATETIME
            return maxDateTime;

        }


        /// <summary>
        /// Returns datetime format to be used.
        /// </summary>
        /// <param name="dateTimeFormat">Date time format type</param>
        /// <returns>String format to be used</returns>
        public static string GetDateTimeOutputFormat(DateTimeFormats dateTimeFormat)
        {

            // VARIABLES
            string format;


            // NOTE: DO NOT FORGET THAT REGEX PATTERNS MUST BE UPDATED TOO IF SOME NEW ENUM OPTION IS ADDED


            // FORMAT DEPENDS ON THE DATE TIME FORMAT
            switch (dateTimeFormat)
            {

                case DateTimeFormats.fullDateTime:
                    format = "yyyy-MM-dd HH:mm:ss.fff";

                    break;

                case DateTimeFormats.fullDateTime2:
                    format = "yyyy-MM-dd HH:mm:ss.fffffff";

                    break;

                case DateTimeFormats.dateTime:
                    format = "yyyy-MM-dd HH:mm";

                    break;

                case DateTimeFormats.date:
                    format = "yyyy-MM-dd";

                    break;

                case DateTimeFormats.hour:
                    format = "HH:mm";

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            
            }

            // RETURN PROCESSED FORMAT
            return format;

        }


        /// <summary>
        /// Return a datetime regex pattern.
        /// </summary>
        /// <param name="allowEmptyValue">True if empty values are allowed<</param>
        /// <returns>Regex pattern</returns>
        public static string GetDateTimeInputPattern(bool allowEmptyValue)
        {

            // VARIABLES
            string pattern;


            // INITIALIZE VARIABLES
            pattern = @"([0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])(\ (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]\.[0-9][0-9]?[0-9]?))#1#$";


            // ALLOW EMPTY VALUE
            if (allowEmptyValue)
                pattern = pattern.Replace("#1#", "?");
            else
                pattern = pattern.Replace("#1#", "");


            // RETURN PATTERN
            return pattern;

        }

        #endregion


        #region String
        /// <summary>
        /// Returns the min length allowed to the String Length Type that is received as a parameter.
        /// </summary>
        /// <param name="stringLengthType">String length type to be used</param>
        /// <returns>Min text length allowed</returns>
        public static int GetStringMinLength(StringLengthTypes stringLengthType)
        {
            // Variables
            int minLength;

            // Min length depends on the string content length
            switch (stringLengthType)
            {
                case StringLengthTypes.xSmall:
                case StringLengthTypes.small:
                case StringLengthTypes.medium:
                case StringLengthTypes.large:
                case StringLengthTypes.xLarge:
                case StringLengthTypes.xxLarge:
                case StringLengthTypes.xxxLarge:
                    minLength = 1;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return length
            return minLength;
        }


        /// <summary>
        /// Returns the max length allowed to the String Length Type that is received as a parameter.
        /// </summary>
        /// <param name="stringLengthType">String length type to be used</param>
        /// <returns>Max text length allowed</returns>
        public static int GetStringMaxLength(StringLengthTypes stringLengthType)
        {
            // Variables
            int maxLength;

            // Max length depends on the string content length
            switch (stringLengthType)
            {
                case StringLengthTypes.xSmall:
                    maxLength = 6;

                    break;

                case StringLengthTypes.small:
                    maxLength = 20;

                    break;

                case StringLengthTypes.medium:
                    maxLength = 80;

                    break;

                case StringLengthTypes.large:
                    maxLength = 150;
                    break;

                case StringLengthTypes.xLarge:
                    maxLength = 500;

                    break;

                case StringLengthTypes.xxLarge:
                    maxLength = 2000;

                    break;

                case StringLengthTypes.fourkLarge:
                    maxLength = 4000;

                    break;

                case StringLengthTypes.xxxLarge:
                    maxLength = int.MaxValue;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return length
            return maxLength;
        }



        /// <summary>
        /// Returns a string regex pattern of the string type that is received as a parameter.
        /// </summary>
        /// <param name="stringType">String type to test</param>
        /// <param name="minLength">Min length allowed of the string</param>
        /// <param name="maxLength">Max length allowed of the string</param>
        /// <param name="allowEmptyValue">True if empty values are allowed</param>
        /// <returns>Regex pattern</returns>
        public static string GetStringInputPattern(StringContentTypes stringType, StringLengthTypes stringContentLength, bool allowEmptyValue)
        {

            /* INFO
             * Used this online tool to test http://regexstorm.net/tester
             */

            // Variables
            int minLength, maxLength;
            string pattern;

            // String lengths
            minLength = GlobalVars.GetStringMinLength(stringContentLength);
            maxLength = GlobalVars.GetStringMaxLength(stringContentLength);

            // Pattern to test depends on the string type
            switch (stringType)
            {
                case StringContentTypes.identifier:
                    pattern = @"^([A-Z0-9\.\(\)]#1#)#2#$";      // @"^([A-Z0-9\-\(\)]{2,5})?$";

                    break;

                case StringContentTypes.regularText:            // @"^([A-Za-z0-9_\ \+\*\/\=\.\,\!\:ºª»«}@{\)\(\]\[\?$áàâãÁÀÂÃéèêÉÈÊíìÍÌõôÕÔúùûÚÙÛçÇ#-]#1#)#2#$";          
                    pattern = @"^$|^.*$";

                    break;
                case StringContentTypes.email:
                    pattern = @"^$|^([a-zA-Z0-9_.-]+)@([\da-zA-Z\.-]+)\.([a-zA-Z\.]{2,8})$";

                    break;
                case StringContentTypes.url:
                    pattern = @"^$|^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

                    break;
                case StringContentTypes.htmlTag:
                    pattern = @"^$|^<([a-z]+)([^<]+)*(?:>(.*)<\/\1>|\s+\/>)$";

                    break;
                case StringContentTypes.acronym:
                    pattern = @"^$|^([A-Za-z]#1#)#2#$";

                    break;
                case StringContentTypes.phone:
                    pattern = @"^$|^([a-zA-Z]{0,5});([0-9 ]#1#)#2#$";
                    // Old pattern, replaced by the pattern above due the country prefix before the number, splitted by ";"
                    // @"^$|^(\+?[0-9 ]#1#|[0-9 ]#1#)#2#$";

                    break;
                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Min and max length
            pattern = pattern.Replace("#1#", string.Format("{{{0},{1}}}", minLength.ToString().Trim(), maxLength.ToString().Trim()));

            // Empty value
            if (allowEmptyValue)
                pattern = pattern.Replace("#2#", "?");
            else
                pattern = pattern.Replace("#2#", "");

            // Return final pattern
            return pattern;
        }
        #endregion


        #region Integer
        /// <summary>
        /// Returns the min integer allowed for the Allowed Interval that is received as a parameter.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Min integer allowed</returns>
        public static int GetIntegerMinValue(IntegerAllowedIntervals allowedInterval)
        {
            // Variables
            int minValue;

            // Min value depends on the allowed interval
            switch (allowedInterval)
            {
                case IntegerAllowedIntervals.all:
                case IntegerAllowedIntervals.negativeWithoutZero:
                case IntegerAllowedIntervals.negativeWithZero:
                    minValue = int.MinValue;

                    break;

                case IntegerAllowedIntervals.positiveWithZero:
                case IntegerAllowedIntervals.shortPositiveWithZero:
                case IntegerAllowedIntervals.percentage:
                    minValue = 0;

                    break;

                case IntegerAllowedIntervals.positiveWithoutZero:
                case IntegerAllowedIntervals.shortPositiveWithoutZero:
                    minValue = 1;

                    break;

                case IntegerAllowedIntervals.intIdentifier:
                case IntegerAllowedIntervals.longIdentifier:
                    minValue = -1;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return min value
            return minValue;
        }


        /// <summary>
        /// Returns the max integer allowed for the Allowed Interval that is received as a parameter.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Max integer allowed</returns>
        public static long GetIntegerMaxValue(IntegerAllowedIntervals allowedInterval)
        {
            // Variables
            long maxValue;

            // Min value depends on the allowed interval
            switch (allowedInterval)
            {
                case IntegerAllowedIntervals.all:
                case IntegerAllowedIntervals.positiveWithZero:
                case IntegerAllowedIntervals.positiveWithoutZero:
                case IntegerAllowedIntervals.intIdentifier:
                    maxValue = int.MaxValue;

                    break;

                case IntegerAllowedIntervals.negativeWithoutZero:
                    maxValue = -1;

                    break;

                case IntegerAllowedIntervals.negativeWithZero:
                    maxValue = 0;

                    break;

                case IntegerAllowedIntervals.percentage:
                    maxValue = 100;

                    break;

                case IntegerAllowedIntervals.longIdentifier:
                    maxValue = long.MaxValue;

                    break;

                case IntegerAllowedIntervals.shortPositiveWithoutZero:
                case IntegerAllowedIntervals.shortPositiveWithZero:
                    maxValue = short.MaxValue;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return max value
            return maxValue;
        }


        /// <summary>
        /// Returns the number of digits possible to represent.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Number of possible digits</returns>
        public static int GetIntegerDigits(IntegerAllowedIntervals allowedInterval)
        {
            // Variables
            int numberOfDigits;

            // Min value depends on the allowed interval
            switch (allowedInterval)
            {
                case IntegerAllowedIntervals.all:
                case IntegerAllowedIntervals.negativeWithoutZero:
                case IntegerAllowedIntervals.negativeWithZero:
                case IntegerAllowedIntervals.positiveWithZero:
                case IntegerAllowedIntervals.positiveWithoutZero:
                case IntegerAllowedIntervals.intIdentifier:
                    numberOfDigits = 12;

                    break;

                case IntegerAllowedIntervals.longIdentifier:
                    numberOfDigits = 21;

                    break;

                case IntegerAllowedIntervals.shortPositiveWithoutZero:
                case IntegerAllowedIntervals.shortPositiveWithZero:
                    numberOfDigits = 6;

                    break;

                case IntegerAllowedIntervals.percentage:
                    numberOfDigits = 3;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return number of digits
            return numberOfDigits;
        }


        /// <summary>
        /// Returns integer format to be used.
        /// </summary>
        /// <param name="numberOfDigits">Number of digits to represent the integer number</param>
        /// <returns>String format to be used</returns>
        public static string GetIntegerOutputFormat(int numberOfDigits)
        {
            // Variables
            string format;

            // Build format
            format = new string('#', numberOfDigits - 1);
            format += "0";

            // Return final format;
            return format;
        }


        /// <summary>
        /// Returns an integer regex pattern.
        /// </summary>
        /// <param name="numberOfDigits">Number of digits to represent the integer number</param>
        /// <param name="allowEmptyValue">True if negative values are allowed</param>
        /// <param name="allowEmptyValue">True if empty values are allowed</param>
        /// <returns>Regex pattern</returns>
        /// 
        public static string GetIntegerInputPattern(int numberOfDigits, bool allowNegativeValue, bool allowEmptyValue)
        {
            // Variables
            string pattern = @"^(#1#[0-9]#2#)#3#$";

            // negative value
            if (allowNegativeValue)
                pattern = pattern.Replace("#1#", @"\-?");
            else
                pattern = pattern.Replace("#1#", "");

            // number of digits allowed
            pattern = pattern.Replace("#2#", string.Format("{{1,{0}}}", numberOfDigits.ToString().TrimEnd()));

            // empty value
            if (allowEmptyValue)
                pattern = pattern.Replace("#3#", "?");
            else
                pattern = pattern.Replace("#3#", "");

            // Return final pattern
            return pattern;
        }

        #endregion


        #region Double

        /// <summary>
        /// Returns the min double allowed for the Allowed Interval that is received as a parameter.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Min double allowed</returns>
        public static Decimal GetDecimalMinValue(DoubleAllowedIntervals allowedInterval)
        {
            // Variables
            decimal minValue;

            // Min value depends on the allowed interval
            switch (allowedInterval)
            {
                case DoubleAllowedIntervals.all:
                case DoubleAllowedIntervals.negativeWithoutZero:
                case DoubleAllowedIntervals.negativeWithZero:
                    minValue = Decimal.MinValue;

                    break;

                case DoubleAllowedIntervals.positiveWithZero:
                case DoubleAllowedIntervals.percentage:
                    minValue = 0;

                    break;

                case DoubleAllowedIntervals.positiveWithoutZero:
                    minValue = (decimal)0.0000000001;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return minValue
            return minValue;
        }


        /// <summary>
        /// Returns the max double allowed for the Allowed Interval that is received as a parameter.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Max double allowed</returns>
        public static Decimal GetDecimalMaxValue(DoubleAllowedIntervals allowedInterval)
        {
            // Variables
            decimal maxValue;

            // Min value depends on the allowed interval
            switch (allowedInterval)
            {
                case DoubleAllowedIntervals.all:
                    maxValue = Decimal.MaxValue;

                    break;

                case DoubleAllowedIntervals.negativeWithoutZero:
                    maxValue = (decimal)-0.0000000001;

                    break;

                case DoubleAllowedIntervals.negativeWithZero:
                    maxValue = 0;

                    break;

                case DoubleAllowedIntervals.percentage:
                    maxValue = 100;

                    break;

                case DoubleAllowedIntervals.positiveWithZero:
                case DoubleAllowedIntervals.positiveWithoutZero:
                    maxValue = Decimal.MaxValue;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return minValue
            return maxValue;
        }


        /// <summary>
        /// Returns the number of numbers possible to represent on left side of the decimal dot.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Number of dot left side possible digits</returns>
        public static int GetDoubleDotLeftDigits(DoubleAllowedIntervals allowedInterval)
        {
            // Variables
            int numberOfDigits;

            // Number of digits depends on the allowed interval
            switch (allowedInterval)
            {
                case DoubleAllowedIntervals.all:
                case DoubleAllowedIntervals.negativeWithoutZero:
                case DoubleAllowedIntervals.negativeWithZero:
                case DoubleAllowedIntervals.positiveWithZero:
                case DoubleAllowedIntervals.positiveWithoutZero:
                    numberOfDigits = 20;

                    break;

                case DoubleAllowedIntervals.percentage:
                    numberOfDigits = 3;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return number of dot left side digits
            return numberOfDigits;
        }


        /// <summary>
        /// Returns the number of numbers possible to represent on right side of the decimal dot.
        /// </summary>
        /// <param name="allowedInterval">Allowed parameter to be used</param>
        /// <returns>Number of dot right side possible digits</returns>
        public static int GetDoubleDotRightDigits(DoubleAllowedIntervals allowedInterval)
        {
            // Variables
            int numberOfDigits;

            // Number of digits depends on the allowed interval
            switch (allowedInterval)
            {
                case DoubleAllowedIntervals.all:
                case DoubleAllowedIntervals.negativeWithoutZero:
                case DoubleAllowedIntervals.negativeWithZero:
                case DoubleAllowedIntervals.positiveWithZero:
                case DoubleAllowedIntervals.positiveWithoutZero:
                case DoubleAllowedIntervals.percentage:
                    numberOfDigits = 10;

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.SpecsResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // Return number of dot left side digits
            return numberOfDigits;
        }


        /// <summary>
        /// Returns double format to be used.
        /// </summary>
        /// <param name="numberOfDotLeftDigits">Number of digits to represent on left side ot the dot symbol</param>
        /// <param name="numberOfDotRightDigits">Number of digits to represent on right side ot the dot symbol</param>
        /// <returns>String format to be used</returns>
        public static string GetDoubleOutputFormat(int numberOfDotLeftDigits, int numberOfDotRightDigits)
        {
            // Variables
            string leftSide;
            string rightSide;

            // Left side
            leftSide = new string('#', numberOfDotLeftDigits - 1);
            leftSide += "0";

            // Right side
            rightSide = new string('0', numberOfDotRightDigits);

            // Return final format;
            return string.Format("{0}.{1}", leftSide, rightSide);
        }


        /// <summary>
        /// Returns a double regex pattern.
        /// </summary>
        /// <param name="precision">Number of decimal places allowed</param>
        /// <param name="allowEmptyValue">True if empty values are allowed</param>
        /// <returns>Regex pattern</returns>
        public static string GetDoubleInputPattern(int numberOfDotLeftDigits, int numberOfDotRightDigits, bool allowNegativeValue, bool allowEmptyValue)
        {
            // Variables
            string pattern = @"^#1#([0-9]#2#(\.[0-9]#3#)?)#4#$";

            // Negative value
            if (allowNegativeValue)
                pattern = pattern.Replace("#1#", @"\-?");
            else
                pattern = pattern.Replace("#1#", "");

            // Number of digits on the left side of the decimal dot
            pattern = pattern.Replace("#2#", string.Format("{{0,{0}}}", numberOfDotLeftDigits.ToString().TrimEnd()));

            // Number of digits on the right side of the decimal dot
            pattern = pattern.Replace("#3#", string.Format("{{0,{0}}}", numberOfDotRightDigits.ToString().TrimEnd()));

            // Empty value
            if (allowEmptyValue)
                pattern = pattern.Replace("#4#", "?");
            else
                pattern = pattern.Replace("#4#", "");

            // Return final pattern
            return pattern;
        }


        /// <summary>
        /// Returns the the json decimal representation for decimal place of a double type.
        /// </summary>
        public static NumberFormatInfo GetDoubleJsonDecimalRepresentation()
        {
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            return nfi;
        }


        /// <summary>
        /// 
        /// </summary>
        public static int PageSizeMax => 50;

        #endregion

    }

}

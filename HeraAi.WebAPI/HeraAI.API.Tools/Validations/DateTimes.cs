namespace HeraAI.API.Tools.Validations
{

    public static class DateTimes
    {

        /// <summary>
        /// Returns true if the received value is between min and max value.
        /// </summary>
        /// <param name="value">Value datetime to be tested</param>
        /// <param name="minValue">Min value allowed</param>
        /// <param name="maxValue">Max value allowed</param>
        /// <returns>True if value datetime is inside the allowed interval [min, max]</returns>
        public static bool IsValid(DateTime? value, DateTime minValue, DateTime maxValue)
        {
            
            // VARIABLES
            bool isValid;


            // IF NULL, IS INVALID
            if (value is null)
                return false;


            // INITIALIZE VARIABLES
            isValid = true;


            // CHECK IF VALUE DATETIME IS OUTSIDE OF MIN AND MAX INTERVAL
            if ( value < minValue  || value > maxValue)
                isValid = false;


            // RETURN INFORMATION ABOUT IF IT IS VALID
            return isValid;
        
        }


        /// <summary>
        /// Returns true if the received value is between min and max value.
        /// </summary>
        /// <param name="value">Value datetime to be tested</param>
        /// <param name="isUpdating">If the record is being updated</param>
        /// <param name="minValue">Min value allowed</param>
        /// <param name="maxValue">Max value allowed</param>
        /// <returns>True if value datetime is inside the allowed interval [min, max]</returns>
        public static bool IsValid(DateTime? value, bool isUpdating, DateTime minValue, DateTime maxValue)
        {

            // VARIABLES
            bool isValid;


            // IF NULL, IS INVALID
            if (value is null)
                return false;


            // INITIALIZE VARIABLES
            isValid = true;


            // CHECK IF VALUE DATETIME IS OUTSIDE OF MIN AND MAX INTERVAL
            if (!isUpdating ? (value < minValue || value > maxValue) : value > maxValue)
                isValid = false;


            // RETURN INFORMATION ABOUT IF IT IS VALID
            return isValid;

        }

    }

}

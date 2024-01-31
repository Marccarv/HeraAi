namespace HeraAI.API.Tools.Validations
{

    public static class Integers
    {

        /// <summary>
        /// Return true if the received value is between min and max value.
        /// </summary>
        /// <param name="value">Value to be tested</param>
        /// <param name="minValue">Min value allowed</param>
        /// <param name="maxValue">Max value allowed</param>
        /// <returns>True if value double is inside the allowed interval [min, max]</returns>
        public static bool IsValid(long? value, int minValue, long maxValue)
        {

            // VARIABLES
            bool isValid;


            // IF NULL, IS INVALID
            if (value is null)
                return false;


            // INITIALIZE VARIABLES
            isValid = true;


            // CHECK IF VALUE IS OUTSIDE OF MIN AND MAX INTERVAL
            if (value < minValue || value > maxValue)
                isValid = false;


            // RETURN INFORMATION ABOUT IF IT IS VALID
            return isValid;

        }

    }

}

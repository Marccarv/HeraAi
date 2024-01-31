using System.Text.RegularExpressions;


namespace HeraAI.API.Tools.Validations
{

    public static class Regex
    {

        /// <summary>
        /// Test if content string respects the desired pattern
        /// </summary>
        /// <param name="content">String content to be tested</param>
        /// <param name="pattern">Patter to be tested</param>
        /// <returns>True if content respects pattern</returns>
        public static bool IsValid(string content, string pattern)
        {

            // VARIABLES
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            Match match;


            // VALIDATE IF CONTENT RESPECTS PATTTERN
            match = regex.Match(content);


            // RETURN RESULT
            return match.Success;
        }

    }

}

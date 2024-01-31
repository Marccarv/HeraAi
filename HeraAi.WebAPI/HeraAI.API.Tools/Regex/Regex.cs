using System.Text.RegularExpressions;


namespace HeraAI.API.Tools
{

    public static class Regex
    {

        /// <summary>
        /// Use this method to test if content string respects the desired pattern
        /// </summary>
        /// <param name="content">string content to be tested</param>
        /// <param name="pattern">patter to be tested</param>
        /// <returns>true if content respects pattern</returns>
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

using HeraAI.API.Exceptions;


namespace HeraAI.API.Tools.Validations
{

    public static class Strings
    {

        // READONLY VARIABLES
        private static readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private static readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        public static (int, int) WordMatching(string[] wordsToVerify, string[] allowedWords)
        {

            // VARIABLES
            int match, unMatch;


            match = wordsToVerify.Intersect(allowedWords).Count();
            unMatch = wordsToVerify.Except(allowedWords).Count();


            return (match, unMatch);

        }


        public static void TrimOrder(string order, string[] allowedWords)
        {

            int matchingWords;
            int unMatchingWords;
            string[] orderOptions;


            // PREPARE THE ORDER OPTIONS
            orderOptions = order.Substring(1, order.Length - 1).ToUpper().Split(".");


            (matchingWords, unMatchingWords) = Strings.WordMatching(orderOptions, allowedWords);


            if (matchingWords.Equals(0))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_NO_VALID_OPTION);


            if (!unMatchingWords.Equals(0))
                throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.OTHERS_UNMATCHED_SORT_WORDS);

        }


        /// <summary>
        /// This method returns true if exist at least one of word of the wordsToCheck set int content string
        /// </summary>
        /// <param name="content">string to be tested</param>
        /// <param name="wordsToCheck">set of words to search inside of the string</param>
        /// <returns>true if at least one word of the wordToCheck set is present in content string</returns>
        public static bool HasWords(string content, string[] wordsToCheck)
        {

            // VARIABLES
            bool hasWords;


            // INICIALIZE VARIABLES
            hasWords = false;


            // CHECK IF SOME WORD OF THE LIST IS PRESENT ON THE CONTENT STRING
            for (int i = 0; ((i < wordsToCheck.Length) && (hasWords == false)); i++)
            {

                if (content.ToUpper().Contains(wordsToCheck[i].ToUpper()))
                    hasWords = true;

            }


            // RETURN HASWORDS
            return hasWords;

        }


        /// <summary>
        /// This methods invalidates all dangerous words thar are present on string received as value
        /// (injection code prevention)
        /// </summary>
        /// <param name="value">string that must be processed</param>
        /// <param name="symbolToInvalidateWords">symbol that will be inserted on second letter position to invalidate word</param>
        /// <param name="dangerousWords">list of dangerous words to be searched</param>
        /// <returns>string without dangerous words</returns>
        public static string InvalidateDangerousWords(string value, string symbolToInvalidateWords, string[] dangerousWords)
        {

            // VARIABLES
            string newValue;


            // INITIALIZE VARIABLES
            newValue = value;


            // INVALIDATE DANGEROUS WORDS
            newValue = Strings.ReplaceWords(newValue, symbolToInvalidateWords, dangerousWords);


            // RETURN NEW VALUE
            return newValue;

        }


        /// <summary>
        /// This methods invalidates all dangerous words thar are present on set of strings (array) received as value
        /// (injection code prevention)
        /// </summary>
        /// <param name="value">array of strings that must be processed</param>
        /// <param name="symbolToInvalidateWords">symbol that will be inserted on second letter position to invalidate word</param>
        /// <param name="dangerousWords">list of dangerous words to be searched</param>
        /// <returns>array of strings without dangerous words</returns>
        public static string[] InvalidateDangerousWords(string[] values, string symbolToInvalidateWords, string[] dangerousWords)
        {

            // VARIABLES
            string[] newValues = new string[values.Length];


            // INVALIDATE DANGEROUS WORDS IN THE ARRAY
            for (int i = 0; i < values.Length; i++)
                newValues[i] = Strings.ReplaceWords(values[i], symbolToInvalidateWords, dangerousWords);


            // RETURN NEW VALUE
            return newValues;

        }


        /// <summary>
        /// This method receives a content string and inserts a symbol on second position of each words on wordsToReplace set
        /// </summary>
        /// <param name="content">content string to processed and updated</param>
        /// <param name="symbolToInsert">symbol to insert on second position</param>
        /// <param name="wordsToReplace">set of words to try to match on the content string</param>
        public static void ReplaceWords(ref string content, string symbolToInsert, string[] wordsToReplace)
        {
            content = ReplaceWords(content, symbolToInsert, wordsToReplace);
        }


        /// <summary>
        /// This method receives a content string and inserts a symbol on second position of each words on wordsToReplace set
        /// </summary>
        /// <param name="content">original content string to processed</param>
        /// <param name="symbolToInsert">symbol to insert on second position</param>
        /// <param name="wordsToReplace">set of words to try to match on the content string</param>
        /// <returns>string word words with inserted symbols</returns>
        public static string ReplaceWords(string content, string symbolToInsert, string[] wordsToReplace)
        {

            // VARIABLES
            string contentTextToReplace;


            // INICIALIZE VARIABLES
            contentTextToReplace = content;


            // REPLACE WORDS THAT ARE PRESENT ON THE RECEIVED LIST
            for (int i = 0; i < wordsToReplace.Length; i++)
            {

                // ADD SYMBOL ON SECOND POSITION OF EACH CURRENT DANGEROUS WORD IN CONTENT STRING
                if (contentTextToReplace.ToUpper().Contains(wordsToReplace[i]))
                    AddSymbolToWords(ref contentTextToReplace, symbolToInsert, wordsToReplace[i]);

            }


            // RETURN PROCESSED STRING
            return contentTextToReplace;

        }


        /// <summary>
        /// This method receives a content string and a symbol to the second position of every ocurrence of word.
        /// </summary>
        /// <param name="content">content string to be searched</param>
        /// <param name="symbolToInsert">symbol to add on the second position of the match substring</param>
        /// <param name="word">word to search on the content</param>
        private static void AddSymbolToWords(ref string content, string symbolToInsert, string word)
        {

            // VARIABLES
            int nextIndex;


            // IF WORD HAS ONLY ONE CHAR REPLACE THAT WORD (SYMBOL) BY THE SYMBOLTOINSERT
            if (word.Trim().Length == 1)
            {

                content = content.Replace(word, symbolToInsert);

                return;

            }


            // LOCATE FIRST INDEX
            nextIndex = content.IndexOf(word, StringComparison.InvariantCultureIgnoreCase);


            // IF DOES NOT EXIST WORD IN CONTENT GET OUT
            if (nextIndex < 0)
                return;


            // LOCATE INDEX OF WORDS TO BE MODIFIED
            while (nextIndex >= 0)
            {

                // ADD SYMBOL ONDE NEXT TO THE FIRST LETTER OF WORD (WORD LENGTH GROW UP 1 CHAR)
                content = content.Insert(nextIndex + 1, symbolToInsert);


                // LOCATE NEXT INDEX
                nextIndex = content.IndexOf(word, nextIndex + 1, StringComparison.InvariantCultureIgnoreCase);

            }

        }


        /// <summary>
        /// This method replaces the received textToReplace with all tokens that came on tokens two-dimensional array
        /// </summary>
        /// <param name="textToReplace">text to process</param>
        /// <param name="tokens">two-dimensional array with tokens to replace</param>
        /// <returns>new processed strings</returns>
        public static string ReplaceWithTokens(string textToReplace, string[,] tokens)
        {

            // REPLACE ALL TOKENS 
            for (int i = 0; i < tokens.GetLength(0); i++)
            {
                textToReplace = textToReplace.Replace(tokens[i, 0], tokens[i, 1]);
            }


            // SEND BACK THE PROCESSED STRING
            return textToReplace;

        }

    }

}

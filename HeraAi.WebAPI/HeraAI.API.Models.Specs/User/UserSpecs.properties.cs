using HeraAI.API.Enums;
using HeraAI.API.Models.Specs.Others.Properties;


namespace HeraAI.API.Models.Specs
{

    public partial class UserSpecs
    {

        // VARIABLES
        private static StringProperty id;
        private static StringProperty countryId;
        private static StringProperty language;
        private static StringProperty email;
        private static StringProperty password;
        private static StringProperty firstName;
        private static StringProperty lastName;
        private static StringProperty phone;



        /// <summary>
        /// Id property specs
        /// </summary>
        public static StringProperty Id
        {

            get
            {

                if (id is null)
                {
                    id = new StringProperty("Id", false, StringContentTypes.regularText, StringLengthTypes.xSmall);
                }


                return id;

            }

        }


        /// <summary>
        /// CountryId property specs
        /// </summary>
        public static StringProperty CountryId
        {

            get
            {

                if (countryId is null)
                {
                    countryId = new StringProperty("CountryId", false, StringContentTypes.regularText, StringLengthTypes.xSmall);
                }


                return countryId;

            }

        }


        /// <summary>
        /// Language property specs
        /// </summary>
        public static StringProperty Language
        {

            get
            {

                if (language is null)
                {
                    language = new StringProperty("Language", true, StringContentTypes.regularText, StringLengthTypes.xSmall);
                }


                return language;

            }

        }


        /// <summary>
        /// Email property specs
        /// </summary>
        public static StringProperty Email
        {

            get
            {

                if (email is null)
                {
                    email = new StringProperty("Email", false, StringContentTypes.email, StringLengthTypes.xLarge);
                }


                return email;

            }

        }


        /// <summary>
        /// Password property specs
        /// </summary>
        public static StringProperty Password
        {

            get
            {

                if (password is null)
                {
                    password = new StringProperty("Password", false, StringContentTypes.regularText, StringLengthTypes.large);
                }


                return password;

            }

        }


        /// <summary>
        /// FirstName property specs
        /// </summary>
        public static StringProperty FirstName
        {

            get
            {

                if (firstName is null)
                {
                    firstName = new StringProperty("FirstName", false, StringContentTypes.regularText, StringLengthTypes.small);
                }


                return firstName;

            }

        }


        /// <summary>
        /// LastName property specs
        /// </summary>
        public static StringProperty LastName
        {

            get
            {

                if (lastName is null)
                {
                    lastName = new StringProperty("LastName", false, StringContentTypes.regularText, StringLengthTypes.small);
                }


                return lastName;

            }

        }


        /// <summary>
        /// Phone property specs
        /// </summary>
        public static StringProperty Phone
        {

            get
            {

                if (phone is null)
                {
                    phone = new StringProperty("Phone", true, StringContentTypes.phone, StringLengthTypes.small);
                }


                return phone;

            }

        }

    }

}

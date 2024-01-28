using System;
using HeraAI.API.Enums;
using HeraAI.API.Models.Specs.Others.Properties;

namespace HeraAI.API.Models.Specs
{
	public partial class UserSpecs
    {
        // Variables
        private static StringProperty id;
        private static StringProperty countryId;
        private static StringProperty language;
        private static StringProperty currencyId;
        private static StringProperty unitId;
        private static StringProperty email;
        private static StringProperty password;
        private static StringProperty firstName;
        private static StringProperty lastName;
        private static StringProperty phone;
        private static BoolProperty isDriver;
        private static IntegerProperty driver_NextGoal;
        private static StringProperty driver_IBAN;
        private static StringProperty driver_DeviceName1;
        private static StringProperty driver_DeviceName2;
        private static StringProperty driver_SWIFT_BIC;
        private static StringProperty driver_Address1;
        private static StringProperty driver_Address2;
        private static StringProperty driver_PostalCode;
        private static StringProperty driver_PostalLocation;
        private static StringProperty driver_VAT;
        private static StringProperty driver_BankCountryId;
        private static DateTimeProperty driver_LastLicenseActivationDate;
        private static IntegerProperty driver_NumberOfAvailableLicenses;


        /// <summary>
        /// Id property specs
        /// </summary>
        public static StringProperty Id
        {
            get
            {
                if (id is null)
                {
                    id = new StringProperty("Id",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.xSmall);
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
                    countryId = new StringProperty("CountryId",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.xSmall);
                }

                return countryId;
            }
        }

        /// <summary>
        /// CurrencyId property specs
        /// </summary>
        public static StringProperty CurrencyId
        {
            get
            {
                if (currencyId is null)
                {
                    currencyId = new StringProperty("CurrencyId",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.xSmall);
                }

                return currencyId;
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
                    language = new StringProperty("Language",
                        true,
                        StringContentTypes.regularText,
                        StringLengthTypes.xSmall);
                }

                return language;
            }
        }

        /// <summary>
        /// UnitId property specs
        /// </summary>
        public static StringProperty UnitId
        {
            get
            {
                if (unitId is null)
                {
                    unitId = new StringProperty("UnitId",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.xSmall);
                }

                return unitId;
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
                    email = new StringProperty("Email",
                        false,
                        StringContentTypes.email,
                        StringLengthTypes.xLarge);
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
                    password = new StringProperty("Password",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.large);
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
                    firstName = new StringProperty("FirstName",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.small);
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
                    lastName = new StringProperty("LastName",
                        false,
                        StringContentTypes.regularText,
                        StringLengthTypes.small);
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
                    phone = new StringProperty("Phone",
                        true,
                        StringContentTypes.phone,
                        StringLengthTypes.small);
                }

                return phone;
            }
        }

        /// <summary>
        /// Driver_NextGoal property specs
        /// </summary>
        public static IntegerProperty Driver_NextGoal
        {
            get
            {
                if (driver_NextGoal is null)
                {
                    driver_NextGoal = new IntegerProperty("Driver_NextGoal",
                        true, IntegerAllowedIntervals.all);
                }

                return driver_NextGoal;
            }
        }

        /// <summary>
        /// Driver_IBAN property specs
        /// </summary>
        public static StringProperty Driver_IBAN
        {
            get
            {
                if (driver_IBAN is null)
                {
                    driver_IBAN = new StringProperty("Driver_IBAN",
                        true,
                        StringContentTypes.regularText,
                        StringLengthTypes.large);
                }

                return driver_IBAN;
            }
        }

        /// <summary>
        /// Driver_LastLicenseActivationDate property specs
        /// </summary>
        public static DateTimeProperty Driver_LastLicenseActivationDate
        {
            get
            {
                if (driver_LastLicenseActivationDate is null)
                {
                    driver_LastLicenseActivationDate = new DateTimeProperty("Driver_LastLicenseActivationDate",
                        true,
                        DateTimeAllowedPeriods.all,
                        DateTimeFormats.dateTime);
                }

                return driver_LastLicenseActivationDate;
            }
        }

        /// <summary>
        /// Driver_NumberOfAvailableLicenses property specs
        /// </summary>
        public static IntegerProperty Driver_NumberOfAvailableLicenses
        {
            get
            {
                if (driver_NumberOfAvailableLicenses is null)
                {
                    driver_NumberOfAvailableLicenses = new IntegerProperty("Driver_NumberOfAvailableLicenses",
                        true,
                        IntegerAllowedIntervals.all);
                }

                return driver_NumberOfAvailableLicenses;
            }
        }

   



       

       
    }
}


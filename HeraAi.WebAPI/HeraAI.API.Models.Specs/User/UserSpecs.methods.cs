using HeraAI.API.Enums;
using HeraAI.API.Tools.Validations;


namespace HeraAI.API.Models.Specs
{
	public partial class UserSpecs
    {
        public static User GetNew()
        {
            // variables
            User newUser;

            // build object
            newUser = new User
            {
                Id = "",
                CountryId = "",
                Country = new Country(),
                Language = "",
                CurrencyId = "",
                Currency = new Currency(),
                UnitId = "",
                Unit = new Unit(),
                Email = "",
                Password = "",
                FirstName = "",
                LastName = "",
                Phone = "",
                IsDriver = false,
                Driver_NextGoal = 0,
                Driver_IBAN = "",
                Driver_SerialNumber1 = new Guid(),
                Driver_SerialNumber2 = new Guid(),
                Driver_DeviceName1 = "",
                Driver_DeviceName2 = "",
                Driver_LastLicenseActivationDate = new DateTime(),
                Driver_NumberOfAvailableLicenses = 0,
                Inactive = false,
                Updating = false
            };

            // return new object
            return newUser;
        }


        public static void InvalidateDangerousWords(ref User obj, string[] dangerousWords, string symbolToInvalidateWords)
        {
            // variables
            string value;

            // check all object properties
            foreach (System.Reflection.PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                object checkNull = propertyInfo.GetValue(obj, null);
                // but only process the writable string properties
                if (propertyInfo.PropertyType.Equals(typeof(string)) && propertyInfo.CanWrite && checkNull != null)
                {
                    // get the current value
                    value = propertyInfo?.GetValue(obj)?.ToString() ?? "";

                    // invalidate dangerous words
                    value = Strings.InvalidateDangerousWords(value, symbolToInvalidateWords, dangerousWords);

                    // set new value
                    propertyInfo.SetValue(obj, value);
                }
            }
        }



        public static bool IsValid(User user, DataOperation dataOperation, ref string info)
        {
            // variables
            string message = "";

            switch (dataOperation)
            {
                case DataOperation.Insert:

                    // Property values validation
                    if (user.Language != null)
                        UserSpecs.Language.IsValid(user.Language, ref message);
                    if (user.CountryId != null)
                        UserSpecs.CountryId.IsValid(user.CountryId, ref message);
                    if (user.CurrencyId != null)
                        UserSpecs.CurrencyId.IsValid(user.CurrencyId, ref message);
                    if (user.UnitId != null)
                        UserSpecs.UnitId.IsValid(user.UnitId, ref message);
                    if (user.Phone != null)
                        UserSpecs.Phone.IsValid(user.Phone, ref message);

                        UserSpecs.LastName.IsValid(user.LastName, ref message);
                        UserSpecs.FirstName.IsValid(user.FirstName, ref message);

                    if (user.Driver_NextGoal != null)
                        UserSpecs.Driver_NextGoal.IsValid(user.Driver_NextGoal, ref message);
                    if (user.Driver_IBAN != null)
                        UserSpecs.Driver_IBAN.IsValid(user.Driver_IBAN, ref message);


                    UserSpecs.Email.IsValid(user.Email, ref message);
                    UserSpecs.Password.IsValid(user.Password, ref message);
                    if (user.IsDriver != null)
                        UserSpecs.IsDriver.IsValid(user.IsDriver, ref message);

                    //  Verify the existence of inconsistency between properties
                    if (user.Updating)
                        message += Resources.Resources.SpecsResources.UPDATING_PROPERTY_VALUE_INCONSISTENCY;


                    break;
                case DataOperation.Update:

                    // Property values validation
                    if (user.Language != null)
                        UserSpecs.Language.IsValid(user.Language, ref message);
                    if (user.CountryId != null)
                        UserSpecs.CountryId.IsValid(user.CountryId, ref message);
                    if (user.CurrencyId != null)
                        UserSpecs.CurrencyId.IsValid(user.CurrencyId, ref message);
                    if (user.UnitId != null)
                        UserSpecs.UnitId.IsValid(user.UnitId, ref message);
                    if (user.Phone != null)
                        UserSpecs.Phone.IsValid(user.Phone, ref message);
                    UserSpecs.LastName.IsValid(user.LastName, ref message);
                    UserSpecs.FirstName.IsValid(user.FirstName, ref message);
                    //UserSpecs.IsDriver.IsValid(user.IsDriver, ref message);
                    if (user.Driver_NextGoal != null)
                        UserSpecs.Driver_NextGoal.IsValid(user.Driver_NextGoal, ref message);
                    if (user.Driver_IBAN != null)
                        UserSpecs.Driver_IBAN.IsValid(user.Driver_IBAN, ref message);
                    if (user.Driver_SWIFT_BIC != null)
                        UserSpecs.Driver_SWIFT_BIC.IsValid(user.Driver_SWIFT_BIC, ref message);
                    if (user.Driver_Address1 != null)
                        UserSpecs.Driver_Address1.IsValid(user.Driver_Address1, ref message);
                    if (user.Driver_Address2 != null)
                        UserSpecs.Driver_Address2.IsValid(user.Driver_Address2, ref message);
                    if (user.Driver_PostalCode != null)
                        UserSpecs.Driver_PostalCode.IsValid(user.Driver_PostalCode, ref message);
                    if (user.Driver_PostalLocation != null)
                        UserSpecs.Driver_PostalLocation.IsValid(user.Driver_PostalLocation, ref message);
                    if (user.Driver_VAT != null)
                        UserSpecs.Driver_VAT.IsValid(user.Driver_VAT, ref message);
                    if (user.Driver_BankCountryId != null)
                        UserSpecs.Driver_BankCountryId.IsValid(user.Driver_BankCountryId, ref message);

                    UserSpecs.LastUpdate.IsValid(user.LastUpdate, ref message);
  
                    break;
                case DataOperation.Delete:
                    // Adicional validations
                    UserSpecs.CreationDate.IsValid(user.CreationDate, ref message);
                    UserSpecs.LastUpdate.IsValid(user.LastUpdate, ref message);

                    if (!user.Updating)
                        message += string.Format("{0}",
                            Resources.Resources.SpecsResources.UPDATING_PROPERTY_VALUE_INCONSISTENCY);

                    break;
                case DataOperation.Select:

                    break;
                default:
                    break;
            }

            // if there are messages to report, object is not valid and sends report to a exception
            if (message.Length > 0)
            {
                info += message;
                return false;
            }

            // object is valid
            return true;

        }
    }
}


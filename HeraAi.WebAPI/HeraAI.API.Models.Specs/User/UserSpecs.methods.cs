using HeraAI.API.Enums;
using HeraAI.API.Tools.Validations;


namespace HeraAI.API.Models.Specs
{
	public partial class UserSpecs
    {
        public static User GetNew()
        {

            // VARIABLES
            User newUser;


            // BUILD OBJECT
            newUser = new User
            {
                Id = "",
                CountryId = "",
                Language = "",
                Email = "",
                Password = "",
                FirstName = "",
                LastName = "",
                Phone = "",
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
               
                    if (user.Phone != null)
                        UserSpecs.Phone.IsValid(user.Phone, ref message);

                        UserSpecs.LastName.IsValid(user.LastName, ref message);
                        UserSpecs.FirstName.IsValid(user.FirstName, ref message);

        


                    UserSpecs.Email.IsValid(user.Email, ref message);
                    UserSpecs.Password.IsValid(user.Password, ref message);
                 

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
                    if (user.Phone != null)
                        UserSpecs.Phone.IsValid(user.Phone, ref message);
                    UserSpecs.LastName.IsValid(user.LastName, ref message);
                    UserSpecs.FirstName.IsValid(user.FirstName, ref message);
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


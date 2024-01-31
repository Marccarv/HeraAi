namespace HeraAI.API.Models.Specs.Others.Properties
{

    public partial class BoolProperty
    {

        /// <summary>
        /// Validates if the received value is valid
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <param name="info">Information on why the validation was not successful</param>
        /// <returns>True if object is valid</returns>
        public bool IsValid(bool? value, ref string info)
        {

            // VALIDATE EMPTY VALUE
            if (!this.AllowEmptyValue && value is null)
            {
                
                info += string.Format("{0}: {1};", this.Name, Resources.Resources.SpecsResources.FORBIDDEN_NULL);
               
                return false;

            }


            // RETURN TRUE
            return true;

        }

    }

}
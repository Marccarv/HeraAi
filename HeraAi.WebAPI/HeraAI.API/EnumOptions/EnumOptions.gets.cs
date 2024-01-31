using HeraAI.API.Models;


namespace HeraAI.API
{

    public partial class EnumOptions
    {
    
        public List<EnumOption> GetIdioms()
        {

            List<EnumOption> enumOptions = new List<EnumOption>
            {
                new EnumOption { Key = "en-US", Value = "English" },
                new EnumOption { Key = "pt-PT", Value = "Portuguese" },
                new EnumOption { Key = "fr-FR", Value = "French" },
                new EnumOption { Key = "es-ES", Value = "Spanish" },
            };


            // RETURN THE LIST OF OBJECTS
            return enumOptions;

        }

    }

}

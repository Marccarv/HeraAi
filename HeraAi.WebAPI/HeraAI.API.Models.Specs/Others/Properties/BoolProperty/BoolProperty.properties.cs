namespace HeraAI.API.Models.Specs.Others.Properties
{

    public partial class BoolProperty
    {

        // VARIABLES
        private readonly string name;
        private readonly bool allowEmptyValue;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Property name</param>
        /// <param name="allowEmptyValue">True if not mandatory</param>
        public BoolProperty(string name, bool allowEmptyValue)
        {
        
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.allowEmptyValue = allowEmptyValue;
        
        }


        public string Name { get => name; }


        public bool AllowEmptyValue { get => allowEmptyValue; }

    }

}

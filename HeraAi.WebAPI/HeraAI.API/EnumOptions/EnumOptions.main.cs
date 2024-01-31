namespace HeraAI.API
{

    public partial class EnumOptions : BaseEntity
    {

        // READONLY VARIABLES
        private readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        public EnumOptions(Engine engine)
        {
            this.engine = engine;
        }

    }

}

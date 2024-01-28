namespace HeraAI.API.Models.Specs
{

	public partial class UserSpecs : BaseEntitySpecs
	{

		// READONLY VARIABLES
		private static readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
		private static readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;

	}

}

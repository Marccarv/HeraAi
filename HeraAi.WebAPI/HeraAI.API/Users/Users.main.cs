using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraAI.API
{
    public partial class Users : BaseEntity
    {
        // Readonly variables
        private readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        public Users(Engine engine)
        {
            this.engine = engine;
        }
    }
}

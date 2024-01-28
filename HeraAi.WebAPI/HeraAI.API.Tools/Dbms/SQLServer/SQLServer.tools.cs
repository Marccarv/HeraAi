using System.Data.SqlClient;


namespace HeraAI.API.Tools.Dbms
{

    public partial class SQLServer
    {

        public static bool FieldExists(ref SqlDataReader sqlDataReader, string fieldToSearch)
        {

            bool found = false;

            for (int i = 0; i < sqlDataReader.FieldCount && !found; i++)
                if (sqlDataReader.GetName(i).Equals(fieldToSearch))
                    found = true;

            return found;

        }

    }

}

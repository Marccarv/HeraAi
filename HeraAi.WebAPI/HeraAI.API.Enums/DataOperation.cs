using System.Runtime.Serialization;


namespace HeraAI.API.Enums
{

    public enum DataOperation { Insert, Update, Delete, Select }


    public enum PayloadTypes
    {

        [EnumMember(Value = "LOAD")]
        LOAD,

        [EnumMember(Value = "UNLOAD")]
        UNLOAD

    }

}
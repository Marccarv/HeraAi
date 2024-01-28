using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraAI.API
{
    public partial class Users : BaseEntity
    {
        private readonly string SP_INSERT = @"uSP_UsersInsert";
        private readonly string SP_INSERT03 = @"uSP_UsersInsert03";
        private readonly string SP_UPDATE = @"uSP_UsersUpdate";
        private readonly string SP_UPDATE_USER = @"uSP_UsersUpdate02";
        private readonly string SP_UPDATENEXTGOAL = @"uSP_UsersUpdateNextGoal";
        
        private readonly string SP_UPDATE03 = @"uSP_UsersUpdate03";
        private readonly string SP_DELETE = @"uSP_UsersDelete";
        private readonly string SP_SELECT_USERS_AUTHENTICATION01 = @"uSP_UsersSelect_Authentication01";
        private readonly string SP_SELECT_USERS_AUTHENTICATION02 = @"uSP_UsersSelect_Authentication02";
        private readonly string SP_SELECT_USERS_AUTHENTICATION03 = @"uSP_UsersSelect_Authentication03";
        private readonly string SP_SELECT_USERS_AUTHENTICATION04 = @"uSP_UsersSelect_Authentication04";
        private readonly string SP_SELECT_USERS_AUTHENTICATION05 = @"uSP_UsersSelect_Authentication05";
        private readonly string SP_SELECT_USERS_AUTHENTICATION06 = @"uSP_UsersSelect_Authentication06";
        private readonly string SP_COUNT_USERS01 = @"uSP_UsersSelect_Count01";
        private readonly string SP_COUNT_USERS02 = @"uSP_UsersSelect_Count02";
        private readonly string SP_COUNT_USERS03 = @"uSP_UsersSelect_Count03";
        private readonly string SP_UPDATE_SERIALNUMBER01 = @"uSP_UsersUpdateSerialNumber01";
        private readonly string SP_UPDATE_SERIALNUMBER02 = @"uSP_UsersUpdateSerialNumber02";
        private readonly string SP_SELECT_GET_DRIVERWITHDRAWAL01 = @"uSP_Users_Withdrawal01";

        private readonly string SP_SELECT_GET_DETAILS01 = @"uSP_UsersSelect_Details01";
        private readonly string SP_SELECT_GET_DETAILS03 = @"uSP_UsersSelect_Details03";
        private readonly string SP_SELECT_GET_DETAILS04 = @"uSP_UsersSelect_Details04";
        private readonly string SP_SELECT_GET_DETAILS05 = @"uSP_UsersSelect_Details05";
        private readonly string SP_SELECT_GET_DETAILS06 = @"uSP_UsersSelect_Details06";
        private readonly string SP_SELECT_GET_INTERNAL = @"uSP_UsersSelect_Internal01";
        private readonly string SP_SELECT_GET_DEVICENAMES = @"uSP_UsersSelect_Devices01";
        private readonly string SP_SELECT_GET_DEFAULT_USERS = @"uSP_UsersSelect_DefaultUsers01";
        private readonly string SP_SELECT_GET_DEFAULT_USERS03 = @"uSP_UsersSelect_DefaultUsers03";
        private readonly string SP_SELECT_GET_SUMMARY01 = @"uSP_UsersSelect_Summary01";
        private readonly string SP_SELECT_GET_SUMMARY02 = @"uSP_UsersSelect_Summary02";
        private readonly string SP_SELECT_GET_SUMMARY03 = @"uSP_UsersSelect_Summary03";
        private readonly string SP_SELECT_GET_DRIVERS01 = @"uSP_UsersSelect_Drivers01";

        private readonly string SP_SELECT_GET_SEARCH01 = @"uSP_UsersSelect_Search01";

        private readonly string[] ORDER_OPTIONS =
        {
                "AU-FIRSTNAME",
                "DU-FIRSTNAME",
                "AU-LASTNAME",
                "DU-LASTNAME",
                "AU-EMAIL",
                "DU-EMAIL",
                "AC-NAME",
                "DC-NAME",
                "ACUR-DESCRIPTION",
                "DCUR-DESCRIPTION",
                "AUN-DESCRIPTION",
                "DUN-DESCRIPTION",
                "AU-ID",
                "DU-ID"
        };
    }
}

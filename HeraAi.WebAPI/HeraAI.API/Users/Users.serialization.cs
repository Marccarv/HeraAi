using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Exceptions;
using HeraAI.API.Models;
using HeraAI.API.Models.Specs;
using HeraAI.API.Tools;
using HeraAI.API.Tools.Dbms;

namespace HeraAI.API
{
    public partial class Users
    {
        private User Deserialize(ref SqlDataReader sqlDataReader)
        {
            // variables
            User user;

            // fill the object properties with data
            user = UserSpecs.GetNew();
            user.Id = SQLServer.FieldExists(ref sqlDataReader, "Id") ? ((string)sqlDataReader["Id"]).TrimEnd() : "";
            user.CountryId = (SQLServer.FieldExists(ref sqlDataReader, "CountryId") && sqlDataReader["CountryId"] != DBNull.Value) ? ((string)sqlDataReader["CountryId"]).TrimEnd() : "";
            user.Language = (SQLServer.FieldExists(ref sqlDataReader, "Language") && sqlDataReader["Language"] != DBNull.Value) ? ((string)sqlDataReader["Language"]).TrimEnd() : "";
            user.Email = SQLServer.FieldExists(ref sqlDataReader, "Email") ? ((string)sqlDataReader["Email"]).TrimEnd() : "";
            user.Password = SQLServer.FieldExists(ref sqlDataReader, "Password") ? ((string)sqlDataReader["Password"]).TrimEnd() : "";
            user.FirstName = SQLServer.FieldExists(ref sqlDataReader, "FirstName") ? ((string)sqlDataReader["FirstName"]).TrimEnd() : "";
            user.LastName = SQLServer.FieldExists(ref sqlDataReader, "LastName") ? ((string)sqlDataReader["LastName"]).TrimEnd() : "";
            user.Phone = (SQLServer.FieldExists(ref sqlDataReader, "Phone") && sqlDataReader["Phone"] != DBNull.Value) ? ((string)sqlDataReader["Phone"]).TrimEnd() : "";
            user.Inactive = SQLServer.FieldExists(ref sqlDataReader, "Inactive") ? (bool)sqlDataReader["Inactive"] : null;
            user.CreationDate = SQLServer.FieldExists(ref sqlDataReader, "CreationDate") ? (DateTime)sqlDataReader["CreationDate"] : null;
            user.LastUpdate = SQLServer.FieldExists(ref sqlDataReader, "LastUpdate") ? (DateTime)sqlDataReader["LastUpdate"] : null;
            user.LastUserId = SQLServer.FieldExists(ref sqlDataReader, "LastUserId") ? ((string)sqlDataReader["LastUserId"]).TrimEnd() : null;
            user.MetaInfo.Add("RoleName", (SQLServer.FieldExists(ref sqlDataReader, "RoleName") && sqlDataReader["RoleName"] != DBNull.Value) ? ((string)sqlDataReader["RoleName"]).TrimEnd() : "");
            user.MetaInfo.Add("InvitationStatus", (SQLServer.FieldExists(ref sqlDataReader, "InvitationStatus") && sqlDataReader["InvitationStatus"] != DBNull.Value) ? ((string)sqlDataReader["InvitationStatus"]).TrimEnd() : "");
            user.Updating = true;


            user.Password = Settings.Decrypt(user.Password);

            // return the new created object
            return user;
        }


        private List<SqlParameter> Serialize(User user, string userId, string? customerId, DataOperation dataOperation)
        {
            // variables
            List<SqlParameter> sqlParameters;

            /* INFO
             * If you want to send a NULL value to a stored procedure first check if the parameter is actually null. If the parameter is null send it with DBNull.Value
             */

            // sql parameters depends on sqlDataOperation
            switch (dataOperation)
            {

                case DataOperation.Insert:
                    // fill insert parameters
                    sqlParameters = new List<SqlParameter>
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@userCustomerId", customerId != null ? customerId : DBNull.Value),
                        new SqlParameter("@countryId", user.CountryId != null ? user.CountryId : DBNull.Value),
                        new SqlParameter("@language", user.Language != null ? user.Language : DBNull.Value),
                        new SqlParameter("@email", user.Email),
                        new SqlParameter("@password", Settings.Encrypt(user.Password)),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@inactive", false),
                        new SqlParameter("@lastUserId", userId != null ? userId : DBNull.Value),
                        new SqlParameter
                        {
                            ParameterName = "@newId",
                            Direction = System.Data.ParameterDirection.Output,
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Size = 6
                        }
                    };

                    break;

                case DataOperation.Update:
                    // fill update parameters 
                    sqlParameters = new List<SqlParameter>
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@userCustomerId", customerId != null ? customerId : DBNull.Value),
                        new SqlParameter("@language", user.Language),
                        new SqlParameter("@id", user.Id),
                        new SqlParameter("@countryId", user.CountryId),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@lastUpdate", user.LastUpdate),
                        new SqlParameter("@lastUserId", userId),
                        new SqlParameter
                        {
                            ParameterName = "@newLastUpdateOtp",
                            Direction = System.Data.ParameterDirection.Output,
                            SqlDbType = System.Data.SqlDbType.DateTime
                        }
                    };

                    break;

                case DataOperation.Delete:
                    // fill delete parameters 
                    sqlParameters = new List<SqlParameter>
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@userCustomerId", customerId),
                        new SqlParameter("@id", user.Id),
                        new SqlParameter("@lastUpdate", user.LastUpdate)
                    };

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // return the new created list of sql parameters
            return sqlParameters;
        }

        private List<SqlParameter> SerializeMobile(User user, string userId, DataOperation dataOperation)
        {
            // variables
            List<SqlParameter> sqlParameters;

            /* INFO
             * If you want to send a NULL value to a stored procedure first check if the parameter is actually null. If the parameter is null send it with DBNull.Value
             */

            // sql parameters depends on sqlDataOperation
            switch (dataOperation)
            {

                case DataOperation.Insert:
                    // fill insert parameters
                    sqlParameters = new List<SqlParameter>
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@countryId", user.CountryId != null ? user.CountryId : DBNull.Value),
                        new SqlParameter("@language", user.Language != null ? user.Language : DBNull.Value),
                        new SqlParameter("@email", user.Email),
                        new SqlParameter("@password", Settings.Encrypt(user.Password)),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@inactive", user.Inactive),
                        new SqlParameter("@lastUserId", user.LastUserId),
                        new SqlParameter
                        {
                            ParameterName = "@newId",
                            Direction = System.Data.ParameterDirection.Output,
                            SqlDbType = System.Data.SqlDbType.NVarChar,
                            Size = 6
                        }
                    };

                    break;

                case DataOperation.Update:
                    // fill update parameters 
                    sqlParameters = new List<SqlParameter>
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@language", user.Language),
                        new SqlParameter("@id", user.Id),
                        new SqlParameter("@countryId", user.CountryId),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@lastUpdate", user.LastUpdate),
                        new SqlParameter("@lastUserId", userId),
                        new SqlParameter
                        {
                            ParameterName = "@newLastUpdateOtp",
                            Direction = System.Data.ParameterDirection.Output,
                            SqlDbType = System.Data.SqlDbType.DateTime
                        }
                    };

                    break;

                case DataOperation.Delete:
                    // fill delete parameters 
                    sqlParameters = new List<SqlParameter>
                    {
                        new SqlParameter("@userId", userId),
                        new SqlParameter("@id", user.Id),
                        new SqlParameter("@lastUpdate", user.LastUpdate)
                    };

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // return the new created list of sql parameters
            return sqlParameters;
        }

    }

}

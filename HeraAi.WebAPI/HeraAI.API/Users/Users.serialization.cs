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
            user.CurrencyId = (SQLServer.FieldExists(ref sqlDataReader, "CurrencyId") && sqlDataReader["CurrencyId"] != DBNull.Value) ? ((string)sqlDataReader["CurrencyId"]).TrimEnd() : "";
            user.UnitId = (SQLServer.FieldExists(ref sqlDataReader, "UnitId") && sqlDataReader["UnitId"] != DBNull.Value) ? ((string)sqlDataReader["UnitId"]).TrimEnd() : "";
            user.Email = SQLServer.FieldExists(ref sqlDataReader, "Email") ? ((string)sqlDataReader["Email"]).TrimEnd() : "";
            user.Password = SQLServer.FieldExists(ref sqlDataReader, "Password") ? ((string)sqlDataReader["Password"]).TrimEnd() : "";
            user.FirstName = SQLServer.FieldExists(ref sqlDataReader, "FirstName") ? ((string)sqlDataReader["FirstName"]).TrimEnd() : "";
            user.LastName = SQLServer.FieldExists(ref sqlDataReader, "LastName") ? ((string)sqlDataReader["LastName"]).TrimEnd() : "";
            user.Phone = (SQLServer.FieldExists(ref sqlDataReader, "Phone") && sqlDataReader["Phone"] != DBNull.Value) ? ((string)sqlDataReader["Phone"]).TrimEnd() : "";
            user.IsDriver = SQLServer.FieldExists(ref sqlDataReader, "IsDriver") ? ((bool)sqlDataReader["IsDriver"]) : null;
            user.Driver_NextGoal = (SQLServer.FieldExists(ref sqlDataReader, "Driver_NextGoal") && sqlDataReader["Driver_NextGoal"] != DBNull.Value) ? (int)sqlDataReader["Driver_NextGoal"] : null;
            user.Driver_IBAN = (SQLServer.FieldExists(ref sqlDataReader, "Driver_IBAN") && sqlDataReader["Driver_IBAN"] != DBNull.Value) ? ((string)sqlDataReader["Driver_IBAN"]).TrimEnd() : "";
            user.Driver_SerialNumber1 = (SQLServer.FieldExists(ref sqlDataReader, "Driver_SerialNumber1") && sqlDataReader["Driver_SerialNumber1"] != DBNull.Value) ? (Guid)sqlDataReader["Driver_SerialNumber1"] : null;
            user.Driver_SerialNumber2 = (SQLServer.FieldExists(ref sqlDataReader, "Driver_SerialNumber2") && sqlDataReader["Driver_SerialNumber2"] != DBNull.Value) ? (Guid)sqlDataReader["Driver_SerialNumber2"] : null;
            user.Driver_DeviceName1 = (SQLServer.FieldExists(ref sqlDataReader, "Driver_DeviceName1") && sqlDataReader["Driver_DeviceName1"] != DBNull.Value) ? ((string)sqlDataReader["Driver_DeviceName1"]).TrimEnd() : null;
            user.Driver_DeviceName2 = (SQLServer.FieldExists(ref sqlDataReader, "Driver_DeviceName2") && sqlDataReader["Driver_DeviceName2"] != DBNull.Value) ? ((string)sqlDataReader["Driver_DeviceName2"]).TrimEnd() : null;
            user.Driver_LastLicenseActivationDate = (SQLServer.FieldExists(ref sqlDataReader, "Driver_LastLicenseActivationDate") && sqlDataReader["Driver_LastLicenseActivationDate"] != DBNull.Value) ? (DateTime)sqlDataReader["Driver_LastLicenseActivationDate"] : null;
            user.Driver_NumberOfAvailableLicenses = (SQLServer.FieldExists(ref sqlDataReader, "Driver_NumberOfAvailableLicenses") && sqlDataReader["Driver_NumberOfAvailableLicenses"] != DBNull.Value) ? (int)sqlDataReader["Driver_NumberOfAvailableLicenses"] : 0;
            user.Driver_SWIFT_BIC = (SQLServer.FieldExists(ref sqlDataReader, "Driver_SWIFT_BIC") && sqlDataReader["Driver_SWIFT_BIC"] != DBNull.Value) ? (string)sqlDataReader["Driver_SWIFT_BIC"] : null;
            user.Driver_Address1 = (SQLServer.FieldExists(ref sqlDataReader, "Driver_Address1") && sqlDataReader["Driver_Address1"] != DBNull.Value) ? (string)sqlDataReader["Driver_Address1"] : null;
            user.Driver_Address2 = (SQLServer.FieldExists(ref sqlDataReader, "Driver_Address2") && sqlDataReader["Driver_Address2"] != DBNull.Value) ? ((string)sqlDataReader["Driver_Address2"]).TrimEnd() : null;
            user.Driver_PostalCode = (SQLServer.FieldExists(ref sqlDataReader, "Driver_PostalCode") && sqlDataReader["Driver_PostalCode"] != DBNull.Value) ? (string)sqlDataReader["Driver_PostalCode"] : null;
            user.Driver_PostalLocation = (SQLServer.FieldExists(ref sqlDataReader, "Driver_PostalLocation") && sqlDataReader["Driver_PostalLocation"] != DBNull.Value) ? (string)sqlDataReader["Driver_PostalLocation"] : null;
            user.Driver_VAT = (SQLServer.FieldExists(ref sqlDataReader, "Driver_VAT") && sqlDataReader["Driver_VAT"] != DBNull.Value) ? ((string)sqlDataReader["Driver_VAT"]).TrimEnd() : null;
            user.Driver_BankCountryId = (SQLServer.FieldExists(ref sqlDataReader, "Driver_BankCountryId") && sqlDataReader["Driver_BankCountryId"] != DBNull.Value) ? ((string)sqlDataReader["Driver_BankCountryId"]).TrimEnd() : null;
            user.Inactive = SQLServer.FieldExists(ref sqlDataReader, "Inactive") ? (bool)sqlDataReader["Inactive"] : null;
            user.CreationDate = SQLServer.FieldExists(ref sqlDataReader, "CreationDate") ? (DateTime)sqlDataReader["CreationDate"] : null;
            user.LastUpdate = SQLServer.FieldExists(ref sqlDataReader, "LastUpdate") ? (DateTime)sqlDataReader["LastUpdate"] : null;
            user.LastUserId = SQLServer.FieldExists(ref sqlDataReader, "LastUserId") ? ((string)sqlDataReader["LastUserId"]).TrimEnd() : null;
            user.MetaInfo.Add("RoleName", (SQLServer.FieldExists(ref sqlDataReader, "RoleName") && sqlDataReader["RoleName"] != DBNull.Value) ? ((string)sqlDataReader["RoleName"]).TrimEnd() : "");
            user.MetaInfo.Add("InvitationStatus", (SQLServer.FieldExists(ref sqlDataReader, "InvitationStatus") && sqlDataReader["InvitationStatus"] != DBNull.Value) ? ((string)sqlDataReader["InvitationStatus"]).TrimEnd() : "");
            user.Updating = true;

            user.MetaInfo.Add("IsNotifiedOnDraftUpdate", (SQLServer.FieldExists(ref sqlDataReader, "IsNotifiedOnDraftUpdate") && sqlDataReader["IsNotifiedOnDraftUpdate"] != DBNull.Value) ? (bool)sqlDataReader["IsNotifiedOnDraftUpdate"] : false);
            user.MetaInfo.Add("IsNotifiedOnDraftFinish", (SQLServer.FieldExists(ref sqlDataReader, "IsNotifiedOnDraftFinish") && sqlDataReader["IsNotifiedOnDraftFinish"] != DBNull.Value) ? (bool)sqlDataReader["IsNotifiedOnDraftFinish"] : false);
            user.MetaInfo.Add("IsNotifiedOnTicketGenerate", (SQLServer.FieldExists(ref sqlDataReader, "IsNotifiedOnTicketGenerate") && sqlDataReader["IsNotifiedOnTicketGenerate"] != DBNull.Value) ? (bool)sqlDataReader["IsNotifiedOnTicketGenerate"] : false);
            user.MetaInfo.Add("CanSeeWeightValue", (SQLServer.FieldExists(ref sqlDataReader, "CanSeeWeightValue") && sqlDataReader["CanSeeWeightValue"] != DBNull.Value) ? (bool)sqlDataReader["CanSeeWeightValue"] : false);
            user.MetaInfo.Add("CanShareDraft", (SQLServer.FieldExists(ref sqlDataReader, "CanShareDraft") && sqlDataReader["CanShareDraft"] != DBNull.Value) ? (bool)sqlDataReader["CanShareDraft"] : false);
            user.MetaInfo.Add("CanShareTicket", (SQLServer.FieldExists(ref sqlDataReader, "CanShareTicket") && sqlDataReader["CanShareTicket"] != DBNull.Value) ? (bool)sqlDataReader["CanShareTicket"] : false);
            user.MetaInfo.Add("IsActive", (SQLServer.FieldExists(ref sqlDataReader, "IsActive") && sqlDataReader["IsActive"] != DBNull.Value) ? (bool)sqlDataReader["IsActive"] : false);
            user.MetaInfo.Add("Type", (SQLServer.FieldExists(ref sqlDataReader, "Type") && sqlDataReader["Type"] != DBNull.Value) ? ((string)sqlDataReader["Type"]).TrimEnd() : "");



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
                        new SqlParameter("@currencyId", user.CurrencyId != null ? user.CurrencyId : DBNull.Value),
                        new SqlParameter("@language", user.Language != null ? user.Language : DBNull.Value),
                        new SqlParameter("@unitId", user.UnitId != null ? user.UnitId : DBNull.Value),
                        new SqlParameter("@email", user.Email),
                        new SqlParameter("@password", Settings.Encrypt(user.Password)),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@isDriver", user.IsDriver),
                        new SqlParameter("@driver_NextGoal", user.Driver_NextGoal != null ? user.Driver_NextGoal : DBNull.Value),
                        new SqlParameter("@driver_IBAN", user.Driver_IBAN != null ? user.Driver_IBAN : DBNull.Value),
                        new SqlParameter("@driver_SerialNumber1", user.Driver_SerialNumber1 != null ? user.Driver_SerialNumber1 : DBNull.Value),
                        new SqlParameter("@driver_SerialNumber2", user.Driver_SerialNumber2 != null ? user.Driver_SerialNumber2 : DBNull.Value),
                        new SqlParameter("@driver_DeviceName1", user.Driver_DeviceName1 != null ? user.Driver_DeviceName1 : DBNull.Value),
                        new SqlParameter("@driver_DeviceName2", user.Driver_DeviceName2 != null ? user.Driver_DeviceName2 : DBNull.Value),
                        new SqlParameter("@driver_LastLicenseActivationDate", user.Driver_LastLicenseActivationDate != null ? user.Driver_LastLicenseActivationDate : DBNull.Value),
                        new SqlParameter("@driver_NumberOfAvailableLicenses", user.Driver_NumberOfAvailableLicenses != null ? user.Driver_NumberOfAvailableLicenses : 4),
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
                        new SqlParameter("@currencyId", user.CurrencyId),
                        new SqlParameter("@unitId", user.UnitId),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@driver_NextGoal", user.Driver_NextGoal != null ? user.Driver_NextGoal : DBNull.Value),
                        new SqlParameter("@driver_IBAN", user.Driver_IBAN != null ? user.Driver_IBAN : DBNull.Value),
                        new SqlParameter("@driver_SerialNumber1", user.Driver_SerialNumber1 != null ? user.Driver_SerialNumber1 : DBNull.Value),
                        new SqlParameter("@driver_SerialNumber2", user.Driver_SerialNumber2 != null ? user.Driver_SerialNumber2 : DBNull.Value),
                        new SqlParameter("@driver_DeviceName1", user.Driver_DeviceName1 != null ? user.Driver_DeviceName1 : DBNull.Value),
                        new SqlParameter("@driver_DeviceName2", user.Driver_DeviceName2 != null ? user.Driver_DeviceName2 : DBNull.Value),
                        new SqlParameter("@driver_LastLicenseActivationDate", user.Driver_LastLicenseActivationDate != null ? user.Driver_LastLicenseActivationDate : DBNull.Value),
                        new SqlParameter("@driver_NumberOfAvailableLicenses", user.Driver_NumberOfAvailableLicenses != null ? user.Driver_NumberOfAvailableLicenses : DBNull.Value),
                        new SqlParameter("@driver_Swift_bic", user.Driver_SWIFT_BIC != null ? user.Driver_SWIFT_BIC : DBNull.Value),
                        new SqlParameter("@driver_Address1", user.Driver_Address1 != null ? user.Driver_Address1 : DBNull.Value),
                        new SqlParameter("@driver_Address2", user.Driver_Address2 != null ? user.Driver_Address2 : DBNull.Value),
                        new SqlParameter("@driver_PostalCode", user.Driver_PostalCode != null ? user.Driver_PostalCode : DBNull.Value),
                        new SqlParameter("@driver_PostalLocation", user.Driver_PostalLocation != null ? user.Driver_PostalLocation : DBNull.Value),
                        new SqlParameter("@driver_Vat", user.Driver_VAT != null ? user.Driver_VAT : DBNull.Value),
                        new SqlParameter("@driver_BankCountryId", user.Driver_BankCountryId != null ? user.Driver_BankCountryId : DBNull.Value),
                        new SqlParameter("@isDriver", user.IsDriver),
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
                        new SqlParameter("@currencyId", user.CurrencyId != null ? user.CurrencyId : DBNull.Value),
                        new SqlParameter("@language", user.Language != null ? user.Language : DBNull.Value),
                        new SqlParameter("@unitId", user.UnitId != null ? user.UnitId : DBNull.Value),
                        new SqlParameter("@email", user.Email),
                        new SqlParameter("@password", Settings.Encrypt(user.Password)),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@isDriver", user.IsDriver),
                        new SqlParameter("@driver_NextGoal", user.Driver_NextGoal != null ? user.Driver_NextGoal : DBNull.Value),
                        new SqlParameter("@driver_IBAN", user.Driver_IBAN != null ? user.Driver_IBAN : DBNull.Value),
                        new SqlParameter("@driver_SerialNumber1", user.Driver_SerialNumber1 != null ? user.Driver_SerialNumber1 : DBNull.Value),
                        new SqlParameter("@driver_SerialNumber2", user.Driver_SerialNumber2 != null ? user.Driver_SerialNumber2 : DBNull.Value),
                        new SqlParameter("@driver_DeviceName1", user.Driver_DeviceName1 != null ? user.Driver_DeviceName1 : DBNull.Value),
                        new SqlParameter("@driver_DeviceName2", user.Driver_DeviceName2 != null ? user.Driver_DeviceName2 : DBNull.Value),
                        new SqlParameter("@driver_LastLicenseActivationDate", user.Driver_LastLicenseActivationDate != null ? user.Driver_LastLicenseActivationDate : DBNull.Value),
                        new SqlParameter("@driver_NumberOfAvailableLicenses", user.Driver_NumberOfAvailableLicenses != null ? user.Driver_NumberOfAvailableLicenses : 4),
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
                        new SqlParameter("@currencyId", user.CurrencyId),
                        new SqlParameter("@unitId", user.UnitId),
                        new SqlParameter("@firstName", user.FirstName),
                        new SqlParameter("@lastName", user.LastName),
                        new SqlParameter("@phone", user.Phone != null ? user.Phone : DBNull.Value),
                        new SqlParameter("@driver_IBAN", user.Driver_IBAN != null ? user.Driver_IBAN : DBNull.Value),
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

        private List<SqlParameter> SerializeUserWithdrawal(Withdrawal withdrawal, string userId, string customerId, DataOperation dataOperation)
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
                        new SqlParameter("@userCustomerId", customerId),
                        new SqlParameter("@driver_Swift_bic", withdrawal.Swift_bic),
                        new SqlParameter("@driver_BankCountryId", withdrawal.BankCountryId),
                        new SqlParameter("@driver_PostalCode", withdrawal.Driver_PostalCode),
                        new SqlParameter("@driver_PostalLocation", withdrawal.Driver_PostalLocation),
                        new SqlParameter("@driver_Vat", withdrawal.Driver_VAT),
                        new SqlParameter("@driver_Address1", withdrawal.Driver_Address1),
                        new SqlParameter("@driver_Address2", withdrawal.Driver_Address2),
                        new SqlParameter("@credits", withdrawal.Credits),
                        new SqlParameter("@dateTime", withdrawal.DateTime)
                    };

                    break;

                default:
                    throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), Resources.Resources.OthersResources.ENUM_OPTION_NOT_EXPECTED);
            }

            // return the new created list of sql parameters
            return sqlParameters;
        }

        public string GetErrorMessage(string message)
        {
            string translated_message = "";

            switch (message)
            {
                case "SQLSERVER_VALIDATIONS_LICENSE_CHANGED_RECENTLY":

                    translated_message = Resources.Resources.SQLResources.SQLSERVER_VALIDATIONS_LICENSE_CHANGED_RECENTLY;
                    break;

                case "SQLSERVER_VALIDATIONS_INVALID_SERIAL_NUMBER":

                    translated_message = "SQLSERVER_VALIDATIONS_INVALID_SERIAL_NUMBER";
                    break;

                case "SQLSERVER_VALIDATIONS_ADD_OR_UPDATE":

                    translated_message = Resources.Resources.SQLResources.SQLSERVER_VALIDATIONS_ADD_OR_UPDATE;
                    break;

                case "SQLSERVER_VALIDATIONS_UPDATE":

                    translated_message = Resources.Resources.SQLResources.SQLSERVER_VALIDATIONS_UPDATE;
                    break;

                case "SQLSERVER_VALIDATIONS_LOW_CREDITS":

                    translated_message = Resources.Resources.SQLResources.SQLSERVER_VALIDATIONS_LOW_CREDITS;
                    break;

                case "SQLSERVER_VALIDATIONS_HIGH_CREDITS":

                    translated_message = Resources.Resources.SQLResources.SQLSERVER_VALIDATIONS_HIGH_CREDITS;
                    break;
            }

            return translated_message;

        }
    }
}

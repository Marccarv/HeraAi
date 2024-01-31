using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Exceptions;
using System.Security.Cryptography;

namespace HeraAI.API.Tools
{
    public class Settings
    {
        //Readonly Variables
        private readonly static string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly static string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;

        //State variables

        private static string connectionString = null;
        private static string loggerFilePath = "";
        private static bool isProduction = false;
        //change to variation
        private static int tnetTokenVersion = 1; 
        private static int tnetTokenVariant = 1;
        private static string encryptionKey = "E546C8DF278CD5931069B522E695D4F2";
        private static string fixedToken = "JOZ7BXhHryKWPTyGQ3GxgDhf7mdYBDSpHgabVDeueFUCBLTU6Ifv141MTnSAymNL";
        private static string pathToSavePhotos = "C:/Users/Documents/HeraAI/HeraAI.WebAPI/HeraAI.API.Tools/";
        private static string systemEmail = "testeemailheraai@gmail.com";
        private static string systemPassword = "bfxqvyuygyjkqiqd";
        private static string folderUploadTicketWeighings = "";
        private static string folderUploadTickets = "";
        private static string folderUploadCertificates = "";
        private static string systemEmailHost = "smtp.gmail.com";

        /// <summary>
        /// Connection string for database 
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (connectionString is null) throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), API.Resources.Resources.OthersResources.SETTINGS_ELEMENT_UNDEFINED);
                return connectionString;
            }

            set
            {
                if (!string.IsNullOrEmpty(connectionString)) throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), API.Resources.Resources.OthersResources.SETTINGS_ELEMENT_ALREADY_DEFINED);
                connectionString = value;
            }
        }

        /// <summary>
        /// Get file to write error logs
        /// </summary>
        public static string LoggerFilePath
        {
            get
            {
                if (loggerFilePath is null) throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), API.Resources.Resources.OthersResources.SETTINGS_ELEMENT_UNDEFINED);
                return loggerFilePath;
            }

            set
            {
                if (!string.IsNullOrEmpty(loggerFilePath)) throw new HeraAIExceptionError(currentNamespace, currentClassName, System.Reflection.MethodBase.GetCurrentMethod().ToString(), API.Resources.Resources.OthersResources.SETTINGS_ELEMENT_ALREADY_DEFINED);
                loggerFilePath = value;
            }
        }

        public static int TNetTokenVersion
        {
            get { return tnetTokenVersion; }

            set { tnetTokenVersion = value; }
        }

        public static bool IsProduction
        {
            get { return isProduction; }

            set { isProduction = value; }
        }

        public static string EncryptionKey
        {
            get { return encryptionKey; }

            set { encryptionKey = value; }
        }


        public static string FixedToken
        {
            get { return fixedToken; }

            set { fixedToken = value; }
        }

        public static string PathToSavePhotos
        {
            get { return pathToSavePhotos; }

            set { pathToSavePhotos = value; }
        }

        public static string SystemEmail
        {
            get { return systemEmail; }

            set { systemEmail = value; }
        }

        public static string SystemPassword
        {
            get { return systemPassword; }

            set { systemPassword = value; }
        }

        public static string SystemEmailHost
        {
            get { return systemEmailHost; }

            set { systemEmailHost = value; }
        }

        public static string FolderUploadCertificates
        {
            get { return folderUploadCertificates; }

            set { folderUploadCertificates = value; }
        }

        public static string FolderUploadTicketWeighings
        {
            get { return folderUploadTicketWeighings; }

            set { folderUploadTicketWeighings = value; }
        }

        public static string FolderUploadTickets
        {
            get { return folderUploadTickets; }

            set { folderUploadTickets = value; }
        }

        public static void DefineSettingsWithConfig(IConfiguration configuration)
        {
            var authenticationSection = configuration.GetSection("Authentication");

            string currentDirectory = Directory.GetCurrentDirectory();

            string newDirectory = currentDirectory + "\\public";

            string fileName = "ErrorLogger";

            CheckDiretory(newDirectory);

            LoggerFilePath = newDirectory + "\\logs\\" + fileName;
            FolderUploadCertificates = newDirectory +"\\certificates";
            FolderUploadTicketWeighings = newDirectory + "\\ticketWeighings";
            FolderUploadTickets = newDirectory + "\\tickets";

            IsProduction = bool.Parse(configuration.GetSection("IsProduction").Value);
            ConnectionString = configuration.GetConnectionString("TnetConnectionString");
        }

        public static void CheckDiretory(string newDirectory)
        {

            string pathLogs = newDirectory + "\\logs";

            if (!Directory.Exists(newDirectory))
            {
                Directory.CreateDirectory(newDirectory);
            }

            if (!Directory.Exists(newDirectory + "\\logs"))
            {
                Directory.CreateDirectory(newDirectory + "\\logs");
            }

            if (!Directory.Exists(newDirectory + "\\certificates"))
            {
                Directory.CreateDirectory(newDirectory + "\\certificates");
            }

            if (!Directory.Exists(newDirectory + "\\ticketWeighings"))
            {
                Directory.CreateDirectory(newDirectory + "\\ticketWeighings");
            }

            if (!Directory.Exists(newDirectory + "\\tickets"))
            {
                Directory.CreateDirectory(newDirectory + "\\tickets");
            }
        }

        public static string Encrypt(string text)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream()) 
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(text);
                        }
                        array = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string text)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                };
            }
        }
    }
}
